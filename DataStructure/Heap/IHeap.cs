namespace XuanLibrary.DataStructure.Heap
{
    using System.Collections.Generic;

    public interface IHeap<T>
    {
        int HeapSize { get; }
        int Length { get; }
        IComparer<T> Comparer { get; }
        T this[int index] { get; set; }
        T Peek();
        T Pop();
        void Push(T value);
        T[] Sort();
    }
}
