using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApplication
{
    public class QueueItem<T>
    {
        public T Value { get; set; }
        public QueueItem<T> Next { get; set; }
        public QueueItem<T> Previous { get; set; }
    }

    public class LimitedSizeStack<T>
    {
        QueueItem<T> head;
        QueueItem<T> tail;
        readonly int sizeLimit;
        int currentSize = 0;

        public LimitedSizeStack(int limit)
        {
            sizeLimit = limit;
        }

        public void Push(T value)
        {
            if (sizeLimit > 0)
            {
                if (currentSize == 0)
                    tail = head = new QueueItem<T> { Value = value, Next = null, Previous = null };
                else
                {
                    if (currentSize >= sizeLimit)
                    {
                        head = head.Next;
                        if(head != null)
                            head.Previous = null;
                        currentSize--;
                    }
                    var newItem = new QueueItem<T> { Value = value, Next = null, Previous = tail };
                    tail.Next = newItem;
                    tail = newItem;
                }
                currentSize++;  
            }
        }

        public T Pop()
        {
            var result = tail.Value;
            tail = tail.Previous;
            currentSize--;

            if (tail == null)
                head = null;
            else
                tail.Next = null;

            return result;
        }

        public int Count
        {
            get
            {
                return currentSize;
            }
        }
    }
}