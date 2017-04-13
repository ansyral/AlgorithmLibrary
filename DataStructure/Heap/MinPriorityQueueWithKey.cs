namespace XuanLibrary.DataStructure.Heap
{
    using System.Collections.Generic;

    public class MinPriorityQueueWithKey<TKey, TValue> where TValue : IHasKey<TKey>
    {
        private MinHeap<TValue> _heap;

        public MinPriorityQueueWithKey(TValue[] array, IComparer<TValue> comparer = null)
        {
            _heap = new MinHeap<TValue>(array, comparer);
        }

        public MinPriorityQueueWithKey(TValue[] array, int capacity, IComparer<TValue> comparer = null)
        {
            _heap = new MinHeap<TValue>(array, capacity, comparer);
        }

        public int Count
        {
            get { return _heap.HeapSize; }
        }

        public TValue Min()
        {
            return _heap.Peek();
        }

        public TValue ExtractMin()
        {
            return _heap.Pop();
        }

        public void DecreasePriority(TKey key, TValue value)
        {
            int index = GetValue(key);
            _heap.Update(index, value);
        }

        public void Insert(TValue value)
        {
            _heap.Push(value);
        }

        public int GetValue(TKey key)
        {
            for (int i = 0; i < _heap.HeapSize; i++)
            {
                if (_heap[i].Key.Equals(key))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
