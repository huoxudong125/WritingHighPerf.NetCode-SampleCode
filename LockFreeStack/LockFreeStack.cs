using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LockFreeStack
{
    class LockFreeStack<T>
    {
        private class Node
        {
            public T Value;
            public Node Next;

            public override string ToString()
            {
                return Value.ToString();
            }
        }

        private Node head;
        private int count;

        public int Count { get { return count; } }

        public void Push(T value)
        {
            var newNode = new Node() { Value = value }; 
            
            while (true)
            {
                newNode.Next = this.head;
                if (Interlocked.CompareExchange(ref this.head, newNode, newNode.Next) == newNode.Next)
                {
                    Interlocked.Increment(ref this.count);
                    return;
                }
            }
        }

        public T Pop()
        {
            while (true)
            {
                Node node = this.head;
                if (node == null)
                {
                    return default(T);
                }
                if (Interlocked.CompareExchange(ref this.head, node.Next, node) == node)
                {
                    Interlocked.Decrement(ref this.count);
                    return node.Value;
                }
            }
        }        
    }
}
