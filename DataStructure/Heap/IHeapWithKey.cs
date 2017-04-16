namespace XuanLibrary.DataStructure.Heap
{
    public interface IHeapWithKey<TKey, TValue> : IHeap<TValue>
    {
        void Update(TKey key, TValue value);
    }
}
