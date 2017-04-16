namespace XuanLibrary.DataStructure.Heap
{
    using System.Collections.Generic;

    public class MinPriorityQueue<T> : IMinPriorityQueue<T>
    {
        private IHeap<T> _heap;

        public MinPriorityQueue(T[] array, IComparer<T> comparer = null)
        {
            _heap = HeapFactory.CreateMin<T>(array, comparer);
        }

        public MinPriorityQueue(T[] array, int capacity, IComparer<T> comparer = null)
        {
            _heap = HeapFactory.CreateMin<T>(array, comparer, capacity);
        }

        public int Count
        {
            get { return _heap.HeapSize; }
        }

        public T Min()
        {
            return _heap.Peek();
        }

        public T ExtractMin()
        {
            return _heap.Pop();
        }

        public void Insert(T value)
        {
            _heap.Push(value);
        }
    }
}
