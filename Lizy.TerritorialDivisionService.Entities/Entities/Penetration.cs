using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lizy.TerritorialDivisionService.Data.Entities
{
    [Owned]
    public class Penetration
    {
        public double Value { get; set; }
        public int PercentileFrom { get; set; }
        public int PercentileTill { get; set; }
    }
}
