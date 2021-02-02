using Lizy.TerritorialDivisionService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TinyCsvParser.TypeConverter;

namespace Lizy.DataImporter.Converters
{
    public class PenetrationConverter : ArrayConverter<Penetration>
    {
        public PenetrationConverter(ITypeConverter<Penetration> internalConverter) : base(internalConverter)
        {
        }
    }
}
