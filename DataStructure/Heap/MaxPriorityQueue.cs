namespace XuanLibrary.DataStructure.Heap
{
    using System.Collections.Generic;

    public class MaxPriorityQueue<T> : IMaxPriorityQueue<T>
    {
        private IHeap<T> _heap;

        public MaxPriorityQueue(T[] array, IComparer<T> comparer = null)
        {
            _heap = HeapFactory.CreateMax<T>(array, comparer);
        }

        public MaxPriorityQueue(T[] array, int capacity, IComparer<T> comparer = null)
        {
            _heap = HeapFactory.CreateMax<T>(array, comparer, capacity);
        }

        public int Count
        {
            get { return _heap.HeapSize; }
        }

        public T Max()
        {
            return _heap.Peek();
        }

        public T ExtractMax()
        {
            return _heap.Pop();
        }

        public void Insert(T value)
        {
            _heap.Push(value);
        }
    }
}
