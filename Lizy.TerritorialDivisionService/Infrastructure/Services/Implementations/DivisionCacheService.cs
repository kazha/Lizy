using Lizy.TerritorialDivisionService.Cache;
using Lizy.TerritorialDivisionService.Configuration;
using Lizy.TerritorialDivisionService.Data.Entities;
using Lizy.TerritorialDivisionService.Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lizy.TerritorialDivisionService.Infrastructure.Services.Implementations
{
    /// <summary>
    /// Created this just to show the idea.
    /// This works for basic purposes,but more general class/interface has to be created.
    /// </summary>
    public class DivisionCacheService
    {
        private TimeSpan _divisionCacheLifeTime = TimeSpan.Zero;
        private MemoryCacheEntryOptions _cacheEntryOptions;

        /// <summary>
        /// For testing purposes this will do
        /// In more serious scenarious IDistributedCache should be used
        /// in memory or redis or other implementation
        /// </summary>
        private readonly IMemoryCache _memoryCache;

        private readonly IRegionRepository _regionRepository;

        public DivisionCacheService(
            IMemoryCache memoryCache,
            IRegionRepository regionRepository,
            IOptions<TerritorialDivisionConfiguration> options)
        {
            _memoryCache = memoryCache;
            _regionRepository = regionRepository;

            _divisionCacheLifeTime = TimeSpan.FromHours(options.Value.DivisionCacheLifeTimeInHours);
            _cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(_divisionCacheLifeTime);
        }

        protected string GetCacheKey<TEntity>()
            where TEntity: Division
        {
            return typeof(TEntity).Name;
        }

        public void LoadCache()
        {
            LoadCache<CountyCacheItem, County>(_regionRepository.CountyListGet);
            LoadCache<ParishCacheItem, Parish>(_regionRepository.ParishListGet);
            LoadCache<SquareKilometerCacheItem, SquareKilometer>(_regionRepository.SquareKilometerListGet);
        }

        private void LoadCache<TCacheItem,TEntity>(Func<IQueryable<TEntity>> listGet)
            where TEntity: Division
            where TCacheItem: DivisionCacheItem<TEntity>
        {
            var cacheEntry = listGet()
                .ToList()
                .Select(entity => (TCacheItem)Activator.CreateInstance(typeof(TCacheItem), entity))
                .ToList();
            _memoryCache.Set(GetCacheKey<TEntity>(), cacheEntry, _cacheEntryOptions);
        }

        /// <summary>
        /// Gets cached items
        /// If there is none, loads them into cache
        /// </summary>
        public List<TCacheItem> GetCachedItems<TCacheItem,TEntity>()
            where TEntity : Division
            where TCacheItem : DivisionCacheItem<TEntity>
        {
            var cacheKey = GetCacheKey<TEntity>();
            List<TCacheItem> cachedItems;
            if (!_memoryCache.TryGetValue(cacheKey, out cachedItems))
            {
                LoadCache();
                _memoryCache.TryGetValue(cacheKey, out cachedItems);
            }
            return cachedItems;
        }

        /// <summary>
        /// Gets cached item.
        /// If cache is empty laods it.
        /// If cache doesn't create item. Creates one and stores value in cache
        /// </summary>
        public TCacheItem GetCacheItem<TCacheItem,TEntity>(TEntity entity)
            where TEntity: Division
            where TCacheItem: DivisionCacheItem<TEntity>
        {
            var cacheKey = GetCacheKey<TEntity>();
            List<TCacheItem> cachedItems = GetCachedItems<TCacheItem, TEntity>();
            var cachedItem = cachedItems?.FirstOrDefault(c => c.Id == entity.Id);
            if (cachedItem == null)
            {
                cachedItem = (TCacheItem)Activator.CreateInstance(typeof(TCacheItem), entity);
                cachedItems.Add(cachedItem);
                _memoryCache.Set(cacheKey, cachedItems, _cacheEntryOptions);
            }
            return cachedItem;
        }
    }
}
