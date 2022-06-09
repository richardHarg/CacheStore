using Microsoft.Extensions.Caching.Memory;
using System;

namespace RLH.CacheStore
{
    public class CacheStore<TKey, TObject> : ICacheStore<TKey, TObject>
    {
        private readonly IMemoryCache _cache;
        public CacheStore(IMemoryCache cache) 
        {
            _cache = cache;
        }

        public bool EntryExists(TKey key)
        {
            return _cache.TryGetValue(key, out TObject value);
        }
        public bool TryGetEntry(TKey key, out TObject value)
        {
            return _cache.TryGetValue(key, out value);
        }
        public bool RemoveEntry(TKey key)
        {
            if (EntryExists(key) == true)
            {
                _cache.Remove(key);
                return true;
            }
            return false;
        }
        public bool SetEntry(TKey key, TObject entry, MemoryCacheEntryOptions memoryCacheEntryOptions = null)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (entry is null)
            {
                throw new ArgumentNullException(nameof(entry));
            }
            if (memoryCacheEntryOptions is null)
            {
                memoryCacheEntryOptions = new MemoryCacheEntryOptions()
                {
                    Priority = CacheItemPriority.NeverRemove,
                    AbsoluteExpiration = DateTimeOffset.Now.AddDays(1)
                };
            }


            // Check if the key already exists in the cache
            if (EntryExists(key))
            {
                return false;
            }

            // if not continue ...
            _cache.Set(key, entry, memoryCacheEntryOptions);

            return true;
        }



    }
}
