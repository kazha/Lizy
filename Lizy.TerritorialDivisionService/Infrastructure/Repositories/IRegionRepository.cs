using Lizy.TerritorialDivisionService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lizy.TerritorialDivisionService.Infrastructure.Repositories
{
    public interface IRegionRepository
    {
        /// <summary>
        /// This one is just for testing purposes
        /// </summary>
        public void ClearDb();
        public IQueryable<County> CountyListGet();
        public IQueryable<Parish> ParishListGet();
        public IQueryable<SquareKilometer> SquareKilometerListGet();

        public Task AddCounties(IEnumerable<County> counties);
        public Task AddParishes(IEnumerable<Parish> parishes);
        public Task AddSquareKilometers(IEnumerable<SquareKilometer> squareKilometers);
    }
}
