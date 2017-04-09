namespace XuanLibrary.DataStructure.Graph
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GraphWithAdjacentMatrix<T> : IGraph<T>
    {
        private int[,] _edges;

        public List<Vertex<T>> Vertices { get; }

        public bool IsDirected { get; }

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

        public IGraph<T> Transform()
        {
            var vertices = Vertices.Select(v => v.Clone()).ToList();
            int len = Vertices.Count;
            var edges = new int[len, len];
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    edges[i, j] = _edges[j, i];
                }
            }
            return new GraphWithAdjacentMatrix<T>(vertices, edges, IsDirected);
        }

        public GraphWithAdjacentMatrix(List<Vertex<T>> vertices, int[,] edges, bool isDirected)
        {
            if (vertices == null)
            {
                throw new ArgumentNullException(nameof(vertices));
            }
            if (edges == null)
            {
                throw new ArgumentNullException(nameof(edges));
            }
            int len = vertices.Count;
            if (len != edges.GetLength(0) || len != edges.GetLength(1))
            {
                throw new ArgumentException("edges length doesn't match vertex count.");
            }
            Vertices = new List<Vertex<T>>(vertices);
            _edges = new int[len, len];
            Array.Copy(edges, _edges, len * len);
            IsDirected = isDirected;
        }
    }
}
