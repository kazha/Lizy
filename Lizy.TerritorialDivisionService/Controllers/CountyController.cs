using Lizy.TerritorialDivisionService.Common;
using Lizy.TerritorialDivisionService.Infrastructure.Services.Interfaces;
using Lizy.TerritorialDivisionService.Models;
using Lizy.TerritorialDivisionService.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lizy.TerritorialDivisionService.Controllers
{
    public class CountyController : LizyControllerBase
    {
        private readonly IRegionManager _regionManager;

        public CountyController(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IList<DivisionModel>),StatusCodes.Status200OK)]
        public async Task<IActionResult> CountyListGet([FromQuery]DivisionFilterModel filter)
        {
            IList<DivisionModel> result;
            var counties = await _regionManager.CountyListGet(filter);
            result = counties.Select(c => (DivisionModel)c).ToList();
            return Ok(result);
        }

        [HttpGet("Summary")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IList<DivisionSummaryModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CountySummaryListGet()
        {
            IList<DivisionSummaryModel> result;
            var counties = await _regionManager.CountySummaryListGet();
            result = counties.Select(c => (DivisionSummaryModel)c).ToList();
            return Ok(result);
        }
    }
}
