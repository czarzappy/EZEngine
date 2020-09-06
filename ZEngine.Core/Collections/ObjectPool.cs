using System;
using System.Collections.Concurrent;

namespace ZEngine.Core.Collections
{
    public class ObjectPool<T>
    {
        private readonly ConcurrentBag<T> mObjects;
        private readonly Func<T> mObjectGenerator;

        public int Count
        {
            get { return mObjects.Count; }
        }

        public ObjectPool(Func<T> objectGenerator)
        {
            if (objectGenerator == null)
            {
                throw new ArgumentNullException("objectGenerator");
            }
            mObjects = new ConcurrentBag<T>();
            mObjectGenerator = objectGenerator;
        }

        public T GetObject()
        {
            T item;
            if (mObjects.TryTake(out item))
            {
                return item;
            }
            
            return mObjectGenerator();
        }

        public void PutObject(T item)
        {
            mObjects.Add(item);
        }
    }
}