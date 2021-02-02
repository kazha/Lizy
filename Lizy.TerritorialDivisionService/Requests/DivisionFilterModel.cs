using Lizy.TerritorialDivisionService.Data.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lizy.TerritorialDivisionService.Requests
{
    /// <summary>
    /// General filter model
    /// Should be split in separated classes and configured with binding if possible
    /// </summary>
    public class DivisionFilterModel
    {
        public Density? MaleDensity { get; set; }
        public Density? KidDensity { get; set; }
        public Density? WorkAgeDensity { get; set; }
        public Density? ElderlyDensity { get; set; }
        public int? InhabitantsFrom { get; set; }
        public int? InhabitantsTill { get; set; }
        public double? PenetrationFrom { get; set; }
        public double? PenetrationTill { get; set; }
        public int? ClientsFrom { get; set; }
        public int? ClientsTill { get; set; }
        public double? PotentialFrom { get; set; }
        public double? PotentialTill { get; set; }
        public int? PenetrationBin { get; set; }
    }
}
