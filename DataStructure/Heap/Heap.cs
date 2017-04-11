namespace XuanLibrary.DataStructure.Heap
{
    using System;
    using System.Collections.Generic;

    public class Heap<T>
    {
        private T[] _array;

        /// <summary>
        /// the elements count in the heap
        /// </summary>
        public int HeapSize { get; private set; }

        /// <summary>
        /// the array length
        /// </summary>
        public int Length { get; }

        public IComparer<T> Comparer { get; }

        public bool IsMax { get; }

        public Heap(T[] array, IComparer<T> comparer = null, bool isMaxHeap = false)
        {
            Comparer = comparer ?? Comparer<T>.Default;
            IsMax = isMaxHeap;
            Length = array.Length;
            _array = new T[Length];
            Array.Copy(array, _array, Length);
            BuildHeap();
        }

        /// <summary>
        /// build a heap according to the comparer
        /// </summary>
        public void BuildHeap()
        {
            HeapSize = Length;
            for (int i = HeapSize / 2; i >= 0; i--)
            {
                Heapify(i);
            }
        }

        public void Sort()
        {
            //to-do
        }

        /// <summary>
        /// the left child and right child are both heap, adjust root to make it a heap,too.
        /// </summary>
        /// <param name="index">the element's index</param>
        public void Heapify(int index)
        {
            // to-do
        }

        /// <summary>
        /// get the parent element's index
        /// </summary>
        /// <param name="index">an element's index</param>
        /// <returns></returns>
        public int Parent(int index)
        {
            return index / 2;
        }

        /// <summary>
        /// get left child's element's index
        /// </summary>
        /// <param name="index">an element's index</param>
        /// <returns></returns>
        public int LeftChild(int index)
        {
            return index * 2;
        }

        /// <summary>
        /// get right child's element's index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int RightChild(int index)
        {
            return index * 2 + 1;
        }
    }
}
