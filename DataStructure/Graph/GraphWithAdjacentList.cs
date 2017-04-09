namespace XuanLibrary.DataStructure.Graph
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GraphWithAdjacentList<T> : IGraph<T>
    {
        private List<Edge<T>> _edges;
        private Dictionary<Vertex<T>, List<Edge<T>>> _indexOnFrom = new Dictionary<Vertex<T>, List<Edge<T>>>();
        private Dictionary<Vertex<T>, List<Edge<T>>> _indexOnTo = new Dictionary<Vertex<T>, List<Edge<T>>>();

        public List<Vertex<T>> Vertices { get; }

        public bool IsDirected { get; }

        public List<Edge<T>> EdgesFrom(Vertex<T> v)
        {
            if (v == null)
            {
                throw new ArgumentNullException(nameof(v));
            }
            List<Edge<T>> adjacent;
            if (_indexOnFrom.TryGetValue(v, out adjacent))
            {
                return adjacent;
            }
            return new List<Edge<T>>();
        }

        public List<Edge<T>> EdgesTo(Vertex<T> v)
        {
            if (v == null)
            {
                throw new ArgumentNullException(nameof(v));
            }
            List<Edge<T>> adjacent;
            if (_indexOnTo.TryGetValue(v, out adjacent))
            {
                return adjacent;
            }
            return new List<Edge<T>>();
        }

        public IGraph<T> Transform()
        {
            var mapper = Vertices.ToDictionary(v => v, v => v.Clone());
            var edges = (from e in _edges
                         select new Edge<T> { From = mapper[e.To], To = mapper[e.From], Weight = e.Weight }).ToList();
            return new GraphWithAdjacentList<T>((from v in Vertices
                                                 select mapper[v]).ToList(), edges, IsDirected);
        }

        public GraphWithAdjacentList(bool isDirected)
        {
            Vertices = new List<Vertex<T>>();
            _edges = new List<Edge<T>>();
            IsDirected = isDirected;
        }

        public GraphWithAdjacentList(List<Vertex<T>> vertices, List<Edge<T>> edges, bool isDirected)
        {
            if (vertices == null)
            {
                throw new ArgumentNullException(nameof(vertices));
            }
            if (edges == null)
            {
                throw new ArgumentNullException(nameof(edges));
            }
            Vertices = new List<Vertex<T>>(vertices);
            _edges = new List<Edge<T>>(edges);
            IsDirected = isDirected;
            BuildIndex();
        }

        public void AddVertex(Vertex<T> v)
        {
            if (v == null)
            {
                throw new ArgumentNullException(nameof(v));
            }
            Vertices.Add(v);
        }

        public void AddEdge(Edge<T> e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }
            _edges.Add(e);
            BuildIndexCore(e);
        }

        private void BuildIndex()
        {
            foreach (var e in _edges)
            {
                BuildIndexCore(e);
            }
        }

        private void BuildIndexCore(Edge<T> e)
        {
            BuildFromIndex(e);
            BuildToIndex(e);
        }

        private void BuildFromIndex(Edge<T> e)
        {
            List<Edge<T>> adjacent;
            if (!_indexOnFrom.TryGetValue(e.From, out adjacent))
            {
                _indexOnFrom[e.From] = adjacent = new List<Edge<T>>();
            }
            adjacent.Add(e);
        }

        private void BuildToIndex(Edge<T> e)
        {
            List<Edge<T>> adjacent;
            if (!_indexOnTo.TryGetValue(e.To, out adjacent))
            {
                _indexOnTo[e.To] = adjacent = new List<Edge<T>>();
            }
            adjacent.Add(e);
        }
    }
}
