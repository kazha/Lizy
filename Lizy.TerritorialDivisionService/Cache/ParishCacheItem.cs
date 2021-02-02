using Lizy.TerritorialDivisionService.Data.Entities;
using Lizy.TerritorialDivisionService.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lizy.TerritorialDivisionService.Cache
{
    public class ParishCacheItem : DivisionCacheItem<Parish>
    {
        public Guid CountyId { get; set; }

        public ParishCacheItem(Parish parish)
            :base(parish.SquareKilometers)
        {
            Id = parish.Id;
            CountyId = parish.CountyId ?? Guid.Empty;
            Code = parish.Code;
            DisplayName = parish.DisplayName;
            Coordinates = parish.Coordinates;
        }

        public static explicit operator ParishCacheItem(Parish parish) => new ParishCacheItem(parish);
    }
}
