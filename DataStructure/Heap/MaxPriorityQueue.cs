﻿namespace XuanLibrary.DataStructure.Heap
{
    using System.Collections.Generic;

    public class MaxPriorityQueue<T>
    {
        private MaxHeap<T> _heap;

        public MaxPriorityQueue(T[] array, IComparer<T> comparer = null)
        {
            _heap = new MaxHeap<T>(array, comparer);
        }

        public MaxPriorityQueue(T[] array, int capacity, IComparer<T> comparer = null)
        {
            _heap = new MaxHeap<T>(array, capacity, comparer);
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

        public void IncreasePriority(int i, T value)
        {
            _heap.Update(i, value);
        }

        public void Insert(T value)
        {
            _heap.Push(value);
        }
    }
}
