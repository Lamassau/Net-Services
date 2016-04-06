
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace tekno.Services.App_Notes
{

    public static class Temp
    {
        //public static  void StoreProfileImageToCloud(string imageUrl, string code)
        //{
        //    WebClient webClient = new WebClient();
        //    using (Stream stream = webClient.OpenRead(imageUrl) )
        //    {
        //        VenueBlob vb = new VenueBlob(code);
        //        vb.Profile.Item.Code = code;
        //        vb.Profile.Item.ContentType = webClient.ResponseHeaders["content-type"];
        //        vb.Profile.Item.WhenUpdated = DateTime.Now;
        //        vb.Profile.Item.WhoUpdated = "teknoapp";
        //        vb.Profile.Item.Parent  = code;
        //        vb.Profile.Add(stream);
        //    }

             

        //}




        //public static void StoreProfileImageToCloud<T>(string imageUrl, string itemCode, string userCode)
        //{
        //    WebClient webClient = new WebClient();
        //    using (Stream stream = webClient.OpenRead(imageUrl))
        //    {
        //        VenueBlob vb = new VenueBlob(itemCode,userCode);
        //        vb.ProfilePhoto.Item.Code = itemCode;
        //        vb.ProfilePhoto.Item.ContentType = webClient.ResponseHeaders["content-type"];
        //        vb.ProfilePhoto.Item.WhenUpdated = DateTime.Now;
        //        vb.ProfilePhoto.Item.WhoUpdated = "teknoapp";
        //        vb.ProfilePhoto.Item.Parent = itemCode;

        //        vb.ProfilePhotos.Item.Code = itemCode;
        //        vb.ProfilePhotos.Item.ContentType = webClient.ResponseHeaders["content-type"];
        //        vb.ProfilePhotos.Item.WhenUpdated = DateTime.Now;
        //        vb.ProfilePhotos.Item.WhoUpdated = "teknoapp";
        //        vb.ProfilePhotos.Item.Parent = itemCode;


        //        vb.ProfilePhoto.Add(stream);
        //        vb.ProfilePhotos.Add(stream);
        //    }



        //}

    }
}