namespace XuanLibrary.Algorithm.SortAlgoSet
{
    using System;
    using System.Collections.Generic;

    using XuanLibrary.DataStructure.Heap;

    public static class HeapSort
    {
        /// <summary>
        /// not stable, use data structure max heap
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="comparer"></param>
        public static void Sort<T>(T[] array, IComparer<T> comparer = null)
        {
            var heap = HeapFactory.CreateMax(array, comparer);
            var sorted = heap.Sort();
            Array.Copy(sorted, array, array.Length);
        }
    }
}
