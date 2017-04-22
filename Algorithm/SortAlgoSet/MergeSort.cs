namespace XuanLibrary.Algorithm.SortAlgoSet
{
    using System;
    using System.Collections.Generic;

    public static class MergeSort
    {
        /// <summary>
        /// recursive one. stable. need extra space.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="comparer"></param>
        public static void SortRecursive<T>(T[] array, IComparer<T> comparer = null)
        {
            comparer = comparer ?? Comparer<T>.Default;
            SortRecursiveCore(array, 0, array.Length - 1, comparer);
        }

        private static void SortRecursiveCore<T>(T[] array, int p, int q, IComparer<T> comparer)
        {
            if (p >= q)
            {
                return;
            }
            int mid = (p + q) / 2;
            SortRecursiveCore(array, p, mid, comparer);
            SortRecursiveCore(array, mid + 1, q, comparer);
            Merge(array, p, mid, q, comparer);
        }

        /// <summary>
        /// merge sorted array range [p,q] and (q,r]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <param name="r"></param>
        private static void Merge<T>(T[] array, int p, int q, int r, IComparer<T> comparer)
        {
            int len1 = q - p + 1;
            int len2 = r - q;
            T[] tmp = new T[len1];
            Array.Copy(array, p, tmp, 0, len1);
            int i = q + 1;
            int j = 0;
            int index = p;

            // if T could provide a non used value, like for int is int.MaxValue, we could add sentinel to simplify the process 
            while (i <= r && j < len1)
            {
                int compare = comparer.Compare(array[i], tmp[j]);
                if (compare >= 0)
                {
                    array[index++] = tmp[j++];
                }
                else
                {
                    array[index++] = array[i++];
                }
            }
            while (i <= r)
            {
                array[index++] = array[i++];
            }
            while (j < len1)
            {
                array[index++] = tmp[j++];
            }
        }
    }
}
