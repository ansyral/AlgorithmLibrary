namespace XuanLibrary.DataStructure.DisjointSet
{
    public interface IDisjointSet<T>
    {
        void MakeSet(T x);

        void Union(T x, T y);

        T FindSet(T x);
    }
}
