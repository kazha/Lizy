using System.Linq;
using System;
using System.Collections.Generic;
using Lizy.TerritorialDivisionService.Data.Entities;
using SharpDom = SharpKml.Dom;
using SharpKml.Engine;

namespace Lizy.DataImporter.Common
{
    public static class KmlExtensions
    {
        public static IEnumerable<Coordinates> GetGeometryByCode(this IEnumerable<SharpDom.Placemark> placemarks,string code)
        {
            var kmlForFilter = placemarks.FirstOrDefault(v => v.Name == code);
            
            if (kmlForFilter != null)// && kmlForFilter.Geometry.Fl is SharpDom.MultipleGeometry geometryList)
            {
                var polygonCoordinates = 
                    kmlForFilter
                        .Flatten()
                        .OfType<SharpDom.MultipleGeometry>()
                        .SelectMany(g => g.Geometry)
                        .SelectMany(g => g.Flatten().OfType<SharpKml.Dom.Polygon>())
                        .Select(p => p?.OuterBoundary?.LinearRing?.Coordinates);
                foreach (var coordinates in polygonCoordinates)
                {
                    //    foreach (var geometry in geometryList.Geometry)
                    //    {
                    //        if (geometry is SharpKml.Dom.Polygon polygin)
                    //        {
                    //            var coordinates = polygin?.OuterBoundary?.LinearRing?.Coordinates;
                    if (coordinates != null)
                    {
                        foreach(var coordinate in coordinates)
                        {
                            yield return new TerritorialDivisionService.Data.Entities.Coordinates
                            {
                                Latitude = coordinate.Latitude,
                                Longitude = coordinate.Longitude
                            };
                        }
                        //                result = coordinates.Select(c =>
                        //                {
                        //                    return new TerritorialDivisionService.Data.Entities.Coordinates
                        //                    {
                        //                        Latitude = c.Latitude,
                        //                        Longitude = c.Longitude
                        //                    };
                        //                }).ToList();
                        //            }
                    }
                }
            }
            //return result;
        }
    }
}
