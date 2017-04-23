namespace XuanLibrary.Algorithm.SortAlgoSet
{
    using System;
    using System.Collections.Generic;

    using XuanLibrary.DataStructure.Common;

    /// <summary>
    /// sorting algos that are not based on comparing
    /// </summary>
    public static class NonComparableSort
    {
        /// <summary>
        /// stable sort, not in place(need extra memory)
        /// </summary>
        /// <param name="array"></param>
        /// <param name="k">the upper bound of array's value, namely [0, k)</param>
        public static void CountSort<T>(T[] array, int k) where T : IHasKey<int>
        {
            int[] count = new int[k];
            T[] sorted = new T[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                count[array[i].Key]++;
            }
            for (int i = 1; i < k; i++)
            {
                count[i] += count[i - 1];
            }
            for (int i = array.Length - 1; i >= 0; i--)
            {
                sorted[count[array[i].Key] - 1] = array[i];
                count[array[i].Key]--;
            }
            Array.Copy(sorted, array, array.Length);
        }

        /// <summary>
        /// int version
        /// </summary>
        /// <param name="array"></param>
        /// <param name="k"></param>
        public static void CountSort(int[] array, int k)
        {
            int[] count = new int[k];
            int[] sorted = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                count[array[i]]++;
            }
            for (int i = 1; i < k; i++)
            {
                count[i] += count[i - 1];
            }
            for (int i = array.Length - 1; i >= 0; i--)
            {
                sorted[count[array[i]] - 1] = array[i];
                count[array[i]]--;
            }
            Array.Copy(sorted, array, array.Length);
        }

        /// <summary>
        /// stable sort
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="k">the upper bound of array's value, namely [0, k). we would use k buckets here.</param>
        public static void BucketSort<T>(T[] array, int k) where T : IHasKey<int>
        {
            List<T>[] buckets = new List<T>[k];
            for (int i = 0; i < k; i++)
            {
                buckets[i] = new List<T>();
            }
            for (int i = 0; i < array.Length; i++)
            {
                buckets[array[i].Key].Add(array[i]);
            }
            int index = 0;
            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < buckets[i].Count; j++)
                {
                    array[index + j] = buckets[i][j];
                }
                index += buckets[i].Count;
            }
        }

        /// <summary>
        /// int version
        /// </summary>
        /// <param name="array"></param>
        /// <param name="k"></param>
        public static void BucketSort(int[] array, int k)
        {
            List<int>[] buckets = new List<int>[k];
            for (int i = 0; i < k; i++)
            {
                buckets[i] = new List<int>();
            }
            for (int i = 0; i < array.Length; i++)
            {
                buckets[array[i]].Add(array[i]);
            }
            int index = 0;
            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < buckets[i].Count; j++)
                {
                    array[index + j] = buckets[i][j];
                }
                index += buckets[i].Count;
            }
        }
    }
}
