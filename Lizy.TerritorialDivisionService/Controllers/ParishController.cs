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
    public class ParishController : LizyControllerBase
    {
        private readonly IRegionManager _regionManager;

        public ParishController(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        [HttpGet("{countyId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IList<DivisionModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ParishListGet(Guid countyId, [FromQuery]DivisionFilterModel filter)
        {
            IList<DivisionModel> result;
            var counties = await _regionManager.ParishListGet(countyId, filter);
            result = counties.Select(c => (DivisionModel)c).ToList();
            return Ok(result);
        }

        [HttpGet("Summary")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IList<DivisionSummaryModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ParishSummaryListGet()
        {
            IList<DivisionSummaryModel> result;
            var parishes = await _regionManager.ParishSummaryListGet();
            result = parishes.Select(c => (DivisionSummaryModel)c).ToList();
            return Ok(result);
        }
    }
}
