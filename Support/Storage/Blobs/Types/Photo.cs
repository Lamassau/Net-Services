using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Specialized;
using System.IO;


namespace tekno.Services.Storage.Blobs.Types
{
    public class Photo : BlobEntity
    {
        public Photo()
        { 
        }

        public string Pull(ICloudBlob blob)
        {
            return blob.Uri.ToString();
        }

        public  Stream GetStream<T>(Stream item) 
        {
            return (Stream) item;
        }

        

    }
}
