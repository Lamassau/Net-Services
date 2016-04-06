using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tekno.Services.Storage.Blobs
{

    public class teknoAppBlobContext : BlobContext
    {

        public teknoAppBlobContext()
            : base()
        {
            ConfigName = "teknoAppConnectionString";
        }

    }

}
