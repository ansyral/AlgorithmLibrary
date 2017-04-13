namespace XuanLibrary.DataStructure.Heap
{
    using System.Collections.Generic;

    public class MaxPriorityQueueWithKey<TKey, TValue> where TValue : IHasKey<TKey>
    {
        private MaxHeap<TValue> _heap;

        public MaxPriorityQueueWithKey(TValue[] array, IComparer<TValue> comparer = null)
        {
            _heap = new MaxHeap<TValue>(array, comparer);
        }

        public MaxPriorityQueueWithKey(TValue[] array, int capacity, IComparer<TValue> comparer = null)
        {
            _heap = new MaxHeap<TValue>(array, capacity, comparer);
        }

        public int Count
        {
            get { return _heap.HeapSize; }
        }

        public TValue Max()
        {
            return _heap.Peek();
        }

        public TValue ExtractMax()
        {
            return _heap.Pop();
        }

        public void IncreasePriority(TKey key, TValue value)
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
