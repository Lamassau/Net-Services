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
    public class teknoBlobs<CT> : teknoBlobs<CT, string>
         where CT : IBlobEntity, new()
    {
        public teknoBlobs(string config, string container, string blobId)
            : base(config, container, blobId)
        { }

    }


    public class teknoBlobs<CT, T> : BlobContext.Blobs<CT>
        where CT : IBlobEntity, new()
    {
        public teknoBlobs(string config, string container, string blobId)
            : base(config, container, blobId)
        { }

        


        public IEnumerable<CT> LoadList()
        {
            var blobsList = new List<CT>();

            // For each item, create a FileEntry which will populate the grid
            foreach (var blobItem in GetBlobs())
            {
                ICloudBlob cloudBlob = this.BlobContainer.GetBlockBlobReference(blobItem.Uri.ToString());
                cloudBlob.FetchAttributes();

                CT t = new CT();
                t.Parse(cloudBlob);

                blobsList.Add(t);
            }

            return blobsList;
        }

        public void  DeleteList(string prefix = "")
        {

            // For each item, create a FileEntry which will populate the grid
            foreach (var blobItem in GetBlobs(prefix))
            {
                ICloudBlob cloudBlob = this.BlobContainer.GetBlockBlobReference(blobItem.Uri.ToString());
                cloudBlob.Delete();
            }
        }

        public IEnumerable<T> GetItems(string prefix = "")
        {
            var blobsList = new List<T>();

            // For each item, create a FileEntry which will populate the grid
            foreach (var blobItem in GetBlobs(prefix))
            {
                ICloudBlob cloudBlob = this.BlobContainer.GetBlockBlobReference(blobItem.Uri.ToString());
                cloudBlob.FetchAttributes();

                CT t = new CT();
                t.Parse(cloudBlob);

                blobsList.Add(t.Pull<T>(cloudBlob));
                //blobsList.Add(this.Item.Pull<T>(cloudBlob));
            }

            return blobsList;
        }

    }

}