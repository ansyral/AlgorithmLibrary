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

        public T this[int index]
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
            Swap(0, HeapSize - 1);
            HeapSize--;
            Heapify(0);
            return ele;
        }

        public void Push(T value)
        {

        }

        public void Update(int index, T v)
        {
            if (index >= HeapSize)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            int compare = Comparer.Compare(_array[index], v);
            if (compare == 0)
            {
                return;
            }
            if (IsMax && compare > 0 || !IsMax && compare < 0)
            {
                throw new InvalidOperationException("invaid update!");
            }
            int parent = Parent(index);
            while (parent >= 0)
            {

            }
        }

        public T[] Sort()
        {
            var cloned = Clone();
            for (int i = cloned.Length - 1; i > 0; i--)
            {
                cloned.Swap(i, 0);
                cloned.HeapSize--;
                cloned.Heapify(0);
            }
            return cloned._array;
        }

        public Heap<T> Clone()
        {
            var cloned = (Heap<T>)this.MemberwiseClone();
            cloned._array = new T[Length];
            Array.Copy(_array, cloned._array, Length);
            return cloned;
        }

        /// <summary>
        /// the left child and right child are both heap, adjust root to make it a heap,too.
        /// </summary>
        /// <param name="index">the element's index</param>
        public void Heapify(int index)
        {
            int left = LeftChild(index);
            int right = RightChild(index);
            if (left < 0 && right < 0)
            {
                return;
            }
            int dest = GetMatched(left, index);
            if (right >= 0)
            {
                dest = GetMatched(right, dest);
            }
            if (dest != index)
            {
                Swap(dest, index);
                Heapify(dest);
            }
        }

        /// <summary>
        /// get the parent element's index
        /// </summary>
        /// <param name="index">an element's index</param>
        /// <returns>-1 if parent doesn't exist</returns>
        public int Parent(int index)
        {
            if (index == 0)
            {
                return -1;
            }
            return index / 2;
        }

        /// <summary>
        /// get left child's element's index
        /// </summary>
        /// <param name="index">an element's index</param>
        /// <returns>-1 if left child doesn't exist</returns>
        public int LeftChild(int index)
        {
            var l = index * 2;
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
        public int RightChild(int index)
        {
            var r = index * 2 + 1;
            if (r >= HeapSize)
            {
                return -1;
            }
            return r;
        }

        private void Swap(int a, int b)
        {
            T temp = _array[a];
            _array[a] = _array[b];
            _array[b] = temp;
        }

        private int GetMatched(int a, int b)
        {
            int dest = b;
            int c = Comparer.Compare(_array[a], _array[b]);
            if (IsMax && c > 0 || !IsMax && c < 0)
            {
                dest = a;
            }
            return dest;
        }
    }
}
