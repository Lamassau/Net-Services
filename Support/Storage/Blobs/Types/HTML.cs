using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Specialized;
using System.IO;

namespace tekno.Services.Storage.Blobs.Types
{
    public class HTML : BlobEntity
    {

        public HTML()
        {
            this.ContentType = "text/html";
            this.Extension = "html";
        }


        public string Pull(ICloudBlob blob)
        {
            return DownloadText(blob);
        }

        public override Stream GetStream<T>(T  item)
        {
            
            byte[] byteArray = Encoding.ASCII.GetBytes(item.ToString());
            return new MemoryStream(byteArray);
            //using (MemoryStream stream = new MemoryStream(byteArray))
            //{
            //    CT m = new CT() { ID = this.ContainerId, Name = item.GetType().ToString(), WhoUpdated = "app", WhenUpdated = DateTime.Now };
            //    this.Add(stream, m);
            //}
            
        }


    }
}

