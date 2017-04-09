namespace XuanLibrary.Algorithm
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;

    using XuanLibrary.DataStructure.Graph;

    public static class GraphAlgoSet
    {
        // Check circle, topological sort
        public static ImmutableList<Vertex<T>> DFS<T>(IGraph<T> g, bool reverseVisit = false, bool breakWhenCyclic = true)
        {
            var context = new TraverseContext<T>(g);
            foreach (var v in g.Vertices)
            {
                if (context.GetVisitStatus(v) == VisitStatus.White)
                {
                    if (!DFS(g, v, context, reverseVisit, breakWhenCyclic) && breakWhenCyclic)
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
            if (!g.IsDirected)
            {
                throw new InvalidOperationException("Only directed graph support topological sort.");
            }
            return DFS(g, reverseVisit: true);
        }

        public static ImmutableList<ImmutableList<Vertex<T>>> GetStronglyConnectedComponents<T>(IGraph<T> g)
        {
            if (!g.IsDirected)
            {
                throw new InvalidOperationException("Only directed graph need to get *strongly* connected component.");
            }

            // get finish time by DFS(G)
            var context = new TraverseContext<T>(g);
            foreach (var v in g.Vertices)
            {
                if (context.GetVisitStatus(v) == VisitStatus.White)
                {
                    DFS(g, v, context, false, false);
                }
            }
            var sorted = context.GetVerticesSortedByFinishTime(false);

            var transformed = g.Transform();
            var tcontext = new TraverseContext<T>(transformed);

            // DFS(G^T) by finish time descending order.
            var res = new List<ImmutableList<Vertex<T>>>();
            int count = 0;
            foreach (var v in sorted)
            {
                var tv = transformed.Vertices[v.Index];
                if (tcontext.GetVisitStatus(tv) == VisitStatus.White)
                {
                    DFS(transformed, tv, tcontext, false, false);
                    res.Add(tcontext.TraverseResult.Skip(count).ToImmutableList());
                    count = tcontext.TraverseResult.Count;
                }
            }
            return res.ToImmutableList();
        }

        /// <summary>
        /// A general(no negative cycle) algo to get the shortest path from a node `s`.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="g"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static ImmutableDictionary<Vertex<T>, long> BellmanFord<T>(IGraph<T> g, Vertex<T> s)
        {
            var res = new Dictionary<Vertex<T>, long>();
            foreach (var v in g.Vertices)
            {
                res[v] = int.MaxValue;
            }
            res[s] = 0;
            var edges = (from v in g.Vertices
                         from e in g.EdgesFrom(v)
                         select e).ToList();
            for (int i = 0; i < g.Vertices.Count - 1; i++)
            {
                foreach (var e in edges)
                {
                    Relax(res, e);
                }
            }

            // check negative cycle
            foreach (var e in edges)
            {
                if (res[e.To] > res[e.From] + e.Weight)
                {
                    Console.WriteLine("Negative cycle existed.");
                    return null;
                }
            }
            return res.ToImmutableDictionary();
        }

        /// <summary>
        /// algo(no cycle, could allow for negative weight) to get shortest path from a node `s`
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="g"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static ImmutableDictionary<Vertex<T>, long> SingleSourceShortestPathWithTopologicalSort<T>(IGraph<T> g, Vertex<T> s)
        {
            if (!g.IsDirected)
            {
                throw new InvalidOperationException("Only directed graph supports this algorithm");
            }
            var sorted = TopologicalSort(g);
            if (sorted == null)
            {
                throw new InvalidOperationException("Only acyclic graph supports this algorithm");
            }

            // initialize
            var res = new Dictionary<Vertex<T>, long>();
            foreach (var v in sorted)
            {
                res[v] = int.MaxValue;
            }
            res[s] = 0;

            foreach (var v in sorted)
            {
                foreach (var e in g.EdgesFrom(v))
                {
                    Relax(res, e);
                }
            }

            return res.ToImmutableDictionary();
        }

        /// <summary>
        /// Simple algo(only allow for positive weight, but could be cyclic) to get shortest path from a node `s`
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="g"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static ImmutableDictionary<Vertex<T>, long> Dijkstra<T>(IGraph<T> g, Vertex<T> s)
        {
            // todo: wait for data structure [minimum priority queue]
            throw new NotImplementedException("wait for data structure [minimum priority queue]");
        }

        /// <summary>
        /// A generic(no negative cycle) algo to get the shortest path from each pair of vertice in the graph.
        /// it uses Dynamic Programming. D[ij][k] means the shortest path from i to j with intermediate vertice between 0~k-1. D[ij][0] means no intermediate vertice.
        /// it only works for graph that is represented as matrix.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="g"></param>
        /// <returns></returns>
        public static long[,] FloydWarshall<T>(GraphWithAdjacentMatrix<T> g)
        {
            int len = g.Vertices.Count;
            var D = new long[len, len];
            Array.Copy(g.Matrix, D, len * len);
            for (int k = 1; k <= len; k++)
            {
                for (int i = 0; i < len; i++)
                {
                    for (int j = 0; j < len; j++)
                    {
                        long throughK = D[i, k - 1] + D[k - 1, j];
                        if (throughK < D[i, j])
                        {
                            D[i, j] = throughK;
                        }
                    }
                }
            }
            return D;
        }

        private static bool DFS<T>(IGraph<T> g, Vertex<T> cur, TraverseContext<T> context, bool reverseVisit, bool breakWhenCyclic)
        {
            if (!reverseVisit)
            {
                context.Visit(cur);
            }
            context.SetVisitStatus(cur, VisitStatus.Grey);
            context.TrackingTime++;
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
                    var temp = context.GetParent(child);
                    context.SetParent(child, cur);
                    if (childStatus == VisitStatus.Grey)
                    {
                        Console.WriteLine("Cycle checked:");
                        PrintPath(cur, cur, context);
                        if (breakWhenCyclic)
                        {
                            return false;
                        }
                        else
                        {
                            context.SetParent(child, temp);
                        }
                    }
                    else
                    {
                        bool res = DFS(g, child, context, reverseVisit, breakWhenCyclic);
                        if (!res && breakWhenCyclic)
                        {
                            return false;
                        }
                    }
                }
            }
            context.SetVisitStatus(cur, VisitStatus.Black);
            context.TrackingTime++;
            context.SetFinishTime(cur);
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

        // use long incase int.MaxValue + positive weight would overflow
        private static void Relax<T>(Dictionary<Vertex<T>, long> res, Edge<T> e)
        {
            long cur = res[e.From] + e.Weight;
            if (cur < res[e.To])
            {
                res[e.To] = cur;
            }
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
        private int[] _finishTime;

        public int VertexCount { get; }

        public List<Vertex<T>> TraverseResult { get; }

        public int TrackingTime { get; set; }

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
            _finishTime = new int[count];
            TraverseResult = new List<Vertex<T>>();
            TrackingTime = 0;
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

        public int GetFinishTime(Vertex<T> v)
        {
            int i = GetIndex(v);
            return _finishTime[i];
        }

        public void SetFinishTime(Vertex<T> v)
        {
            int i = GetIndex(v);
            _finishTime[i] = TrackingTime;
        }

        public ImmutableList<VertexWithIndex<T>> GetVerticesSortedByFinishTime(bool ascending)
        {
            var reversedIndex = _index.ToDictionary(p => p.Value, p => p.Key);
            var pairs = _finishTime.Select((x, i) => new KeyValuePair<int, VertexWithIndex<T>>(x, new VertexWithIndex<T>(i, reversedIndex[i])));
            if (ascending)
            {
                return pairs.OrderBy(p => p.Key)
                    .Select(p => p.Value)
                    .ToImmutableList();
            }
            else
            {
                return pairs.OrderByDescending(p => p.Key)
                    .Select(p => p.Value)
                    .ToImmutableList();
            }
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

    internal class VertexWithIndex<T>
    {
        public Vertex<T> Vertex { get; set; }

        public int Index { get; set; }

        public VertexWithIndex(int index, Vertex<T> v)
        {
            Index = index;
            Vertex = v;
        }
    }
}
