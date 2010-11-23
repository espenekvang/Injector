using System.Collections.Generic;
using System.Threading;
using System;

namespace Injector.Collection
{
    public class SynchronizedDictionary
    {
        private readonly IDictionary<string, object> _collection = new Dictionary<string, object>();
        private readonly ReaderWriterLockSlim _collectionLock = new ReaderWriterLockSlim();
        
        public object Get(string key)
        {
            _collectionLock.EnterReadLock();
            try
            {
                object value;
                if (_collection.TryGetValue(key, out value))
                {
                    return value;
                }
            }
            finally
            {
                _collectionLock.ExitReadLock();
            }
            return null;
        }

        public void Add(string key, object objectToAdd)
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

        public void Add(string key, object objectToAdd, bool overrideExistingValue)
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

        internal void RemoveAll()
        {
            _collectionLock.EnterWriteLock();
            try
            {
                _collection.Clear();
            }
            finally
            {
                _collectionLock.ExitWriteLock();
            }
        }
    }
}
