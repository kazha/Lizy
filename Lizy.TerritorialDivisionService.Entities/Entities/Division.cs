using Lizy.TerritorialDivisionService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lizy.TerritorialDivisionService.Data.Entities
{
    public class Division: EntityBase
    {
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public List<Coordinates> Coordinates { get; set; }
        public Penetration Penetration { get; set; }
    }
}
