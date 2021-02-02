using Lizy.DataImporter.Common;
using Lizy.TerritorialDivisionService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lizy.DataImporter.DataObjects
{
    public class SquareKilometerData : DataObject<SquareKilometer>
    {
        public override string KmlFilePath => "1km_lv.kml";
        public override string FilterFilePath => "1km_filtri.csv";

        public SquareKilometerData()
        {

        }
    }
}
