using Lizy.TerritorialDivisionService.Cache;
using Lizy.TerritorialDivisionService.Data.Entities;
using Lizy.TerritorialDivisionService.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lizy.TerritorialDivisionService.Models
{
    public class DivisionModel
    {
        public Guid Id{ get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }

        public List<CoordinateModel> Coordinates { get; set; }

        public DivisionModel()
        {

        }

        public DivisionModel(DivisionCacheItem division)
        {
            Id = division.Id;
            Code = division.Code;
            DisplayName = division.DisplayName;
            Coordinates = division?.Coordinates?.Select(c => (CoordinateModel)c)?.ToList();
        }

        public static explicit operator DivisionModel(CountyCacheItem county) => new DivisionModel(county);
        public static explicit operator DivisionModel(ParishCacheItem parish) => new DivisionModel(parish);
    }
}
