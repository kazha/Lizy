using Lizy.TerritorialDivisionService.Common;
using Lizy.TerritorialDivisionService.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lizy.TerritorialDivisionService.Controllers
{
    public class ImportDataController : LizyControllerBase
    {
        private readonly IRegionImportManager _importManager;

        public ImportDataController(IRegionImportManager importManager)
        {
            _importManager = importManager;
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ImportCountyData()
        {
            await _importManager.ImportData();
            return Ok();
        }
    }
}
