using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tekno.Services;
using tekno.Services.Storage.Blobs;
using tekno.Services.Storage.Blobs.Types;

namespace tekno.Services
{
    public class teknoModelFactory
    {

        public static T GetCachedModel<T>(Func<string[], T> LoadMethod , params string[] args) 
            where T : teknoModel, new()
        {
            teknoBlob<JSON, T> cs = CachingService<JSON>.GetCachService<T>(LoadMethod.Method.Name.ToLower(), args);
            try
            {
                T c =  cs.Get();
                if (!c.IsDirty(cs.WhenUpdated))
                    return c;
            }
            catch
            { }

            T m = LoadMethod(args);
            cs.Put(m);
            return m;
        }

        public static void DeleteCachedModel<T>(Func<string[], T> LoadMethod, params string[] args)
            where T : teknoModel, new()
        {

            teknoBlob<JSON, T> cs = CachingService<JSON>.GetCachService<T>(LoadMethod.Method.Name.ToLower(), args);
            cs.Delete();
        }

        public static T GetModel<T>(Func<string[], T> LoadMethod, params string[] args) 
            where T : teknoModel, new()
        {
            T m = LoadMethod(args);
            return m;
        }

    }
    
}
