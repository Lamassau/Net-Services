
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tekno.Services.Storage.Blobs
{
    public class teknoBlob<CT> : teknoBlob<CT, string>
         where CT : IBlobEntity, new()
    {
        public teknoBlob(string config, string container, string blobId, bool isList=false)
            : base(config, container, blobId)
        { }

    }


    public class teknoBlob<CT, T> : BlobContext.Blob<CT>
        where CT : IBlobEntity, new()
    {
        public teknoBlob(string config, string container, string blobId, bool isList=false)
            : base(config, container, blobId)
        { }

        public T Get()
        {
            ICloudBlob cloudBlob = GetBlob();
            cloudBlob.FetchAttributes();
            Item.Parse(cloudBlob);

            return this.Item.Pull<T>(cloudBlob);
        }

        public CT Load()
        {
            ICloudBlob cloudBlob = GetBlob();
            cloudBlob.FetchAttributes();
            Item.Parse(cloudBlob);

            return Item;
        }


        public void Put(T model)
        {
            using (Stream stream = Item.GetStream<T>(model))
            {
                Item.Parent = typeof(T).Name;
                Item.Code = "N/A";
                Item.WhoUpdated = "app";
                Item.WhenUpdated = DateTime.Now;

                this.Uplaod(Item, stream);
            }
        }

        public DateTimeOffset WhenUpdated
        {
            get
            {
                return Item.WhenUpdated.Value;
            }
        }
        


    }

}