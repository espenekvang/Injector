using System;
using System.Collections.Generic;
using System.Threading;

namespace Injector.Collection
{
    public class SynchronizedDictionaryTable<T> where T : class
    {
        private readonly IDictionary<string, T> _collection = new Dictionary<string, T>();
        private readonly ReaderWriterLockSlim _collectionLock = new ReaderWriterLockSlim();
        
        public T Get(string key)
        {
            _collectionLock.EnterReadLock();
            try
            {
                T value;
                if (_collection.TryGetValue(key, out value))
                {
                    return value;
                }
            }
            finally
            {
                _collectionLock.ExitReadLock();
            }
            return default(T);
        }

        public void Add(string key, T objectToAdd)
        {
            _collectionLock.EnterWriteLock();
            try
            {
                _collection.Add(key, objectToAdd);
            }
            finally
            {
                _collectionLock.ExitWriteLock();
            }
        }

        public void Add(string key, T objectToAdd, bool overrideExistingValue)
        {
            if (overrideExistingValue)
            {
                Remove(key);
            }
            Add(key, objectToAdd);
        }

        private void Remove(string key)
        {
            _collectionLock.EnterWriteLock();
            try
            {
                _collection.Remove(key);
            }
            finally
            {
                _collectionLock.ExitWriteLock();
            }
        }
    }
}
