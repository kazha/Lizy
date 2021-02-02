using Lizy.TerritorialDivisionService.Cache;
using Lizy.TerritorialDivisionService.Data.Entities;
using Lizy.TerritorialDivisionService.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lizy.TerritorialDivisionService.Infrastructure.Services.Interfaces
{
    public interface IRegionManager
    {
        /// <summary>
        /// Gets cached county summary
        /// </summary>
        /// <returns>Cached counties</returns>
        public Task<List<CountyCacheItem>> CountySummaryListGet();
        /// <summary>
        /// Gets cached parish summary
        /// </summary>
        /// <returns>Cached parishes</returns>
        public Task<List<ParishCacheItem>> ParishSummaryListGet();
        /// <summary>
        /// Filter and gets counties
        /// </summary>
        /// <returns>Cached counties</returns>
        public Task<List<CountyCacheItem>> CountyListGet(DivisionFilterModel args);
        /// <summary>
        /// Filter and gets parishes by countyId
        /// </summary>
        /// <returns>Cached parishes</returns>
        public Task<List<ParishCacheItem>> ParishListGet(Guid countyId,DivisionFilterModel args);
        /// <summary>
        /// Filter and gets square kilometers by parishId
        /// </summary>
        /// <returns>Cached parishes</returns>
        public Task<List<SquareKilometerCacheItem>> SquareKilometerListGet(Guid parishId,DivisionFilterModel args);
    }
}
