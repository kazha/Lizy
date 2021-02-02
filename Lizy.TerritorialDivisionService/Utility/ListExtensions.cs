using Lizy.TerritorialDivisionService.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lizy.TerritorialDivisionService.Utility
{
    public static class ListExtensions
    {
        /// <summary>
        /// Turns enums intoo Flagged enum value
        /// </summary>
        /// <param name="enums">Enum lists</param>
        /// <returns>bitwise value of density as Enum</returns>
        public static Density ToEnum(this IEnumerable<Density> enums)
        {
            Density result = default;
            enums.Distinct().ToList().ForEach(v => result |= v);
            return result;
        }
    }
}
