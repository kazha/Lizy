using Lizy.TerritorialDivisionService.Data;
using Lizy.TerritorialDivisionService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lizy.TerritorialDivisionService.Infrastructure.Repositories
{
    public class RegionRepository: IRegionRepository
    {
        private readonly RegionDbContext _dataContext;

        public RegionRepository(RegionDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        async Task IRegionRepository.AddCounties(IEnumerable<County> counties)
        {
            await _dataContext.Counties.AddRangeAsync(counties);
            _dataContext.SaveChanges();
        }

        async Task IRegionRepository.AddParishes(IEnumerable<Parish> parishes)
        {
            await _dataContext.Parishes.AddRangeAsync(parishes);
            _dataContext.SaveChanges();
        }

        async Task IRegionRepository.AddSquareKilometers(IEnumerable<SquareKilometer> squareKilometers)
        {
            await _dataContext.SquareKilometers.AddRangeAsync(squareKilometers);
            _dataContext.SaveChanges();
        }

        IQueryable<County> IRegionRepository.CountyListGet()
        {
            return _dataContext
                .Counties
                .Include(c=>c.Parishes)
                .ThenInclude(p=>p.SquareKilometers)
                .AsQueryable();
        }

        IQueryable<Parish> IRegionRepository.ParishListGet()
        {
            return _dataContext
                .Parishes
                .Include(p => p.SquareKilometers)
                .AsQueryable();
        }

        IQueryable<SquareKilometer> IRegionRepository.SquareKilometerListGet()
        {
            return _dataContext.SquareKilometers
                .Include( s => s.Parish )
                .ThenInclude(p=>p.County)
                .AsQueryable();
        }

        /// <summary>
        /// Clears db, its sole purpose is to be used for testing
        /// </summary>
        public void ClearDb()
        {
            foreach(var squareKilometer in _dataContext.SquareKilometers)
            {
                squareKilometer.Parish = null;
                squareKilometer.ParishId = null;
                _dataContext.SquareKilometers.Remove(squareKilometer);
            }
            foreach (var parish in _dataContext.Parishes)
            {
                parish.County = null;
                parish.CountyId = null;
                _dataContext.Parishes.Remove(parish);
            }
            foreach (var county in _dataContext.Counties)
            {
                _dataContext.Counties.Remove(county);
            }
        }
    }
}
