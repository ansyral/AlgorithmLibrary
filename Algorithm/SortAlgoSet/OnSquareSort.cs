namespace XuanLibrary.Algorithm.SortAlgoSet
{
    using System.Collections.Generic;

    public static class OnSquareSort
    {
        /// <summary>
        /// not stable sort(swap elements, hard to keep stable, e.g. 287183564), each iteration gets a minimum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="comparer"></param>
        public static void SelectionSort<T>(T[] array, IComparer<T> comparer = null)
        {
            comparer = comparer ?? Comparer<T>.Default;
            for (int i = 0; i < array.Length - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (comparer.Compare(array[j], array[min]) < 0)
                    {
                        min = j;
                    }
                }
                if (min != i)
                {
                    Swap(array, i, min);
                }
            }
        }

        /// <summary>
        /// stable sort
        /// array range[0,i) is sorted
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="comparer"></param>
        public static void InsertSort<T>(T[] array, IComparer<T> comparer = null)
        {
            comparer = comparer ?? Comparer<T>.Default;
            for (int i = 1; i < array.Length; i++)
            {
                T tmp = array[i];
                int j = i - 1;
                while (j >= 0 && comparer.Compare(array[j], tmp) > 0)
                {
                    array[j + 1] = array[j];
                    j--;
                }
                array[j + 1] = tmp;
            }
        }

        /// <summary>
        /// stable sort(only nearby element would swap), each iteration gets a maximum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="comparer"></param>
        public static void BubbleSort<T>(T[] array, IComparer<T> comparer = null)
        {
            comparer = comparer ?? Comparer<T>.Default;
            bool sorted;
            for (int times = 0; times < array.Length; times++)
            {
                sorted = true;
                for (int i = 1; i < array.Length - times; i++)
                {
                    if (comparer.Compare(array[i], array[i - 1]) < 0)
                    {
                        Swap(array, i, i - 1);
                        sorted = false;
                    }
                }
                if (sorted)
                {
                    break;
                }
            }
        }

        private static void Swap<T>(T[] array, int i, int j)
        {
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}
