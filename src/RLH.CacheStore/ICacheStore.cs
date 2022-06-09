using Microsoft.Extensions.Caching.Memory;

namespace RLH.CacheStore
{
    public interface ICacheStore<TKey,TObject>
    {
        public bool EntryExists(TKey key);
        public bool TryGetEntry(TKey key,out TObject value);
        public bool RemoveEntry(TKey key);
        public bool SetEntry(TKey key, TObject entry, MemoryCacheEntryOptions memoryCacheEntryOptions = null);
    }
}
