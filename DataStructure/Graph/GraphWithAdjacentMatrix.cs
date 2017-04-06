namespace XuanLibrary.DataStructure.Graph
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GraphWithAdjacentMatrix<T> : IGraph<T>
    {
        private int[,] _edges;

        public List<Vertex<T>> Vertices { get; }

        public List<Edge<T>> EdgesFrom(Vertex<T> v)
        {
            int index = Vertices.IndexOf(v);
            if (index < 0)
            {
                throw new InvalidOperationException($"vertex {v} isn't in the graph.");
            }
            return (from i in Enumerable.Range(0, Vertices.Count)
                    where i != index && _edges[index, i] != int.MaxValue
                    select new Edge<T>() { From = v, To = Vertices[i], Weight = _edges[index, i] }).ToList();
        }

        public List<Edge<T>> EdgesTo(Vertex<T> v)
        {
            int index = Vertices.IndexOf(v);
            if (index < 0)
            {
                throw new InvalidOperationException($"vertex {v} isn't in the graph.");
            }
            return (from i in Enumerable.Range(0, Vertices.Count)
                    where i != index && _edges[i, index] != int.MaxValue
                    select new Edge<T>() { From = Vertices[i], To = v, Weight = _edges[i, index] }).ToList();
        }

        public GraphWithAdjacentMatrix(List<Vertex<T>> vertices, int[,] edges)
        {
            if (vertices == null)
            {
                throw new ArgumentNullException(nameof(vertices));
            }
            if (edges == null)
            {
                throw new ArgumentNullException(nameof(edges));
            }
            int len = edges.GetLength(0);
            if (len != edges.GetLength(1) || len != vertices.Count)
            {
                throw new ArgumentException("edges data is illegal.");
            }
            Vertices = new List<Vertex<T>>(vertices);
            _edges = new int[len, len];
            Array.Copy(edges, _edges, len);
        }
    }
}
