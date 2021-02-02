using System.Text;
using System.Linq;
using TinyCsvParser;
using TinyCsvParser.Mapping;
using Lizy.TerritorialDivisionService.Data.Entities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using Lizy.DataImporter.Common;
using System;
using Lizy.DataImporter.Mappings;
using Lizy.DataImporter.DataObjects;
using SharpKml.Base;
using Lizy.DataImporter.CsvMappings;
using System.Threading.Tasks;
using SharpDom = SharpKml.Dom;
using SharpKml.Engine;

namespace Lizy.DataImporter
{

    /// <summary>
    /// For testing purposes this would do.
    /// In general separated tool should be created as ExternalData can be large.
    /// SharpKml seems slow, deeper investigation should be made, or custom solution created, not sure, perhaps my approach isn't correct
    /// Parallel Task execution would improve performance though
    /// </summary>
    public class DataParser
    {
        /// <summary>
        /// Assuming that this wont be released, just set docker externalData folder path manually
        /// </summary>
        private string _rootPath = "./bin/Debug/netcoreapp3.1/ExternalData/";

        private CsvParserOptions _options;

        public DataParser()
        {
            _options = new CsvParserOptions(true, ',');
        }

        public Task FullImport(Action<IEnumerable<County>, IEnumerable<Parish>,IEnumerable<SquareKilometer>> callback)
        {
            var countyDataObject = new CountyData();
            var parishDataObject = new ParishData();

            var parishCountyData = ParseCsv<ParishToCounty, CountyCsvMapping>($"{_rootPath}{countyDataObject.FilterFilePath}");
            var countyKml = ParseLocalKml($"{_rootPath}{countyDataObject.KmlFilePath}");
            var parishKml = ParseLocalKml($"{_rootPath}{parishDataObject.KmlFilePath}");

            var countyList = new List<County>();
            var parishList = new Dictionary<string,Parish>();
            foreach (var parishCounty in parishCountyData) 
            {
                var county = countyList.FirstOrDefault(c => c.Code == parishCounty.CountyCode);
                if (county == null) 
                {
                    county = new County
                    {
                        Code = parishCounty.CountyCode,
                        DisplayName = parishCounty.CountyDisplayName,
                        Coordinates = countyKml.GetGeometryByCode(parishCounty.CountyCode).ToList()
                    };
                    countyList.Add(county);
                }

                var parish = new Parish
                {
                    Code = parishCounty.ParishCode,
                    DisplayName = parishCounty.ParishDisplayName,
                    CountyId = county.Id,
                    Coordinates = parishKml.GetGeometryByCode(parishCounty.ParishCode).ToList()
                };
                parishList.Add(parish.Code, parish);
            }
            var squareKilometers = ImportSquareKilometers(parishList);
            callback(countyList,parishList.Values, squareKilometers);
            return Task.CompletedTask;
        }

        public IEnumerable<SquareKilometer> ImportSquareKilometers(Dictionary<string, Parish> parishes)
        {
            var squareKilometerDataObject = new SquareKilometerData();
            var parishDataObject = new ParishData();
            var parishData = ParseCsv<SquareKilometerToParish, ParishCsvMapping>($"{_rootPath}{parishDataObject.FilterFilePath}");
            var squareKilometerData = ParseCsv<SquareKilometer, SquareKilometerCsvMapping>($"{_rootPath}{squareKilometerDataObject.FilterFilePath}");
            var squareKilometerKml = ParseLocalKml($"{_rootPath}{squareKilometerDataObject.KmlFilePath}");

            var placemakers = squareKilometerKml
                                .GroupBy(p => p.Name)
                                .ToDictionary(
                                    group => group.Key,
                                    group => group.GetGeometryByCode(group.Key)
                                );
            foreach (var squareKilometer in squareKilometerData)
            {
                squareKilometer.Coordinates = placemakers[squareKilometer.Code].ToList();
                var mappedParish = parishData.FirstOrDefault(p => p.SquareKilometerCode == squareKilometer.Code);
                if(mappedParish!= null)
                {
                    squareKilometer.ParishId = parishes[mappedParish.ParishCode]?.Id;
                }
                yield return squareKilometer;
            }
        }

        private IEnumerable<SharpKml.Dom.Placemark> ParseLocalKml(string path)
        {
            var inputXml = File.ReadAllText(path);
            Parser parser = new Parser();
            parser.ParseString(inputXml, false);
            return (parser.Root as SharpDom.Kml).Flatten().OfType<SharpKml.Dom.Placemark>();
        }

        /// <summary>
        /// Copy/Paste from SharpKml samples
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="placemarks"></param>
        private static void ExtractPlacemarks(SharpDom.Feature feature, List<SharpKml.Dom.Placemark> placemarks)
        {
            if (feature is SharpKml.Dom.Placemark placemark)
            {
                placemarks.Add(placemark);
            }
            else
            {
                if (feature is SharpDom.Container container)
                {
                    foreach (SharpDom.Feature f in container.Features)
                    {
                        ExtractPlacemarks(f, placemarks);
                    }
                }
            }
        }

        private IEnumerable<TEntity> ParseCsv<TEntity,TMapper>(string filePath)
            where TMapper : CsvMapping<TEntity>, new()
            where TEntity: class, new()
        {
            var csvParser = new CsvParser<TEntity>(_options, new TMapper());
            var mappingResults = csvParser.ReadFromFile(filePath, Encoding.UTF8);
            foreach(var mapping in mappingResults)
            {
                if (mapping.IsValid)
                {
                    yield return mapping.Result;
                }
                else
                {
                    Debug.WriteLine($"Row: {mapping.RowIndex} Failed: {mapping.Error}");
                }
            }
        }
    }
}
