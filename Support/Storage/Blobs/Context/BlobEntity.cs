using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.Storage;
using System.Runtime.Serialization;
using System.Collections.Specialized;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

namespace tekno.Services.Storage.Blobs
{

    public interface IBlobEntity
    {

        void Parse(ICloudBlob blob);
        void PopulateMeta(ICloudBlob blob);
        T Pull<T>(ICloudBlob blob);
        Stream GetStream<T>(T item);

        Uri BlobUri { get; set; }
        string Parent { get; set; } //ID
        string Code { get; set; } //Name and Extension
        string ContentType { get; set; } //Type
        string Extension { get; set; }

        string WhoUpdated { get; set; }
        DateTimeOffset? WhenUpdated { get; set; }

    }

    public abstract class BlobEntity : IBlobEntity
    {

        public void Parse( ICloudBlob blob)
        {
            this.BlobUri = blob.Uri;
            this.Parent = blob.Metadata["Parent"];
            this.Code = blob.Metadata["Code"];
            this.WhoUpdated = blob.Metadata["WhoUpdated"];
            this.ContentType = blob.Properties.ContentType;
            this.WhenUpdated = blob.Properties.LastModified ; 
        }

        public void PopulateMeta(ICloudBlob blob)
        {
            blob.Properties.ContentType = this.ContentType;
            blob.Metadata["Parent"] = this.Parent;
            blob.Metadata["Code"] = this.Code;
            blob.Metadata["WhoUpdated"] = this.WhoUpdated;
        }

        public Uri BlobUri { get; set; }
        public string Parent { get; set; } 
        public string Code { get; set; } 
        public string WhoUpdated { get; set; }
        public string ContentType { get; set; }
        private string _Extension;

        public string Extension 
        { 
            get 
            {
                //if (string.IsNullOrEmpty(_Extension))
                //{
                //    _Extension = WebHelper.GuessExtension(ContentType);
                //}
                return _Extension;
            }
            set
            {
                _Extension = value;
            }
        }

        public DateTimeOffset? WhenUpdated { get; set; }


        public virtual T Pull<T>(ICloudBlob blob)
        {
            throw new NotImplementedException();
        }

        public virtual Stream GetStream<T>(T item)
        {
            throw new NotImplementedException();
        }

        protected string DownloadText(ICloudBlob blob)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                blob.DownloadToStream(stream);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

    }

}
