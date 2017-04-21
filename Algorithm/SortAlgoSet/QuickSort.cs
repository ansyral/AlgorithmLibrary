namespace XuanLibrary.Algorithm.SortAlgoSet
{
    using System;
    using System.Collections.Generic;

    public static class QuickSort
    {
        /// <summary>
        /// stable, use PartitionLastAsPivot.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        public static void SortLastAsPivot<T>(T[] array, IComparer<T> comparer = null)
        {
            comparer = comparer ?? Comparer<T>.Default;
            int len = array.Length;
            SortCore(array, 0, len - 1, comparer, PartitionLastAsPivot);
        }

        /// <summary>
        /// stable, use PartitionFirstAsPivot
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="comparer"></param>
        public static void SortFirstAsPivot<T>(T[] array, IComparer<T> comparer = null)
        {
            comparer = comparer ?? Comparer<T>.Default;
            int len = array.Length;
            SortCore(array, 0, len - 1, comparer, PartitionFirstAsPivot);
        }

        /// <summary>
        /// use last element as pivot. partition the array range [p,q] range to [p,r-1],[r],[r+1,q]
        /// [p,i] less than or equal to pivot, [i+1,j) gt pivot.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <param name="comparer"></param>
        /// <returns>r</returns>
        private static int PartitionLastAsPivot<T>(T[] array, int p, int q, IComparer<T> comparer)
        {
            T pivot = array[q];
            int i = p - 1;
            for (int j = p; j <= q - 1; j++)
            {
                if (comparer.Compare(array[j], pivot) <= 0)
                {
                    i++;
                    Swap(array, i, j);
                }
            }
            Swap(array, i + 1, q);
            return i + 1;
        }

        /// <summary>
        /// use first element as pivot. partition the array range [p,q] range to [p,r-1],[r],[r+1,q]
        /// [p+1,i] lt pivot, [i+1,j) great than or equal to pivot.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <param name="comparer"></param>
        /// <returns>r</returns>
        private static int PartitionFirstAsPivot<T>(T[] array, int p, int q, IComparer<T> comparer)
        {
            T pivot = array[p];
            int i = p;
            for (int j = p + 1; j <= q; j++)
            {
                if (comparer.Compare(array[j], pivot) < 0)
                {
                    i++;
                    Swap(array, i, j);
                }
            }
            Swap(array, i, p);
            return i + 1;
        }

        private static void SortCore<T>(T[] array, int p, int q, IComparer<T> comparer, Func<T[], int, int, IComparer<T>, int> partitioner)
        {
            int r = partitioner(array, p, q, comparer);
            SortCore(array, p, r - 1, comparer, partitioner);
            SortCore(array, r + 1, q, comparer, partitioner);
        }

        private static void Swap<T>(T[] array, int i, int j)
        {
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}
