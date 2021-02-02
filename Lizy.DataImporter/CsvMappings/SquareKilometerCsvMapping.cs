using Lizy.TerritorialDivisionService.Data.Entities;
using Lizy.TerritorialDivisionService.Data.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using TinyCsvParser.Mapping;
using TinyCsvParser.TypeConverter;

namespace Lizy.DataImporter.CsvMappings
{
    public class SquareKilometerCsvMapping : CsvMapping<SquareKilometer>
    {
        public SquareKilometerCsvMapping() : base()
        {
            var order = 0;
            MapProperty(order++, c => c.Code);
            MapProperty(order++, c => c.Inhabitants);
            MapProperty(order++, c => c.Latitude);
            MapProperty(order++, c => c.Longitude);
            MapProperty(order++, c => c.MaleDensity, new EnumConverter<Density>());
            MapProperty(order++, c => c.KidDensity, new EnumConverter<Density>());
            MapProperty(order++, c => c.WorkAgeDensity, new EnumConverter<Density>());
            MapProperty(order++, c => c.ElderlyDensity, new EnumConverter<Density>());

            var penetrationTokenIndex = order++;
            MapProperty(order++, c => c.Clients);
            MapProperty(order++, c => c.Potential);
            var penetratioBinIndex = order++;

            MapUsing((entity,values) =>
            {
                var result = false;
                if (values.Tokens.Length == order)
                {
                    var penetrationToken = values.Tokens[penetrationTokenIndex];
                    var penetrationBin = values.Tokens[penetratioBinIndex];

                    var penetration = new Penetration();
                    double penetrationValue = 0;
                    penetrationToken = penetrationToken.Replace(',','.');
                    if (double.TryParse(penetrationToken, NumberStyles.Any, CultureInfo.InvariantCulture, out penetrationValue))
                    {
                        penetration.Value = penetrationValue;
                    }

                    var rangeFrom = 0;
                    var rangeTill = 0;
                    
                    penetrationBin = penetrationBin.Replace("%", "");
                    if (penetrationBin.StartsWith('<'))
                    {
                        var rangeTillValue = penetrationBin.Substring(1, penetrationBin.Length-1);
                        result = int.TryParse(rangeTillValue, out rangeTill);
                    }
                    else
                    {
                        var penetrationBinRange = penetrationBin.Split('-');
                        result =
                            int.TryParse(penetrationBinRange[0], out rangeFrom) &&
                            int.TryParse(penetrationBinRange[1], out rangeTill);
                    }
                    penetration.PercentileFrom = rangeFrom;
                    penetration.PercentileTill = rangeTill;
                    entity.Penetration = penetration;
                }
                else
                {
                    result = false;
                }

                return result;
            });
            
        }
    }
}
