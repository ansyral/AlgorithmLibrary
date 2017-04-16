namespace XuanLibrary.DataStructure.Heap
{
    using System;
    using System.Collections.Generic;

    public class Heap<T> : IHeap<T>
    {
        private T[] _array;

        public bool IsMax { get; }

        internal Heap(T[] array, IComparer<T> comparer = null, bool isMaxHeap = false, int? capacity = null)
        {
            Comparer = comparer ?? Comparer<T>.Default;
            IsMax = isMaxHeap;
            HeapSize = array.Length;
            Length = capacity ?? HeapSize;
            if (Length < HeapSize)
            {
                Length = HeapSize;
            }
            _array = new T[Length];
            Array.Copy(array, _array, HeapSize);
        }

        #region IHeap members
        /// <summary>
        /// the elements count in the heap
        /// </summary>
        public int HeapSize { get; private set; }

        /// <summary>
        /// the array length
        /// </summary>
        public int Length { get; private set; }

        public IComparer<T> Comparer { get; }

        public virtual T this[int index]
        {
            get
            {
                if (index >= HeapSize)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                return _array[index];
            }
            set
            {
                if (index >= HeapSize)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                _array[index] = value;
            }
        }

        public T Peek()
        {
            if (HeapSize == 0)
            {
                throw new InvalidOperationException("Heap is empty!");
            }
            return _array[0];
        }

        public T Pop()
        {
            var ele = _array[0];
            this[0] = _array[HeapSize - 1];
            HeapSize--;
            HeapifyDown(0);
            return ele;
        }

        public void Push(T value)
        {
            if (HeapSize == Length)
            {
                var temp = new T[2 * Length];
                Array.Copy(_array, temp, Length);
                _array = temp;
                Length = 2 * Length;
            }
            this[HeapSize++] = value;
            Update(HeapSize - 1, value);
        }

        public T[] Sort()
        {
            var cloned = Clone();
            for (int i = cloned.Length - 1; i > 0; i--)
            {
                cloned.Swap(i, 0);
                cloned.HeapSize--;
                cloned.HeapifyDown(0);
            }
            return cloned._array;
        }
        #endregion

        public Heap<T> Clone()
        {
            var cloned = (Heap<T>)this.MemberwiseClone();
            cloned._array = new T[Length];
            Array.Copy(_array, cloned._array, Length);
            return cloned;
        }

        protected void Update(int index, T v)
        {
            if (index >= HeapSize)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            int compare = Compare(_array[index], v);
            if (compare > 0)
            {
                throw new InvalidOperationException("invaid update!");
            }
            this[index] = v;
            HeapifyUp(index);
        }

        /// <summary>
        /// build a heap according to the comparer
        /// </summary>
        internal void BuildHeap()
        {
            for (int i = HeapSize / 2 - 1; i >= 0; i--)
            {
                HeapifyDown(i);
            }
        }

        /// <summary>
        /// the left child and right child are both heap, adjust root down to make it a heap,too.
        /// </summary>
        /// <param name="index">the element's index</param>
        private void HeapifyDown(int index)
        {
            T cur = _array[index];
            int iter = index;
            while (iter >= 0)
            {
                int left = LeftChild(iter);
                int right = RightChild(iter);
                int dest = GetMatched(left, right);
                if (dest >= 0 && Compare(cur, _array[dest]) < 0)
                {
                    this[iter] = _array[dest];
                    iter = dest;
                }
                else
                {
                    break;
                }
            }
            if (iter != index)
            {
                this[iter] = cur;
            }
        }

        /// <summary>
        /// the subtree rooted in index is heap, adjust root up to make it a heap
        /// </summary>
        /// <param name="index"></param>
        private void HeapifyUp(int index)
        {
            T cur = _array[index];
            int i = index;
            int parent = Parent(i);
            while (parent >= 0 && Compare(_array[parent], cur) < 0)
            {
                this[i] = _array[parent];
                i = parent;
                parent = Parent(i);
            }
            if (i != index)
            {
                this[i] = cur;
            }
        }

        /// <summary>
        /// get the parent element's index
        /// </summary>
        /// <param name="index">an element's index</param>
        /// <returns>-1 if parent doesn't exist</returns>
        private int Parent(int index)
        {
            if (index == 0)
            {
                return -1;
            }
            return (index - 1) / 2;
        }

        /// <summary>
        /// get left child's element's index
        /// </summary>
        /// <param name="index">an element's index</param>
        /// <returns>-1 if left child doesn't exist</returns>
        private int LeftChild(int index)
        {
            var l = index * 2 + 1;
            if (l >= HeapSize)
            {
                return -1;
            }
            return l;
        }

        /// <summary>
        /// get right child's element's index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>-1 if right child doesn't exist</returns>
        private int RightChild(int index)
        {
            var r = index * 2 + 2;
            if (r >= HeapSize)
            {
                return -1;
            }
            return r;
        }

        private void Swap(int a, int b)
        {
            T temp = _array[a];
            this[a] = _array[b];
            this[b] = temp;
        }

        private int GetMatched(int ia, int ib)
        {
            if (ia < 0 && ib < 0)
            {
                return -1;
            }
            else if (ia < 0)
            {
                return ib;
            }
            else if (ib < 0)
            {
                return ia;
            }
            return Compare(_array[ia], _array[ib]) > 0 ? ia : ib;
        }

        private int Compare(T a, T b)
        {
            if (IsMax)
            {
                return Comparer.Compare(a, b);
            }
            else
            {
                return -Comparer.Compare(a, b);
            }
        }
    }
}
