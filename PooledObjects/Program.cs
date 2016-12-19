using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PooledArray
{
    interface IPoolableObject : IDisposable
    {
        int Size { get; }
        void Reset();
        void SetPoolManager(PoolManager poolManager);
    }

    class PoolManager
    {
        private class Pool
        {
            public int PooledSize { get; set; }
            public int Count { get { return this.Stack.Count; } }
            public Stack<IPoolableObject> Stack { get; private set; }
            public Pool()
            {
                this.Stack = new Stack<IPoolableObject>();
            }

        }
        const int MaxSizePerType = 10 * (1 << 10); // 10 MB

        Dictionary<Type, Pool> pools = new Dictionary<Type, Pool>();

        public int TotalCount
        {
            get
            {
                int sum = 0;
                foreach (var pool in this.pools.Values)
                {
                    sum += pool.Count;
                }
                return sum;
            }
        }

        public T GetObject<T>() where T : class, IPoolableObject, new()
        {
            Pool pool;
            T valueToReturn = null;
            if (pools.TryGetValue(typeof(T), out pool))
            {
                if (pool.Stack.Count > 0)
                {
                    valueToReturn = pool.Stack.Pop() as T;
                }
            }
            if (valueToReturn == null)
            {
                valueToReturn = new T();
            }
            valueToReturn.SetPoolManager(this);
            return valueToReturn;
        }

        public void ReturnObject<T>(T value) where T : class, IPoolableObject, new()
        {
            Pool pool;
            if (!pools.TryGetValue(typeof(T), out pool))
            {
                pool = new Pool();
                pools[typeof(T)] = pool;
            }

            if (value.Size + pool.PooledSize < MaxSizePerType)
            {
                pool.PooledSize += value.Size;
                value.Reset();                
                pool.Stack.Push(value);
            }
        }
    }

    class MyObject : IPoolableObject
    {
        private PoolManager poolManager;
        public byte[] Data { get; set; }
        public int UsableLength { get; set; }

        public int Size
        {
            get { return Data != null ? Data.Length : 0; }
        }

        void IPoolableObject.Reset()
        {
            UsableLength = 0;
        }

        void IPoolableObject.SetPoolManager(PoolManager poolManager)
        {
            this.poolManager = poolManager;
        }

        public void Dispose()
        {
            this.poolManager.ReturnObject(this);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var poolManager = new PoolManager();
            
            Console.WriteLine("Items in pool: {0}", poolManager.TotalCount);

            using (var obj = poolManager.GetObject<MyObject>())
            {
                obj.Data = new byte[256];
                obj.Data[0] = 13;
                obj.UsableLength = 1;
            }

            Console.WriteLine("Items in pool: {0}", poolManager.TotalCount);

            using (var obj = poolManager.GetObject<MyObject>())
            {
                Console.WriteLine("obj.UsableLength == {0}", obj.UsableLength);
                Console.WriteLine("obj.Data.Length == {0}", obj.Data.Length);

                using (var obj2 = poolManager.GetObject<MyObject>())
                {
                    obj.Data = new byte[512];
                    obj.Data[0] = 14;
                    obj.UsableLength = 1;
                }
            }

            Console.WriteLine("Items in pool: {0}", poolManager.TotalCount);
        }
    }
}
