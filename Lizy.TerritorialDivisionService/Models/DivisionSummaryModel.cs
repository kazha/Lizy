using Lizy.TerritorialDivisionService.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lizy.TerritorialDivisionService.Models
{
    public class DivisionSummaryModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public int Inhabitants { get; set; }
        public int Clients { get; set; }
        public int Potential { get; set; }

        public DivisionSummaryModel(DivisionCacheItem item)
        {
            Id = item.Id;
            Code = item.Code;
            Inhabitants = item.Inhabitants;
            Clients = item.Clients;
            Potential = item.Potential;
        }

        public static explicit operator DivisionSummaryModel(CountyCacheItem county) => new DivisionSummaryModel(county);
        public static explicit operator DivisionSummaryModel(ParishCacheItem parish) => new DivisionSummaryModel(parish);
        public static explicit operator DivisionSummaryModel(SquareKilometerCacheItem squareKilometer) => new DivisionSummaryModel(squareKilometer);
    }
}
