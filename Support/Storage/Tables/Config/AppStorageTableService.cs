
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tekno.Services.Storage.Tables
{
    public class AppStorageTableService<T> : StorageTableService<T>
        where T : ITableEntity
    {
        public AppStorageTableService()
            : base("teknoAppConnectionString")
        {}
    }
}