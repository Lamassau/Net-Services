using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tekno.Services.Storage.Tables
{
    public class UserStorageTableService<T> : StorageTableService<T>
        where T : ITableEntity
    {
        public UserStorageTableService()
            : base("teknoUserConnectionString")
        { }
    }
}
