using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage;
using System.IO;

using System.Text;
using Microsoft.WindowsAzure.Storage.Table;
using System.Linq;
using System.Globalization;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace tekno.Services.Storage.Tables
{
    public abstract class StorageTableService<T>
        where T : ITableEntity
    {
        //private TableServiceContext _teknoServiceContext = null;
        private CloudTable _storage = null;


        public StorageTableService(string _config)
        {
            var storageAccount = CloudStorageAccount.Parse(_config);
            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            
            // Create the table if it doesn't exist.
            CloudTable table = tableClient.GetTableReference(typeof(T).Name);
            table.CreateIfNotExists();
            _storage = table;

        }



        public void Update(T lts) 
        {
            if (lts.Timestamp.Ticks == 0)
            {
                try
                {
                    TableOperation insertOperation = TableOperation.Insert(lts);
                    _storage.Execute(insertOperation);

                }
                catch { }
            }
            else
            {
                try
                {
                    TableOperation replaceOperation = TableOperation.Replace(lts);
                    _storage.Execute(replaceOperation);

                    //teknoServiceContext.AttachTo(HumanizerHelper.Pluralize(lts.GetType().Name), lts, "*");
                }
                catch { }
            }
        }

        public void Delete(T lts) 
        {
            TableOperation deleteOperation = TableOperation.Delete(lts);
            _storage.Execute(deleteOperation);
        }




        public IQueryable<T> Find(Expression<Func<T, bool>> criteria)
        {
            //return (Items != null) ? Items.Where(criteria) : null;
            return (new TableQuery<T>()).Where(criteria);
        }


    }
}