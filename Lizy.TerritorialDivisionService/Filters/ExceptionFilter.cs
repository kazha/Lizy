using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Lizy.TerritorialDivisionService.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public ExceptionFilter()
        {
        }

        /// <summary>
        /// In general more detailed exception handling should be implemented
        /// But for test purposes this will do, if something failed then, well it failed cos I messed up somewhere
        /// return 500 error for now
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            Debugger.Break();
            context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
