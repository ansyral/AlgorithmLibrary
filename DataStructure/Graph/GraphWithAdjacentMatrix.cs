namespace XuanLibrary.DataStructure.Graph
{
    using System;
    using System.Collections.Generic;

    public class GraphWithAdjacentMatrix<T> : IGraph<T>
    {
        private int[,] _matrix;

        public List<Vertex<T>> Vertices { get; }

        public List<Edge<T>> EdgesFrom(Vertex<T> v)
        {
            throw new NotImplementedException();
        }

        public List<Edge<T>> EdgesTo(Vertex<T> v)
        {
            throw new NotImplementedException();
        }
    }
}
