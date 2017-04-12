namespace XuanLibrary.DataStructure.Heap
{
    using System.Collections.Generic;

    public class MinPriorityQueue<T>
    {
        private MinHeap<T> _heap;

        public MinPriorityQueue(T[] array, IComparer<T> comparer = null)
        {
            _heap = new MinHeap<T>(array, comparer);
        }

        public MinPriorityQueue(T[] array, int capacity, IComparer<T> comparer = null)
        {
            _heap = new MinHeap<T>(array, capacity, comparer);
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

        public void DecreaseKey(int i, T key)
        {
            _heap.Update(i, key);
        }

        public void Insert(T key)
        {
            _heap.Push(key);
        }
    }
}
