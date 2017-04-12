namespace XuanLibrary.DataStructure.Heap
{
    using System.Collections.Generic;

    public class MaxHeap<T> : Heap<T>
    {
        public MaxHeap(T[] array, IComparer<T> comparer = null) : base(array, comparer, true)
        { }

        public MaxHeap(T[] array, int capacity, IComparer<T> comparer = null) : base(array, comparer, true, capacity)
        { }
    }
}
