using System;
using tekno.Services.Storage.Blobs;


namespace tekno.Services
{
    public class CacheBlob<CT, T> : teknoAppBlobContext
        where CT : IBlobEntity, new()
    {

        public CacheBlob(string item, params string[] args)
            : base()
        {
            switch (typeof(CT).Name)
            {
                case "JSON":
                    BlobContID = string.Format("cache-{0}", typeof(T).Name.ToLower());
                    break;
                case "HTML":
                    BlobContID = string.Format("widget-html");
                    break;

                default:
                    throw new NotImplementedException();
            }

            BlobID = (item + ((args.Length == 0) ? "" : "/" + string.Join("-", args).Replace(" ", "-"))).Replace("|", "-").ToLower();


        }

        private string BlobID;

        private teknoBlob<CT, T> _Cache;
        public teknoBlob<CT, T> Cache
        {
            get
            {
                if (_Cache == null)
                {

                    _Cache = new teknoBlob<CT, T>(ConfigName, BlobContID, BlobID);
                }
                return _Cache;
            }
        }


    }
}