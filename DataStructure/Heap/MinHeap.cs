namespace XuanLibrary.DataStructure.Heap
{
    using System.Collections.Generic;

    public class MinHeap<T> : Heap<T>
    {
        public MinHeap(T[] array, IComparer<T> comparer = null) : base(array, comparer, false)
        { }

        public MinHeap(T[] array, int capacity, IComparer<T> comparer = null) : base(array, comparer, false, capacity)
        { }
    }
}