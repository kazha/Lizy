using Lizy.TerritorialDivisionService.Data.Entities;
using Lizy.TerritorialDivisionService.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lizy.TerritorialDivisionService.Utility;

namespace Lizy.TerritorialDivisionService.Cache
{
    public class CountyCacheItem: DivisionCacheItem<County>
    {
        public CountyCacheItem(County county)
            :base(county.Parishes.SelectMany(p => p.SquareKilometers).ToList())
        {
            Id = county.Id;
            Code = county.Code;
            DisplayName = county.DisplayName;
            Coordinates = county.Coordinates;
        }

        public static explicit operator CountyCacheItem(County county) => new CountyCacheItem(county);
    }
}
