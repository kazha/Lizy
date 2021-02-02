using Lizy.TerritorialDivisionService.Data.Entities;
using Lizy.TerritorialDivisionService.Infrastructure.Repositories;
using Lizy.TerritorialDivisionService.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lizy.TerritorialDivisionService.Infrastructure.Services.Implementations
{
    public class RegionImportManager: IRegionImportManager
    {
        private readonly DivisionCacheService _divisionCacheService;
        private readonly IRegionRepository _regionRepository;
        private readonly DataImporter.DataParser _parser;

        public RegionImportManager(
            DivisionCacheService divisionCache,
            IRegionRepository regionRepository,
            DataImporter.DataParser parser)
        {
            _divisionCacheService = divisionCache;
            _regionRepository = regionRepository;
            _parser = parser;
        }

        Task IRegionImportManager.AddCounties(IEnumerable<County> counties)
        {
            return _regionRepository.AddCounties(counties);
        }

        Task IRegionImportManager.AddParishes(IEnumerable<Parish> parishes)
        {
            return _regionRepository.AddParishes(parishes);
        }

        async Task IRegionImportManager.AddSquareKilometers(IEnumerable<SquareKilometer> squareKilometers)
        {
            await _regionRepository.AddSquareKilometers(squareKilometers);
            _divisionCacheService.LoadCache();
        }

        async Task IRegionImportManager.ImportData()
        {
            _regionRepository.ClearDb();
            await _parser.FullImport(
                async (counties, parishes, squareKilometers) =>
                {
                    var regionManager = (IRegionImportManager)this;
                    await regionManager.AddCounties(counties);
                    await regionManager.AddParishes(parishes);
                    await regionManager.AddSquareKilometers(squareKilometers);
                }
            );
        }
    }
}
