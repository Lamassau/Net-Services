using Microsoft.WindowsAzure.Storage.Table;
using System.Data.Services.Common;

namespace tekno.Services.Storage.Tables
{
    [DataServiceKey("PartitionKey", "RowKey")]
    public abstract class teknoTable : TableEntity
    {

        public teknoTable(string PartitionKey, string RowKey)
            : base(PartitionKey, RowKey)
        {
            Init();
        }

        public teknoTable(string PartitionKey)
            : base(PartitionKey, StorageHelper.GenerateNewRowKey())
        {
            Init();
        }

        public teknoTable()
        {
            Init();
        }

        public string DataSource { get; set; }
        public bool IsActive { get; set; }

        protected virtual  void Init()
        {

        }

    }

}
