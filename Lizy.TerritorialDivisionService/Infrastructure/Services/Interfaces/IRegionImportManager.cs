using Lizy.TerritorialDivisionService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lizy.TerritorialDivisionService.Infrastructure.Services.Interfaces
{
    public interface IRegionImportManager
    {
        public Task ImportData();
        public Task AddCounties(IEnumerable<County> counties);
        public Task AddParishes(IEnumerable<Parish> parishes);
        public Task AddSquareKilometers(IEnumerable<SquareKilometer> squareKilometers);
    }
}
