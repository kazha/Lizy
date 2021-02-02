using Lizy.TerritorialDivisionService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lizy.TerritorialDivisionService.Cache
{
    public class SquareKilometerCacheItem:DivisionCacheItem<SquareKilometer>
    {
        public Guid ParishId { get; set; }

        public SquareKilometerCacheItem(SquareKilometer squareKilometer)
            :base(new List<SquareKilometer> { squareKilometer })
        {
            Id = squareKilometer.Id;
            ParishId = squareKilometer.ParishId ?? Guid.Empty;
            Code = squareKilometer.Code;
            Coordinates = squareKilometer.Coordinates;
        }

        public static explicit operator SquareKilometerCacheItem(SquareKilometer squareKilometer) => new SquareKilometerCacheItem(squareKilometer);
    }
}
