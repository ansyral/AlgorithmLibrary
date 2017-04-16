namespace XuanLibrary.DataStructure.Heap
{
    using System.Collections.Generic;

    public class MinPriorityQueue<TKey, TValue> : IMinPriorityQueue<TKey, TValue> where TValue : IHasKey<TKey>
    {
        private IHeapWithKey<TKey, TValue> _heap;

        public MinPriorityQueue(TValue[] array, IComparer<TValue> comparer = null)
        {
            _heap = HeapFactory.CreateMin<TKey, TValue>(array, comparer);
        }

        public MinPriorityQueue(TValue[] array, int capacity, IComparer<TValue> comparer = null)
        {
            _heap = HeapFactory.CreateMin<TKey, TValue>(array, comparer, capacity);
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
            _heap.Update(key, value);
        }

        public void Insert(TValue value)
        {
            _heap.Push(value);
        }
    }
}
