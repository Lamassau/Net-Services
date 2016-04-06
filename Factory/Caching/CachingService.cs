using tekno.Services.Storage.Blobs;



namespace tekno.Services
{
    public class CachingService<CT> : ICachingService
        where CT : IBlobEntity, new()
    {
        public static teknoBlob<CT, T> GetCachService<T>(string item, params string[] args)
        {
            return new CacheBlob<CT, T>(item, args).Cache;
        }

    }

}