using Lizy.TerritorialDivisionService.Cache;
using Lizy.TerritorialDivisionService.Configuration;
using Lizy.TerritorialDivisionService.Data.Entities;
using Lizy.TerritorialDivisionService.Data.Enums;
using Lizy.TerritorialDivisionService.Infrastructure.Repositories;
using Lizy.TerritorialDivisionService.Infrastructure.Services.Implementations;
using Lizy.TerritorialDivisionService.Infrastructure.Services.Interfaces;
using Lizy.TerritorialDivisionService.Requests;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lizy.TerritorialDivisionService.Infrastructure.Services
{
    public class RegionManager : IRegionManager
    {
        private readonly DivisionCacheService _divisionCacheService;

        public RegionManager(DivisionCacheService divisionCache)
        {
            _divisionCacheService = divisionCache;
        }

        Task<List<CountyCacheItem>> IRegionManager.CountySummaryListGet()
        {
            return Task.FromResult(_divisionCacheService.GetCachedItems<CountyCacheItem, County>());
        }

        Task<List<ParishCacheItem>> IRegionManager.ParishSummaryListGet()
        {
            return Task.FromResult(_divisionCacheService.GetCachedItems<ParishCacheItem, Parish>());
        }

        Task<List<CountyCacheItem>> IRegionManager.CountyListGet(DivisionFilterModel filter)
        {
            return FilteredChachedItemsGet<CountyCacheItem, County>(filter);
        }

        Task<List<ParishCacheItem>> IRegionManager.ParishListGet(Guid countyId, DivisionFilterModel filter)
        {
            return FilteredChachedItemsGet<ParishCacheItem, Parish>(
                filter,
                (s) => s.CountyId == countyId
            );
        }

        Task<List<SquareKilometerCacheItem>> IRegionManager.SquareKilometerListGet(Guid parishId, DivisionFilterModel filter)
        {
            return FilteredChachedItemsGet<SquareKilometerCacheItem, SquareKilometer>(
                filter, 
                (s) => s.ParishId == parishId
            );
        }

        #region Filter Extensions

        private Task<List<TCacheItem>> FilteredChachedItemsGet<TCacheItem, TDivision>(
            DivisionFilterModel filter,
            Func<TCacheItem, bool> primaryFilter = null
        )
            where TDivision : Division
            where TCacheItem : DivisionCacheItem<TDivision>
        {
            var result = _divisionCacheService.GetCachedItems<TCacheItem, TDivision>()
                        .Where(v => primaryFilter == null || primaryFilter(v))
                        .Where(v => FilterCachedValues<TCacheItem, TDivision>(filter, v))
                        .ToList();

            return Task.FromResult(result);
        }

        //Note: FilterCachedValues and FilterDensity probably would be better out in separated place

        /// <summary>
        /// Filters items
        /// Should be simplified, and separated 
        /// </summary>
        private bool FilterCachedValues<TCacheItem, TDivision>(DivisionFilterModel filter, TCacheItem cacheItem)
            where TDivision : Division
            where TCacheItem : DivisionCacheItem<TDivision>
        {
            var clientsFrom = filter.ClientsFrom ?? 0;
            var clientsTill = filter.ClientsTill ?? int.MaxValue;

            var inhabitantsFrom = filter.InhabitantsFrom ?? 0;
            var inhabitantsTill = filter.InhabitantsTill ?? int.MaxValue;

            var potentialFrom = filter.PotentialFrom ?? 0;
            var potentialTill = filter.PotentialTill ?? int.MaxValue;

            var penetrationFrom = filter.PenetrationFrom ?? 0;
            var penetrationTill = filter.PenetrationTill ?? 1;

            var penetrationFilter =
                (
                    (
                        (!filter.PenetrationFrom.HasValue && !filter.PenetrationTill.HasValue) ||
                        cacheItem.Penetrations.Any(p => penetrationFrom <= p.Value && p.Value <= penetrationTill )
                    ) &&
                    (
                        !filter.PenetrationBin.HasValue ||
                        cacheItem.Penetrations.Any(p => p.PercentileFrom <= filter.PenetrationBin && filter.PenetrationBin <= p.PercentileTill )
                    )
                );

            var densityFilter =
                FilterDensity(cacheItem.ElderlyDensity, filter.ElderlyDensity) &&
                FilterDensity(cacheItem.KidDensity, filter.KidDensity) &&
                FilterDensity(cacheItem.MaleDensity, filter.MaleDensity) &&
                FilterDensity(cacheItem.WorkAgeDensity, filter.WorkAgeDensity);

            return
                penetrationFilter &&
                densityFilter &&
                clientsFrom <= cacheItem.Clients &&
                cacheItem.Clients <= clientsTill &&
                potentialFrom <= cacheItem.Potential &&
                cacheItem.Potential <= potentialTill &&
                inhabitantsFrom <= cacheItem.Inhabitants &&
                cacheItem.Inhabitants <= inhabitantsTill;
        }

        private bool FilterDensity(Density? densityToFilter,Density? densityFilter)
        {
            return 
                !densityFilter.HasValue || 
                (   
                    densityToFilter.HasValue && 
                    densityToFilter.Value.HasFlag(densityFilter.Value)
                );
        }

        #endregion
    }
}
