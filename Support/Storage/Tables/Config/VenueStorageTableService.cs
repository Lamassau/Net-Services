using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tekno.Services.Storage.Tables
{
    public class VenueStorageTableService<T> : StorageTableService<T>
        where T : ITableEntity
    {
        public VenueStorageTableService()
            : base("teknoVenueConnectionString")
        { }
    }
}
