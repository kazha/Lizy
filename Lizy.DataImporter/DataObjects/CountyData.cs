using Lizy.DataImporter.Common;
using Lizy.TerritorialDivisionService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lizy.DataImporter.DataObjects
{
    public class CountyData : DataObject<County>
    {
        public override string KmlFilePath => "novadi_pol.kml";
        public override string FilterFilePath => "nov_pag_mappings.csv";

        public CountyData()
        {

        }
    }
}
