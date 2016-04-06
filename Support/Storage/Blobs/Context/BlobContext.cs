using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage;
using System.IO;
using Microsoft.WindowsAzure.Storage.Blob;


namespace tekno.Services.Storage.Blobs
{
    public abstract class BlobContext
    {

        //private  static CloudBlobClient BlobClient = null;


        private static CloudBlobContainer BlobContainer(string config , string blobContainerAddress)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(config);
            CloudBlobClient BlobClient = storageAccount.CreateCloudBlobClient();
            // Get and create the container
            CloudBlobContainer BlobContainer = BlobClient.GetContainerReference(blobContainerAddress);
            BlobContainer.CreateIfNotExists();

            // Setup the permissions on the container to be public
            var permissions = new BlobContainerPermissions();
            permissions.PublicAccess = BlobContainerPublicAccessType.Container;
            BlobContainer.SetPermissions(permissions);

            return BlobContainer;
        }

        protected string ConfigName;
        protected string BlobContID;


        public abstract class Blob<CT> where CT : IBlobEntity, new()
        {


            public CloudBlobContainer BlobContainer { get { return _blobContainer; } }
            
            protected string BlobId { get { return _blobId; } }

            private readonly string _blobId;
            private CloudBlobContainer _blobContainer = null;
            private CT _BlobItem;


            public Blob(string config, string container,string blobId)
            {
                _BlobItem = new CT();
                _blobId = blobId;
                _blobContainer = BlobContainer(config, container);
            }


            public CT Uplaod(CT item, Stream objStream)
            {
                CloudBlockBlob  Bblob;

                // Create the Blob and upload the file
                Bblob = BlobContainer.GetBlockBlobReference(string.Format("{0}.{1}", BlobId, item.Extension));
                
                // Set the properties
                
                item.PopulateMeta(Bblob);
                Bblob.UploadFromStream(objStream);
                Bblob.SetProperties();
                Bblob.SetMetadata();
                item.Parse(Bblob);
                return item;

            }

            public void Delete()
            {
                BlobContainer.GetBlockBlobReference(fullBlobID).DeleteIfExists();
            }

            internal CT Item
            {
                get
                {
                    return _BlobItem;
                }
            }

            internal string fullBlobID
            {
                get
                {
                    return string.Format("{0}.{1}", BlobId, _BlobItem.Extension);
                }
            }


            protected IEnumerable<IListBlobItem> GetBlobs(string prefix = null)
            {
                return BlobContainer.ListBlobs(string.Format("{0}/{1}", BlobId, prefix));
            }


            protected ICloudBlob GetBlob()
            {
                ICloudBlob b = BlobContainer.GetBlockBlobReference(fullBlobID);

                return b;

            }
        }


        public abstract class Blobs<CT> where CT : IBlobEntity, new()
        {


            public CloudBlobContainer BlobContainer { get { return _blobContainer; } }

            protected string BlobId { get { return _blobId; } }

            private readonly string _blobId;
            private CloudBlobContainer _blobContainer = null;
            private CT _BlobItem;

            public Blobs(string config, string container, string blobId)
            {
                _BlobItem = new CT();
                _blobId = blobId;
                _blobContainer = BlobContainer(config, container);
            }


            public CT Add(CT item , Stream objStream)
            {
                CloudBlockBlob Bblob;

                // Create the Blob and upload the file
                
                    Bblob = BlobContainer.GetBlockBlobReference(string.Format("{0}/{1}.{2}", BlobId,
                        //GenerateRandomCode(6)
                        "123456"
                        , item.Extension));
                

                // Set the properties

                item.PopulateMeta(Bblob);
                Bblob.UploadFromStream(objStream);
                Bblob.SetProperties();
                Bblob.SetMetadata();
                item.Parse(Bblob);
                return item;

            }

            //public void Delete()
            //{
            //    BlobContainer.GetBlockBlobReference(fullBlobID).DeleteIfExists();
            //}

            //public CT Item
            //{
            //    get
            //    {
            //        return _BlobItem;
            //    }
            //}

            //internal string fullBlobID
            //{
            //    get
            //    {
            //        return string.Format("{0}.{1}", BlobId, _BlobItem.Extension);
            //    }
            //}


            protected IEnumerable<IListBlobItem> GetBlobs(string prefix = null)
            {
                return BlobContainer.ListBlobs(string.Format("{0}/{1}", BlobId, prefix));
            }


            //protected ICloudBlob GetBlob()
            //{
            //    ICloudBlob b = BlobContainer.GetBlockBlobReference(fullBlobID);

            //    return b;

            //}
        }

    }

}