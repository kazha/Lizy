using Lizy.TerritorialDivisionService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lizy.TerritorialDivisionService.Models
{
    public class CoordinateModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public CoordinateModel()
        {

        }

        public CoordinateModel(Coordinates coordinates)
        {
            Latitude = coordinates.Latitude;
            Longitude = coordinates.Longitude;
        }

        public static explicit operator CoordinateModel(Coordinates coordinates) => new CoordinateModel(coordinates);
    }
}
