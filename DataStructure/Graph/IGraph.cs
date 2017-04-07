namespace XuanLibrary.DataStructure.Graph
{
    using System.Collections.Generic;

    public interface IGraph<T>
    {
        List<Vertex<T>> Vertices { get; }

        List<Edge<T>> EdgesFrom(Vertex<T> v);

        List<Edge<T>> EdgesTo(Vertex<T> v);

        bool IsDirected { get; }
    }
}
