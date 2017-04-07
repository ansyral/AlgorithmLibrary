namespace XuanLibrary.Algorithm
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;

    using XuanLibrary.DataStructure.Graph;

    public static class GraphAlgoSet
    {
        // Check circle, topological sort
        public static ImmutableList<Vertex<T>> DFS<T>(IGraph<T> g, bool reverseVisit = false)
        {
            var context = new TraverseContext<T>(g);
            foreach (var v in g.Vertices)
            {
                if (context.GetVisitStatus(v) == VisitStatus.White)
                {
                    if (!DFS(g, v, context, reverseVisit))
                    {
                        return null;
                    }
                }
            }
            return context.TraverseResult.ToImmutableList();
        }

        // shortest path for unweighted graph
        public static ImmutableList<Vertex<T>> BFS<T>(IGraph<T> g)
        {
            var context = new TraverseContext<T>(g);
            var start = g.Vertices[0];
            Queue<Vertex<T>> queue = new Queue<Vertex<T>>();
            queue.Enqueue(start);
            context.SetVisitStatus(start, VisitStatus.Grey);
            while (queue.Count > 0)
            {
                var cur = queue.Dequeue();
                context.Visit(cur);
                foreach (var edge in g.EdgesFrom(cur))
                {
                    var child = edge.To;
                    if (context.GetVisitStatus(child) == VisitStatus.White)
                    {
                        queue.Enqueue(child);
                        context.SetVisitStatus(child, VisitStatus.Grey);
                        context.SetParent(child, cur);
                    }
                }
                context.SetVisitStatus(cur, VisitStatus.Black);
            }
            return context.TraverseResult.ToImmutableList();
        }

        public static ImmutableList<Vertex<T>> TopologicalSort<T>(IGraph<T> g)
        {
            if (!g.IsDirected)
            {
                throw new InvalidOperationException("Only directed graph support topological sort.");
            }
            var result = new List<Vertex<T>>();
            var queue = new Queue<Vertex<T>>();
            var degree = new Dictionary<Vertex<T>, int>();
            foreach (var v in g.Vertices)
            {
                int count = g.EdgesTo(v).Count;
                if (count == 0)
                {
                    queue.Enqueue(v);
                }
                degree[v] = count;
            }
            while (queue.Count > 0)
            {
                var cur = queue.Dequeue();
                result.Add(cur);
                foreach (var e in g.EdgesFrom(cur))
                {
                    degree[e.To]--;
                    if (degree[e.To] == 0)
                    {
                        queue.Enqueue(e.To);
                    }
                }
            }
            if (result.Count != g.Vertices.Count)
            {
                Console.WriteLine("Cycle exists in the graph");
                return null;
            }
            return result.ToImmutableList();
        }

        public static ImmutableList<Vertex<T>> TopologicalSortWithDFS<T>(IGraph<T> g)
        {
            return DFS(g, true);
        }

        private static bool DFS<T>(IGraph<T> g, Vertex<T> cur, TraverseContext<T> context, bool reverseVisit)
        {
            if (!reverseVisit)
            {
                context.Visit(cur);
            }
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
                        if (!DFS(g, child, context, reverseVisit))
                        {
                            return false;
                        }
                    }
                }
            }
            context.SetVisitStatus(cur, VisitStatus.Black);
            if (reverseVisit)
            {
                context.Visit(cur, reverse: true);
            }
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

        public void Visit(Vertex<T> v, bool reverse = false)
        {
            if (reverse)
            {
                TraverseResult.Insert(0, v);
            }
            else
            {
                TraverseResult.Add(v);
            }
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

        public int GetIndex(Vertex<T> v)
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
