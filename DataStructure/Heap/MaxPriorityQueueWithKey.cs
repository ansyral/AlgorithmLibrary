namespace XuanLibrary.DataStructure.Heap
{
    using System.Collections.Generic;
    using XuanLibrary.DataStructure.Common;

    public class MaxPriorityQueue<TKey, TValue> : IMaxPriorityQueue<TKey, TValue> where TValue : IHasKey<TKey>
    {
        private IHeapWithKey<TKey, TValue> _heap;

        public MaxPriorityQueue(TValue[] array, IComparer<TValue> comparer = null)
        {
            _heap = HeapFactory.CreateMax<TKey, TValue>(array, comparer);
        }

        public MaxPriorityQueue(TValue[] array, int capacity, IComparer<TValue> comparer = null)
        {
            _heap = HeapFactory.CreateMax<TKey, TValue>(array, comparer, capacity);
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
            _heap.Update(key, value);
        }

        public void Insert(TValue value)
        {
            _heap.Push(value);
        }
    }
}
