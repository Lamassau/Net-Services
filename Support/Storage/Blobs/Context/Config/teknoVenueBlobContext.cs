using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tekno.Services.Storage.Blobs
{
    public class teknoVenueBlobContext : BlobContext
    {

        public teknoVenueBlobContext()
            : base()
        {
            ConfigName = "teknoVenueConnectionString";
        }

    }
}
