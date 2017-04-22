namespace XuanLibrary.DataStructure.Heap
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using XuanLibrary.DataStructure.Common;

    // refered https://ezekiel.encs.vancouver.wsu.edu/~cs223/projects/dijkstra-priority-queue/dijkstra-priority-queue.pdf
    public class Heap<TKey, TValue> : Heap<TValue>, IHeapWithKey<TKey, TValue> where TValue : IHasKey<TKey>
    {
        private Dictionary<TKey, int> _keyIndex;

        public override TValue this[int index]
        {
            get
            {
                return base[index];
            }
            set
            {
                if (index >= HeapSize)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                base[index] = value;
                if (value != null)
                {
                    _keyIndex[value.Key] = index;
                }
            }
        }

        internal Heap(TValue[] array, IComparer<TValue> comparer = null, bool isMaxHeap = false, int? capacity = null) : base(array, comparer, isMaxHeap, capacity)
        {
            _keyIndex = array.Select((a, i) => Tuple.Create(a.Key, i)).ToDictionary(t => t.Item1, t => t.Item2);
        }

        #region IHeapWithKey members
        public void Update(TKey key, TValue value)
        {
            int index;
            if (!_keyIndex.TryGetValue(key, out index) || !key.Equals(this[index].Key))
            {
                throw new ArgumentException($"Key {key} doesn't exist in the heap.");
            }
            Update(index, value);
        }
        #endregion
    }
}
