using Lizy.TerritorialDivisionService.Cache;
using Lizy.TerritorialDivisionService.Data.Entities;
using Lizy.TerritorialDivisionService.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lizy.TerritorialDivisionService.Models
{
    public class SquareKilometerModel: DivisionModel
    {
        public int Inhabitants { get; set; }
        public int Clients { get; set; }
        public int Potential { get; set; }
        public double Penetration { get; set; }
        public Density ElderlyDensity { get; set; }
        public Density WorkAgeDensity { get; set; }
        public Density MaleDensity { get; set; }
        public Density KidDensity { get; set; }

        private string _penetrationBin;
        public string PenetrationBin { get => _penetrationBin; }

        public SquareKilometerModel(SquareKilometerCacheItem squareKilometer)
            :base(squareKilometer)
        {
            Inhabitants = squareKilometer.Inhabitants;
            Clients = squareKilometer.Clients;
            Potential = squareKilometer.Potential;
            var penetration = squareKilometer.Penetrations.FirstOrDefault();
            Penetration = penetration.Value;
            _penetrationBin = GetPenetrationBin(penetration);
            ElderlyDensity = squareKilometer.ElderlyDensity.Value;
            WorkAgeDensity = squareKilometer.WorkAgeDensity.Value;
            MaleDensity = squareKilometer.MaleDensity.Value;
            KidDensity = squareKilometer.KidDensity.Value;
        }

        /// <summary>
        /// Not sure if there is a better way to convert result models
        /// Maybe Binding!??
        /// </summary>
        /// <param name="penetration"></param>
        /// <returns></returns>
        private string GetPenetrationBin(Penetration penetration)
        {
            var percentileFrom = penetration.PercentileFrom;
            var percentileTill = penetration.PercentileTill;
            //Default result
            var result = $"{percentileFrom}% - {percentileTill}%";
            //Shows if only less 10%
            if(percentileTill <= 10)
            {
                result = $" < {percentileTill}%";
            }
            return result;
        }
        public static explicit operator SquareKilometerModel(SquareKilometerCacheItem squareKilometer) => new SquareKilometerModel(squareKilometer);
    }
}
