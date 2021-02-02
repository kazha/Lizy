using Lizy.TerritorialDivisionService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lizy.TerritorialDivisionService.Data.Entities
{
    public class County : Division
    {
        public List<Parish> Parishes { get; set; }
    }
}
