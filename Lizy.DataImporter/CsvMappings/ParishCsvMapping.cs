using Lizy.DataImporter.Mappings;
using Lizy.TerritorialDivisionService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TinyCsvParser.Mapping;

namespace Lizy.DataImporter.CsvMappings
{
    public class ParishCsvMapping : CsvMapping<SquareKilometerToParish>
    {
        public ParishCsvMapping()
        {
            int order = 0;
            MapProperty(order++, v => v.SquareKilometerCode);
            MapProperty(order++, v => v.ParishCode);
        }
    }
}
