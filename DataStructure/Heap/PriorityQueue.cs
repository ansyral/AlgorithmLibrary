namespace XuanLibrary.DataStructure.Heap
{
    using System.Collections.Generic;

    public class PriorityQueue<T>
    {
        private Heap<T> _heap;

        public PriorityQueue(T[] array, IComparer<T> comparer = null, bool isMax = false)
        {
            if (isMax)
            {
                _heap = new MaxHeap<T>(array, comparer);
            }
            else
            {
                _heap = new MinHeap<T>(array, comparer);
            }
        }

        public T PriorityZero()
        {
            return _heap[0];
        }

        public T ExtractPriorityZero()
        {
            //to-do
            throw new System.NotImplementedException();
        }
    }
}
