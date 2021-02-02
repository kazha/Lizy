using Lizy.TerritorialDivisionService.Common;
using Lizy.TerritorialDivisionService.Infrastructure.Services.Interfaces;
using Lizy.TerritorialDivisionService.Models;
using Lizy.TerritorialDivisionService.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lizy.TerritorialDivisionService.Controllers
{
    public class SquareKilometerController : LizyControllerBase
    {
        private readonly IRegionManager _regionManager;

        public SquareKilometerController(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        [HttpGet("{parishId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IList<SquareKilometerModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SquareKilometerListGet(Guid parishId,[FromQuery] DivisionFilterModel filter)
        {
            IList<SquareKilometerModel> result;
            var squareKilometers = await _regionManager.SquareKilometerListGet(parishId,filter);
            result = squareKilometers.Select(c => (SquareKilometerModel)c).ToList();
            return Ok(result);
        }
    }
}
