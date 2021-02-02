using System;
using System.Collections.Generic;
using System.Text;

namespace Lizy.TerritorialDivisionService.Data.Entities
{
    public class Parish: Division
    {
        public Guid? CountyId { get; set; }
        public County County { get; set; }
        public List<SquareKilometer> SquareKilometers { get; set; }
    }
}
