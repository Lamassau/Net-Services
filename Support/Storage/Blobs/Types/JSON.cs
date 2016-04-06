using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Specialized;
using System.IO;
using Newtonsoft.Json;

namespace tekno.Services.Storage.Blobs.Types
{
    public class JSON : BlobEntity
    {
        public JSON()
        {
            this.ContentType = "application/json";
            this.Extension = "json";
        }

        public override T Pull<T>(ICloudBlob blob)
        {
            string json = DownloadText(blob);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public override Stream GetStream<T>(T item)
        {
            string json = JsonConvert.SerializeObject(item);
            byte[] byteArray = Encoding.ASCII.GetBytes(json);
            return new MemoryStream(byteArray);

        }


    }
}       