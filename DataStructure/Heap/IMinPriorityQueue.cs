namespace XuanLibrary.DataStructure.Heap
{
    public interface IMinPriorityQueue<T>
    {
        int Count { get; }
        T Min();
        T ExtractMin();
        void Insert(T value);
    }

    public interface IMinPriorityQueue<TKey, TValue> : IMinPriorityQueue<TValue>
    {
        void DecreasePriority(TKey key, TValue value);
    }
}
