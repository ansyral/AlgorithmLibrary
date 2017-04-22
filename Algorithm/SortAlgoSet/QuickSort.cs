namespace XuanLibrary.Algorithm.SortAlgoSet
{
    using System;
    using System.Collections.Generic;

    public static class QuickSort
    {
        /// <summary>
        /// notstable, use PartitionLastAsPivot.(87183564)
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
        /// nonstable, use PartitionFirstAsPivot(8743564)
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
        /// not stable, use PartitionTwoWay
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="comparer"></param>
        public static void SortTwoWay<T>(T[] array, IComparer<T> comparer = null)
        {
            comparer = comparer ?? Comparer<T>.Default;
            int len = array.Length;
            SortCore(array, 0, len - 1, comparer, PartitionTwoWay);
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
            return i;
        }

        /// <summary>
        /// use first element as pivot. partition the array range [p,q] range to [p,r-1],[r],[r+1,q]
        /// [p+1,i) lt or equal to pivot, (j,q] great than or equal to pivot. finally j = i -1
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <param name="comparer"></param>
        /// <returns>r</returns>
        private static int PartitionTwoWay<T>(T[] array, int p, int q, IComparer<T> comparer)
        {
            T pivot = array[p];
            int i = p + 1, j = q;
            while (i <= j)
            {
                while (i <= j && comparer.Compare(array[i], pivot) <= 0)
                {
                    i++;
                }
                while (i <= j && comparer.Compare(array[j], pivot) >= 0)
                {
                    j--;
                }
                if (i < j)
                {
                    Swap(array, i, j);
                }
            }
            Swap(array, p, j);
            return j;
        }

        private static void SortCore<T>(T[] array, int p, int q, IComparer<T> comparer, Func<T[], int, int, IComparer<T>, int> partitioner)
        {
            if (p >= q)
            {
                return;
            }
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
