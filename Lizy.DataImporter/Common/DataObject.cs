using Lizy.TerritorialDivisionService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lizy.DataImporter.Common
{
    public abstract class DataObject<TEntity>
        where TEntity: Division
    {
        public Type Type { get => typeof(TEntity); }

        public abstract string KmlFilePath { get; }
        public abstract  string FilterFilePath { get; }        
    }
}
