namespace XuanLibrary.DataStructure.Heap
{
    public interface IMaxPriorityQueue<T>
    {
        int Count { get; }
        T Max();
        T ExtractMax();
        void Insert(T value);
    }

    public interface IMaxPriorityQueue<TKey, TValue> : IMaxPriorityQueue<TValue>
    {
        void IncreasePriority(TKey key, TValue value);
    }

}
