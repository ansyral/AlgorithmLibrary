namespace XuanLibrary.DataStructure.Heap
{
    using System.Collections.Generic;

    public static class HeapFactory
    {
        public static IHeap<T> CreateMax<T>(T[] array, IComparer<T> comparer = null, int? capacity = null)
        {
            return Create<T>(array, comparer, true, capacity);
        }

        public static IHeapWithKey<TKey, TValue> CreateMax<TKey, TValue>(TValue[] array, IComparer<TValue> comparer = null, int? capacity = null)
            where TValue : IHasKey<TKey>
        {
            return Create<TKey, TValue>(array, comparer, true, capacity);
        }

        public static IHeap<T> CreateMin<T>(T[] array, IComparer<T> comparer = null, int? capacity = null)
        {
            return Create(array, comparer, false, capacity);
        }

        public static IHeapWithKey<TKey, TValue> CreateMin<TKey, TValue>(TValue[] array, IComparer<TValue> comparer = null, int? capacity = null)
            where TValue : IHasKey<TKey>
        {
            return Create<TKey, TValue>(array, comparer, false, capacity);
        }

        private static IHeap<T> Create<T>(T[] array, IComparer<T> comparer = null, bool isMaxHeap = false, int? capacity = null)
        {
            var instance = new Heap<T>(array, comparer, isMaxHeap, capacity);
            instance.BuildHeap();
            return instance;
        }

        private static IHeapWithKey<TKey, TValue> Create<TKey, TValue>(TValue[] array, IComparer<TValue> comparer = null, bool isMaxHeap = false, int? capacity = null)
            where TValue : IHasKey<TKey>
        {
            var instance = new Heap<TKey, TValue>(array, comparer, isMaxHeap, capacity);
            instance.BuildHeap();
            return instance;
        }
    }
}
