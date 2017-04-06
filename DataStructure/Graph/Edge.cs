namespace XuanLibrary.DataStructure.Graph
{
    public class Edge<T>
    {
        public int Weight { get; set; }

        public Vertex<T> From { get; set; }

        public Vertex<T> To { get; set; }

        public bool BiDirectional { get; set; }
    }
}
