
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tekno.Services.Storage.Tables
{
    public static class StorageHelper
    {
        public static string GetPartitionKey(string ID)
        {
            return ID.Remove(ID.Length - (GetRowKey(ID).Length + 1));
        }

        public static string GetRowKey(string ID)
        {
            string[] AID = DeConstructID(ID);
            return AID[AID.Length - 1];
        }

        public static string ConstructID(string PartitionKey, string RowKey)
        {
            return string.Format("{0},{1}", PartitionKey, RowKey);
        }

        public static string[] DeConstructID(string ID)
        {
            return ID.Split(',');
        }

        public static string ConstrucKeys(string key1, string key2)
        {
            return string.Format("{0}!{1}", key1, key2);
        }

        public static string[] DeConstrucKeys(string keys)
        {
            return keys.Split('!');
        }

        public static string GenerateNewRowKey()
        {
            return string.Format("{0}", Guid.NewGuid());
        }
    }

    
}
