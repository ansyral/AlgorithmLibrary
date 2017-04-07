namespace XuanLibrary.Algorithm
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;

    using XuanLibrary.DataStructure.Graph;

    public static class GraphAlgoSet
    {
        public static ImmutableList<Vertex<T>> DFS<T>(IGraph<T> g)
        {
            var context = new TraverseContext<T>(g);
            foreach (var v in g.Vertices)
            {
                if (context.GetVisitStatus(v) == VisitStatus.White)
                {
                    if (!DFS(g, v, context))
                    {
                        return null;
                    }
                }
            }
            return context.TraverseResult.ToImmutableList();
        }

        private static bool DFS<T>(IGraph<T> g, Vertex<T> cur, TraverseContext<T> context)
        {
            context.Visit(cur);
            context.SetVisitStatus(cur, VisitStatus.Grey);
            var parent = context.GetParent(cur);
            foreach (var edge in g.EdgesFrom(cur))
            {
                var child = edge.To;
                if (!g.IsDirected && child == parent)
                {
                    continue;
                }
                var childStatus = context.GetVisitStatus(child);
                if (childStatus != VisitStatus.Black)
                {
                    context.SetParent(child, cur);
                    if (childStatus == VisitStatus.Grey)
                    {
                        Console.WriteLine("Cycle checked:");
                        PrintPath(cur, cur, context);
                        return false;
                    }
                    else
                    {
                        if (!DFS(g, child, context))
                        {
                            return false;
                        }
                    }
                }
            }
            context.SetVisitStatus(cur, VisitStatus.Black);
            return true;
        }

        private static void PrintPath<T>(Vertex<T> end, Vertex<T> start, TraverseContext<T> context)
        {
            if (end == null)
            {
                throw new ArgumentNullException(nameof(end));
            }

            var path = new List<T>();
            Vertex<T> cur = end;
            do
            {
                path.Insert(0, cur.Data);
                cur = context.GetParent(cur);
            } while (cur != start && cur != null);
            if (cur != null)
            {
                path.Insert(0, cur.Data);
            }

            Console.WriteLine($"Path: {string.Join("->", path)}");
        }
    }

    internal enum VisitStatus
    {
        White,
        Grey,
        Black,
    }

    internal class TraverseContext<T>
    {
        private Dictionary<Vertex<T>, int> _index;
        private VisitStatus[] _visited;
        private Vertex<T>[] _parent;

        public int VertexCount { get; }

        public List<Vertex<T>> TraverseResult { get; }

        public TraverseContext(IGraph<T> g)
        {
            int count = g.Vertices.Count;
            VertexCount = count;
            _index = new Dictionary<Vertex<T>, int>();
            for (int i = 0; i < count; i++)
            {
                _index[g.Vertices[i]] = i;
            }
            _visited = new VisitStatus[count];
            _parent = new Vertex<T>[count];
            TraverseResult = new List<Vertex<T>>();
        }

        public void Visit(Vertex<T> v)
        {
            TraverseResult.Add(v);
        }

        public VisitStatus GetVisitStatus(Vertex<T> v)
        {
            int i = GetIndex(v);
            return _visited[i];
        }

        public void SetVisitStatus(Vertex<T> v, VisitStatus s)
        {
            int i = GetIndex(v);
            _visited[i] = s;
        }

        public Vertex<T> GetParent(Vertex<T> v)
        {
            int i = GetIndex(v);
            return _parent[i];
        }

        public void SetParent(Vertex<T> v, Vertex<T> p)
        {
            int i = GetIndex(v);
            _parent[i] = p;
        }

        private int GetIndex(Vertex<T> v)
        {
            if (v == null)
            {
                throw new ArgumentNullException(nameof(v));
            }
            int i;
            if (!_index.TryGetValue(v, out i))
            {
                throw new ArgumentException($"vertex {v} doesn't exist in the graph.");
            }
            return i;
        }
    }
}
