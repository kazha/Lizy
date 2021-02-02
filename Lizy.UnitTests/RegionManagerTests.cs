using Lizy.TerritorialDivisionService.Cache;
using Lizy.TerritorialDivisionService.Configuration;
using Lizy.TerritorialDivisionService.Data;
using Lizy.TerritorialDivisionService.Data.Entities;
using Lizy.TerritorialDivisionService.Data.Enums;
using Lizy.TerritorialDivisionService.Infrastructure.Repositories;
using Lizy.TerritorialDivisionService.Infrastructure.Services;
using Lizy.TerritorialDivisionService.Infrastructure.Services.Implementations;
using Lizy.TerritorialDivisionService.Infrastructure.Services.Interfaces;
using Lizy.TerritorialDivisionService.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Lizy.UnitTests
{
    /// <summary>
    /// Filter Unit test
    /// </summary>
    public class RegionUnitTests
    {
        private IRegionManager _regionManager;

        public RegionUnitTests()
        {
            var memoryCacheMock = CreateMemoryCacheMock();
            var options = new DbContextOptionsBuilder<RegionDbContext>()
                            .UseInMemoryDatabase(databaseName: "RegionDb")
                            .Options;
            var regionDbContextMock = new Mock<RegionDbContext>(options);
            IRegionRepository regionRepository = new RegionRepository(regionDbContextMock.Object);
            var optionsMock = CreateOptionsMock();
            _regionManager = new RegionManager(new DivisionCacheService(memoryCacheMock.Object,regionRepository,optionsMock.Object));
        }

        private Mock<IOptions<TerritorialDivisionConfiguration>> CreateOptionsMock()
        {
            var mock = new Mock<IOptions<TerritorialDivisionConfiguration>>();
            mock.SetupGet(p => p.Value).Returns(new TerritorialDivisionConfiguration());
            return mock;
        }

        delegate void OutDelegate<TIn, TOut>(TIn input, out TOut output);

        private Mock<IMemoryCache> CreateMemoryCacheMock()
        {
            var mock = new Mock<IMemoryCache>();
            var penetration = new Penetration { Value = 0.5, PercentileFrom = 2, PercentileTill = 10 };
            var coordinates = new List<Coordinates> { new Coordinates { Latitude = 1, Longitude = 2 } };

            var squareKilometers = new List<SquareKilometer>
            {
                new SquareKilometer
                {
                    Code ="1",
                    Coordinates = coordinates,
                    Penetration = penetration,
                    Clients = 100,
                    Inhabitants = 200,
                    Latitude = 2,
                    Longitude = 5,
                    Potential = 20,
                    ElderlyDensity = Density.Medium,
                    WorkAgeDensity = Density.Medium,
                    MaleDensity = Density.Medium,
                    KidDensity = Density.Medium
                },
                new SquareKilometer
                {
                    Code ="2",
                    Coordinates = coordinates,
                    Penetration = penetration,
                    Clients = 10,
                    Inhabitants = 20,
                    Latitude = 1,
                    Longitude = 3,
                    Potential = 2,
                    ElderlyDensity = Density.High,
                    WorkAgeDensity = Density.High,
                    MaleDensity = Density.High,
                    KidDensity = Density.High
                }
            };

            var parishes = new List<Parish> 
            {
                new Parish
                {
                    Code = "Parish",
                    DisplayName = "TestParish",
                    Penetration = penetration,
                    Coordinates = coordinates,
                    SquareKilometers = squareKilometers
                },
                new Parish
                {
                    Code = "Parish2",
                    DisplayName = "TestParish2",
                    Penetration = penetration,
                    Coordinates = coordinates,
                    SquareKilometers = new List<SquareKilometer>()
                }
            };
            var county = new County
            {
                Code = "CountyCode",
                DisplayName = "Test",
                Coordinates = coordinates,
                Penetration = penetration,
                Parishes = parishes
            };

            var counties = new List<CountyCacheItem>
            {
                new CountyCacheItem(county)
            };
            object countyMockObject;
            mock.Setup(c => c.TryGetValue(typeof(County).Name, out countyMockObject))
                .Callback(new OutDelegate<object, object>((object k, out object v) => v = counties))
                .Returns(true);
            return mock;
        }

        [Fact]
        public async Task FilterDensity()
        {
            var filter = new DivisionFilterModel();
            var result = await _regionManager.CountyListGet(filter);
            Assert.Single(result);

            filter.ElderlyDensity = Density.Low;
            result = await _regionManager.CountyListGet(filter);
            Assert.Empty(result);

            filter.ElderlyDensity = Density.High;
            filter.WorkAgeDensity = Density.Low;
            result = await _regionManager.CountyListGet(filter);
            Assert.Empty(result);

            filter.WorkAgeDensity = Density.High;
            filter.KidDensity = Density.Low;
            result = await _regionManager.CountyListGet(filter);
            Assert.Empty(result);

            filter.KidDensity = Density.High;
            filter.MaleDensity = Density.Low;
            result = await _regionManager.CountyListGet(filter);
            Assert.Empty(result);

            filter.MaleDensity = Density.High;
            result = await _regionManager.CountyListGet(filter);
            Assert.Single(result);
        }

        [Fact]
        public async Task FilterInhabitants()
        {
            var filter = new DivisionFilterModel
            {
                InhabitantsFrom = 221
            };
            var result = await _regionManager.CountyListGet(filter);
            Assert.Empty(result);

            filter.InhabitantsFrom = null;
            filter.InhabitantsTill = 219;
            result = await _regionManager.CountyListGet(filter);
            Assert.Empty(result);

            filter.InhabitantsFrom = 220;
            filter.InhabitantsTill = null;
            result = await _regionManager.CountyListGet(filter);
            Assert.Single(result);

            filter.InhabitantsTill = 220;
            result = await _regionManager.CountyListGet(filter);
            Assert.Single(result);

            filter.InhabitantsFrom = 200;
            filter.InhabitantsTill = 300;
            result = await _regionManager.CountyListGet(filter);
            Assert.Single(result);
        }

        [Fact]
        public async Task FilterClients()
        {
            var filter = new DivisionFilterModel
            {
                ClientsFrom = 100,
                ClientsTill = 200
            };
            var result = await _regionManager.CountyListGet(filter);
            Assert.Single(result);

            filter.ClientsFrom = 200;
            filter.ClientsTill = 300;
            result = await _regionManager.CountyListGet(filter);
            Assert.Empty(result);
        }

        [Fact]
        public async Task FilterPotential()
        {
            var filter = new DivisionFilterModel
            {
                PotentialFrom = 20,
                PotentialTill = 30
            };
            var result = await _regionManager.CountyListGet(filter);
            Assert.Single(result);

            filter.PotentialFrom = 23;
            filter.PotentialTill = 30;
            result = await _regionManager.CountyListGet(filter);
            Assert.Empty(result);
        }

        [Fact]
        public async Task FilterPenetration()
        {
            var filter = new DivisionFilterModel
            {
                PenetrationFrom = 0.5,
                PenetrationTill = 0.6
            };
            var result = await _regionManager.CountyListGet(filter);
            Assert.Single(result);

            filter.PenetrationFrom = 0.6;
            filter.PenetrationTill = 0.7;
            result = await _regionManager.CountyListGet(filter);
            Assert.Empty(result);
        }

        [Fact]
        public async Task FilterPenetrationBin()
        {
            var filter = new DivisionFilterModel
            {
                PenetrationBin = 5,
            };
            var result = await _regionManager.CountyListGet(filter);
            Assert.Single(result);

            filter.PenetrationBin = 22;
            result = await _regionManager.CountyListGet(filter);
            Assert.Empty(result);
        }
    }
}
