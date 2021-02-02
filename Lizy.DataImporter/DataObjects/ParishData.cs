using Lizy.DataImporter.Common;
using Lizy.TerritorialDivisionService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lizy.DataImporter.DataObjects
{
    public class ParishData : DataObject<Parish>
    {
        public override string KmlFilePath => "pagasti_pol.kml";
        public override string FilterFilePath => "pagasta_km_mappings.csv";
    }
}
