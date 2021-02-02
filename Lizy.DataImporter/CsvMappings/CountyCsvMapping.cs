using Lizy.DataImporter.Mappings;
using Lizy.TerritorialDivisionService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TinyCsvParser.Mapping;

namespace Lizy.DataImporter.CsvMappings
{
    public class CountyCsvMapping : CsvMapping<ParishToCounty>
    {
        public CountyCsvMapping() : base()
        {
            var order = 0;
            MapProperty(order++, c=>c.CountyCode);
            MapProperty(order++, c => c.ParishDisplayName);
            MapProperty(order++, c => c.ParishCode);
            MapProperty(order++, c => c.CountyDisplayName);
        }
    }
}
