namespace XuanLibrary.DataStructure.Graph
{
    public class Vertex<T>
    {
        public T Data { get; set; }

        public Vertex(T d)
        {
            Data = d;
        }
    }
}
