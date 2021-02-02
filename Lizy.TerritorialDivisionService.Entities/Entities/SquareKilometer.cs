using Lizy.TerritorialDivisionService.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lizy.TerritorialDivisionService.Data.Entities
{
    public class SquareKilometer: Division
    {
        public Guid? ParishId { get; set; }
        public Parish Parish { get; set; }

        public double Inhabitants { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Density MaleDensity { get; set; }
        public Density KidDensity { get; set; }
        public Density WorkAgeDensity{ get; set; }
        public Density ElderlyDensity { get; set; }
        public int Clients { get; set; }
        public int Potential { get; set; }
    }
}
