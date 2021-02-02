using Lizy.TerritorialDivisionService.Data.Entities;
using Lizy.TerritorialDivisionService.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lizy.TerritorialDivisionService.Utility;

namespace Lizy.TerritorialDivisionService.Cache
{
    public class DivisionCacheItem
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public int Inhabitants { get; set; }
        public int Clients { get; set; }
        public int Potential { get; set; }
        public List<Penetration> Penetrations { get; set; }
        public Density? ElderlyDensity { get; set; }
        public Density? WorkAgeDensity { get; set; }
        public Density? MaleDensity { get; set; }
        public Density? KidDensity { get; set; }
        public List<Coordinates> Coordinates { get; set; }
    }

    public class DivisionCacheItem<TEntity>: DivisionCacheItem
        where TEntity : Division
    {
        protected DivisionCacheItem(TEntity TEntity){}
        protected DivisionCacheItem(List<SquareKilometer> squareKilometers)
        {
            Inhabitants = (int)squareKilometers.Sum(t => t.Inhabitants);
            Clients = squareKilometers.Sum(t => t.Clients);
            Potential = squareKilometers.Sum(t => t.Potential);
            ElderlyDensity = squareKilometers.Select(t => t.ElderlyDensity).ToEnum();
            WorkAgeDensity = squareKilometers.Select(t => t.ElderlyDensity).ToEnum();
            MaleDensity = squareKilometers.Select(t => t.ElderlyDensity).ToEnum();
            KidDensity = squareKilometers.Select(t => t.ElderlyDensity).ToEnum();
            Penetrations = squareKilometers.Select(t => t.Penetration).ToList();
        }
    }
}
