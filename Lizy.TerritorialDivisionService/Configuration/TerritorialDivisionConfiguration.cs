using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lizy.TerritorialDivisionService.Configuration
{
    public class TerritorialDivisionConfiguration
    {
        public const string Path = "ServiceConfiguration";
        public int DivisionCacheLifeTimeInHours { get; set; } = 24;
    }
}
