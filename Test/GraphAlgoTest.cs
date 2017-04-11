namespace XuanLibrary.Test
{
    using System.Collections.Generic;
    using System.Linq;

    using XuanLibrary.Algorithm;
    using XuanLibrary.DataStructure.Graph;

    using Xunit;

    public class GraphAlgoTest
    {
        [Fact]
        public void TestDFSOnDirectedCyclicGraphWithAdjacentList()
        {
            var vertices = new List<Vertex<char>> { new Vertex<char>('A'), new Vertex<char>('B'), new Vertex<char>('C'), new Vertex<char>('D'), };
            var graph = new GraphWithAdjacentList<char>(
                vertices,
                new List<Edge<char>>
                {
                    new Edge<char> { From = vertices[0], To = vertices[1], Weight = 1 },
                    new Edge<char> { From = vertices[1], To = vertices[2], Weight = 1 },
                    new Edge<char> { From = vertices[2], To = vertices[0], Weight = 1 },
                    new Edge<char> { From = vertices[3], To = vertices[2], Weight = 1 },
                },
                true);
            var res = GraphAlgoSet.DFS(graph);
            Assert.Equal(res, null);
        }

        [Fact]
        public void TestDFSOnDirectedACyclicGraphWithAdjacentList()
        {
            var vertices = new List<Vertex<char>> { new Vertex<char>('A'), new Vertex<char>('B'), new Vertex<char>('C'), new Vertex<char>('D'), };
            var graph = new GraphWithAdjacentList<char>(
                vertices,
                new List<Edge<char>>
                {
                    new Edge<char> { From = vertices[0], To = vertices[1], Weight = 1 },
                    new Edge<char> { From = vertices[1], To = vertices[2], Weight = 1 },
                    new Edge<char> { From = vertices[0], To = vertices[2], Weight = 1 },
                    new Edge<char> { From = vertices[3], To = vertices[2], Weight = 1 },
                },
                true);
            var res = GraphAlgoSet.DFS(graph);
            Assert.True(res != null);
            Assert.Equal(res.Count, 4);
            Assert.Equal(string.Concat(res.Select(r => r.Data)), "ABCD");
        }

        [Fact]
        public void TestDFSOnUnDirectedCyclicGraphWithAdjacentList()
        {
            var vertices = new List<Vertex<char>> { new Vertex<char>('A'), new Vertex<char>('B'), new Vertex<char>('C'), new Vertex<char>('D'), };
            var graph = new GraphWithAdjacentList<char>(
                vertices,
                new List<Edge<char>>
                {
                    new Edge<char> { From = vertices[0], To = vertices[1], Weight = 1 },
                    new Edge<char> { From = vertices[1], To = vertices[0], Weight = 1 },
                    new Edge<char> { From = vertices[1], To = vertices[2], Weight = 1 },
                    new Edge<char> { From = vertices[2], To = vertices[1], Weight = 1 },
                    new Edge<char> { From = vertices[0], To = vertices[2], Weight = 1 },
                    new Edge<char> { From = vertices[2], To = vertices[0], Weight = 1 },
                    new Edge<char> { From = vertices[3], To = vertices[2], Weight = 1 },
                    new Edge<char> { From = vertices[2], To = vertices[3], Weight = 1 },
                },
                false);
            var res = GraphAlgoSet.DFS(graph);
            Assert.True(res == null);
        }

        [Fact]
        public void TestDFSOnUnDirectedACyclicGraphWithAdjacentList()
        {
            var vertices = new List<Vertex<char>> { new Vertex<char>('A'), new Vertex<char>('B'), new Vertex<char>('C'), new Vertex<char>('D'), };
            var graph = new GraphWithAdjacentList<char>(
                vertices,
                new List<Edge<char>>
                {
                    new Edge<char> { From = vertices[0], To = vertices[1], Weight = 1 },
                    new Edge<char> { From = vertices[1], To = vertices[0], Weight = 1 },
                    new Edge<char> { From = vertices[1], To = vertices[2], Weight = 1 },
                    new Edge<char> { From = vertices[2], To = vertices[1], Weight = 1 },
                    new Edge<char> { From = vertices[3], To = vertices[2], Weight = 1 },
                    new Edge<char> { From = vertices[2], To = vertices[3], Weight = 1 },
                },
                false);
            var res = GraphAlgoSet.DFS(graph);
            Assert.True(res != null);
            Assert.Equal(res.Count, 4);
            Assert.Equal(string.Concat(res.Select(r => r.Data)), "ABCD");
        }

        [Fact]
        public void TestDFSOnDirectedCyclicGraphWithAdjacentMatrix()
        {
            var vertices = new List<Vertex<char>> { new Vertex<char>('A'), new Vertex<char>('B'), new Vertex<char>('C'), new Vertex<char>('D'), };
            var graph = new GraphWithAdjacentMatrix<char>(
                vertices,
                new int[,]
                {
                    { 0, 1, int.MaxValue, int.MaxValue },
                    { int.MaxValue, 0, 1, int.MaxValue },
                    { 1,int.MaxValue, 0, int.MaxValue },
                    { int.MaxValue, int.MaxValue, 1, 0 },
                },
                true);
            var res = GraphAlgoSet.DFS(graph);
            Assert.Equal(res, null);
        }

        [Fact]
        public void TestDFSOnDirectedACyclicGraphWithAdjacentMatrix()
        {
            var vertices = new List<Vertex<char>> { new Vertex<char>('A'), new Vertex<char>('B'), new Vertex<char>('C'), new Vertex<char>('D'), };
            var graph = new GraphWithAdjacentMatrix<char>(
                vertices,
                new int[,]
                {
                    { 0, 1, 1, int.MaxValue },
                    { int.MaxValue, 0, 1, int.MaxValue },
                    { int.MaxValue, int.MaxValue, 0, int.MaxValue },
                    { int.MaxValue, int.MaxValue, 1, 0 },
                },
                true);
            var res = GraphAlgoSet.DFS(graph);
            Assert.True(res != null);
            Assert.Equal(res.Count, 4);
            Assert.Equal(string.Concat(res.Select(r => r.Data)), "ABCD");
        }

        [Fact]
        public void TestDFSOnUnDirectedCyclicGraphWithAdjacentMatrix()
        {
            var vertices = new List<Vertex<char>> { new Vertex<char>('A'), new Vertex<char>('B'), new Vertex<char>('C'), new Vertex<char>('D'), };
            var graph = new GraphWithAdjacentMatrix<char>(
                vertices,
                new int[,]
                {
                    { 0, 1, 1, int.MaxValue },
                    { 1, 0, 1, int.MaxValue },
                    { 1, 1, 0, 1 },
                    { int.MaxValue, int.MaxValue, 1, 0 },
                },
                false);
            var res = GraphAlgoSet.DFS(graph);
            Assert.True(res == null);
        }

        [Fact]
        public void TestDFSOnUnDirectedACyclicGraphWithAdjacentMatrix()
        {
            var vertices = new List<Vertex<char>> { new Vertex<char>('A'), new Vertex<char>('B'), new Vertex<char>('C'), new Vertex<char>('D'), };
            var graph = new GraphWithAdjacentMatrix<char>(
                vertices,
                new int[,]
                {
                    { 0, 1, int.MaxValue, int.MaxValue },
                    { 1, 0, 1, int.MaxValue },
                    { int.MaxValue, 1, 0, 1 },
                    { int.MaxValue, int.MaxValue, 1, 0 },
                },
                false);
            var res = GraphAlgoSet.DFS(graph);
            Assert.True(res != null);
            Assert.Equal(res.Count, 4);
            Assert.Equal(string.Concat(res.Select(r => r.Data)), "ABCD");
        }

        [Fact]
        public void TestBFSOnDirectedGraphWithAdjacentList()
        {
            var vertices = new List<Vertex<char>> { new Vertex<char>('A'), new Vertex<char>('B'), new Vertex<char>('C'), new Vertex<char>('D'), };
            var graph = new GraphWithAdjacentList<char>(
                vertices,
                new List<Edge<char>>
                {
                    new Edge<char> { From = vertices[0], To = vertices[1], Weight = 1 },
                    new Edge<char> { From = vertices[0], To = vertices[2], Weight = 1 },
                    new Edge<char> { From = vertices[1], To = vertices[2], Weight = 1 },
                    new Edge<char> { From = vertices[1], To = vertices[3], Weight = 1 },
                },
                true);
            var res = GraphAlgoSet.BFS(graph);
            Assert.True(res != null);
            Assert.Equal(res.Count, 4);
            Assert.Equal(string.Concat(res.Select(r => r.Data)), "ABCD");
        }

        [Fact]
        public void TestBFSOnUnDirectedGraphWithAdjacentList()
        {
            var vertices = new List<Vertex<char>> { new Vertex<char>('A'), new Vertex<char>('B'), new Vertex<char>('C'), new Vertex<char>('D'), };
            var graph = new GraphWithAdjacentList<char>(
                vertices,
                new List<Edge<char>>
                {
                    new Edge<char> { From = vertices[0], To = vertices[1], Weight = 1 },
                    new Edge<char> { From = vertices[1], To = vertices[0], Weight = 1 },
                    new Edge<char> { From = vertices[0], To = vertices[2], Weight = 1 },
                    new Edge<char> { From = vertices[2], To = vertices[0], Weight = 1 },
                    new Edge<char> { From = vertices[1], To = vertices[2], Weight = 1 },
                    new Edge<char> { From = vertices[2], To = vertices[1], Weight = 1 },
                    new Edge<char> { From = vertices[1], To = vertices[3], Weight = 1 },
                    new Edge<char> { From = vertices[3], To = vertices[1], Weight = 1 },
                },
                false);
            var res = GraphAlgoSet.BFS(graph);
            Assert.True(res != null);
            Assert.Equal(res.Count, 4);
            Assert.Equal(string.Concat(res.Select(r => r.Data)), "ABCD");
        }

        [Fact]
        public void TestBFSOnDirectedGraphWithAdjacentMatrix()
        {
            var vertices = new List<Vertex<char>> { new Vertex<char>('A'), new Vertex<char>('B'), new Vertex<char>('C'), new Vertex<char>('D'), };
            var graph = new GraphWithAdjacentMatrix<char>(
                vertices,
                new int[,]
                {
                    { 0, 1, 1, int.MaxValue },
                    { int.MaxValue, 0, 1, 1 },
                    { int.MaxValue, int.MaxValue, 0, int.MaxValue },
                    { int.MaxValue, int.MaxValue, int.MaxValue, 0 },
                },
                true);
            var res = GraphAlgoSet.BFS(graph);
            Assert.True(res != null);
            Assert.Equal(res.Count, 4);
            Assert.Equal(string.Concat(res.Select(r => r.Data)), "ABCD");
        }

        [Fact]
        public void TestBFSOnUnDirectedGraphWithAdjacentMatrix()
        {
            var vertices = new List<Vertex<char>> { new Vertex<char>('A'), new Vertex<char>('B'), new Vertex<char>('C'), new Vertex<char>('D'), };
            var graph = new GraphWithAdjacentMatrix<char>(
                vertices,
                new int[,]
                {
                    { 0, 1, 1, int.MaxValue },
                    { 1, 0, 1, 1 },
                    { 1, 1, 0, int.MaxValue },
                    { int.MaxValue, 1, int.MaxValue, 0 },
                },
                false);
            var res = GraphAlgoSet.BFS(graph);
            Assert.True(res != null);
            Assert.Equal(res.Count, 4);
            Assert.Equal(string.Concat(res.Select(r => r.Data)), "ABCD");
        }

        [Fact]
        public void TestTopologicalSortOnCyclicGraphWithAdjacentList()
        {
            var vertices = new List<Vertex<char>> { new Vertex<char>('A'), new Vertex<char>('B'), new Vertex<char>('C'), new Vertex<char>('D'), };
            var graph = new GraphWithAdjacentList<char>(
                vertices,
                new List<Edge<char>>
                {
                    new Edge<char> { From = vertices[0], To = vertices[1], Weight = 1 },
                    new Edge<char> { From = vertices[1], To = vertices[2], Weight = 1 },
                    new Edge<char> { From = vertices[2], To = vertices[0], Weight = 1 },
                    new Edge<char> { From = vertices[3], To = vertices[2], Weight = 1 },
                },
                true);
            var res1 = GraphAlgoSet.TopologicalSort(graph);
            var res2 = GraphAlgoSet.TopologicalSortWithDFS(graph);
            Assert.Equal(res1, null);
            Assert.Equal(res2, null);
        }

        [Fact]
        public void TestTopologicalSortOnACyclicGraphWithAdjacentList()
        {
            var vertices = new List<Vertex<char>> { new Vertex<char>('A'), new Vertex<char>('B'), new Vertex<char>('C'), new Vertex<char>('D'), };
            var graph = new GraphWithAdjacentList<char>(
                vertices,
                new List<Edge<char>>
                {
                    new Edge<char> { From = vertices[0], To = vertices[1], Weight = 1 },
                    new Edge<char> { From = vertices[1], To = vertices[2], Weight = 1 },
                    new Edge<char> { From = vertices[0], To = vertices[2], Weight = 1 },
                    new Edge<char> { From = vertices[3], To = vertices[2], Weight = 1 },
                },
                true);
            var res1 = GraphAlgoSet.TopologicalSort(graph);
            var res2 = GraphAlgoSet.TopologicalSortWithDFS(graph);
            Assert.True(res1 != null);
            Assert.Equal(res1.Count, 4);
            Assert.Equal(string.Concat(res1.Select(r => r.Data)), "ADBC");
            Assert.True(res2 != null);
            Assert.Equal(res2.Count, 4);
            Assert.Equal(string.Concat(res2.Select(r => r.Data)), "DABC");
        }

        [Fact]
        public void TestTopologicalSortOnCyclicGraphWithAdjacentMatrix()
        {
            var vertices = new List<Vertex<char>> { new Vertex<char>('A'), new Vertex<char>('B'), new Vertex<char>('C'), new Vertex<char>('D'), };
            var graph = new GraphWithAdjacentMatrix<char>(
                vertices,
                new int[,]
                {
                    { 0, 1, int.MaxValue, int.MaxValue },
                    { int.MaxValue, 0, 1, int.MaxValue },
                    { 1,int.MaxValue, 0, int.MaxValue },
                    { int.MaxValue, int.MaxValue, 1, 0 },
                },
                true);
            var res1 = GraphAlgoSet.TopologicalSort(graph);
            var res2 = GraphAlgoSet.TopologicalSortWithDFS(graph);
            Assert.Equal(res1, null);
            Assert.Equal(res2, null);
        }

        [Fact]
        public void TestTopologicalSortOnACyclicGraphWithAdjacentMatrix()
        {
            var vertices = new List<Vertex<char>> { new Vertex<char>('A'), new Vertex<char>('B'), new Vertex<char>('C'), new Vertex<char>('D'), };
            var graph = new GraphWithAdjacentMatrix<char>(
                vertices,
                new int[,]
                {
                    { 0, 1, 1, int.MaxValue },
                    { int.MaxValue, 0, 1, int.MaxValue },
                    { int.MaxValue, int.MaxValue, 0, int.MaxValue },
                    { int.MaxValue, int.MaxValue, 1, 0 },
                },
                true);
            var res1 = GraphAlgoSet.TopologicalSort(graph);
            var res2 = GraphAlgoSet.TopologicalSortWithDFS(graph);
            Assert.True(res1 != null);
            Assert.Equal(res1.Count, 4);
            Assert.Equal(string.Concat(res1.Select(r => r.Data)), "ADBC");
            Assert.True(res2 != null);
            Assert.Equal(res2.Count, 4);
            Assert.Equal(string.Concat(res2.Select(r => r.Data)), "DABC");
        }

        [Fact]
        public void TestSCCOnDirectedCyclicGraphWithAdjacentList()
        {
            var vertices = new List<Vertex<char>> { new Vertex<char>('A'), new Vertex<char>('B'), new Vertex<char>('C'), new Vertex<char>('D'), };
            var graph = new GraphWithAdjacentList<char>(
                vertices,
                new List<Edge<char>>
                {
                    new Edge<char> { From = vertices[0], To = vertices[1], Weight = 1 },
                    new Edge<char> { From = vertices[1], To = vertices[2], Weight = 1 },
                    new Edge<char> { From = vertices[2], To = vertices[0], Weight = 1 },
                    new Edge<char> { From = vertices[3], To = vertices[2], Weight = 1 },
                },
                true);
            var res = GraphAlgoSet.GetStronglyConnectedComponents(graph);
            Assert.True(res != null);
            Assert.Equal(res.Count, 2);
            var formatted = res.Select(r => string.Concat(r.Select(rr => rr.Data)));
            Assert.Contains("ACB", formatted);
            Assert.Contains("D", formatted);
        }

        [Fact]
        public void TestSCCOnDirectedACyclicGraphWithAdjacentList()
        {
            var vertices = new List<Vertex<char>> { new Vertex<char>('A'), new Vertex<char>('B'), new Vertex<char>('C'), new Vertex<char>('D'), };
            var graph = new GraphWithAdjacentList<char>(
                vertices,
                new List<Edge<char>>
                {
                    new Edge<char> { From = vertices[0], To = vertices[1], Weight = 1 },
                    new Edge<char> { From = vertices[1], To = vertices[2], Weight = 1 },
                    new Edge<char> { From = vertices[0], To = vertices[2], Weight = 1 },
                    new Edge<char> { From = vertices[3], To = vertices[2], Weight = 1 },
                },
                true);
            var res = GraphAlgoSet.GetStronglyConnectedComponents(graph);
            Assert.True(res != null);
            Assert.Equal(res.Count, 4);
            var formatted = res.Select(r => string.Concat(r.Select(rr => rr.Data)));
            Assert.Contains("A", formatted);
            Assert.Contains("B", formatted);
            Assert.Contains("C", formatted);
            Assert.Contains("D", formatted);
        }

        [Fact]
        public void TestSCCOnDirectedCyclicGraphWithAdjacentMatrix()
        {
            var vertices = new List<Vertex<char>> { new Vertex<char>('A'), new Vertex<char>('B'), new Vertex<char>('C'), new Vertex<char>('D'), };
            var graph = new GraphWithAdjacentMatrix<char>(
                vertices,
                new int[,]
                {
                    { 0, 1, int.MaxValue, int.MaxValue },
                    { int.MaxValue, 0, 1, int.MaxValue },
                    { 1,int.MaxValue, 0, int.MaxValue },
                    { int.MaxValue, int.MaxValue, 1, 0 },
                },
                true);
            var res = GraphAlgoSet.GetStronglyConnectedComponents(graph);
            Assert.True(res != null);
            Assert.Equal(res.Count, 2);
            var formatted = res.Select(r => string.Concat(r.Select(rr => rr.Data)));
            Assert.Contains("ACB", formatted);
            Assert.Contains("D", formatted);
        }

        [Fact]
        public void TestSCCOnDirectedACyclicGraphWithAdjacentMatrix()
        {
            var vertices = new List<Vertex<char>> { new Vertex<char>('A'), new Vertex<char>('B'), new Vertex<char>('C'), new Vertex<char>('D'), };
            var graph = new GraphWithAdjacentMatrix<char>(
                vertices,
                new int[,]
                {
                    { 0, 1, 1, int.MaxValue },
                    { int.MaxValue, 0, 1, int.MaxValue },
                    { int.MaxValue, int.MaxValue, 0, int.MaxValue },
                    { int.MaxValue, int.MaxValue, 1, 0 },
                },
                true);
            var res = GraphAlgoSet.GetStronglyConnectedComponents(graph);
            Assert.True(res != null);
            Assert.Equal(res.Count, 4);
            var formatted = res.Select(r => string.Concat(r.Select(rr => rr.Data)));
            Assert.Contains("A", formatted);
            Assert.Contains("B", formatted);
            Assert.Contains("C", formatted);
            Assert.Contains("D", formatted);
        }

        [Fact]
        public void TestBellmanFordOnDirectedGraphWithAdjacentList()
        {
            var vertices = new List<Vertex<char>> { new Vertex<char>('A'), new Vertex<char>('B'), new Vertex<char>('C'), new Vertex<char>('D'), new Vertex<char>('E'), };
            var graph = new GraphWithAdjacentList<char>(
                vertices,
                new List<Edge<char>>
                {
                    new Edge<char> { From = vertices[0], To = vertices[1], Weight = 6 },
                    new Edge<char> { From = vertices[0], To = vertices[2], Weight = 7 },
                    new Edge<char> { From = vertices[1], To = vertices[2], Weight = 8 },
                    new Edge<char> { From = vertices[1], To = vertices[3], Weight = -4 },
                    new Edge<char> { From = vertices[1], To = vertices[4], Weight = 5 },
                    new Edge<char> { From = vertices[2], To = vertices[3], Weight = 9 },
                    new Edge<char> { From = vertices[2], To = vertices[4], Weight = -3 },
                    new Edge<char> { From = vertices[3], To = vertices[0], Weight = 2 },
                    new Edge<char> { From = vertices[3], To = vertices[4], Weight = 7 },
                    new Edge<char> { From = vertices[4], To = vertices[1], Weight = -2 },
                },
                true);
            var res = GraphAlgoSet.BellmanFord(graph, vertices[0]);
            Assert.True(res != null);
            Assert.Equal(0, res[vertices[0]]);
            Assert.Equal(2, res[vertices[1]]);
            Assert.Equal(7, res[vertices[2]]);
            Assert.Equal(-2, res[vertices[3]]);
            Assert.Equal(4, res[vertices[4]]);
        }

        [Fact]
        public void TestBellmanFordOnDirectedNegativeCyclicGraphWithAdjacentList()
        {
            var vertices = new List<Vertex<char>> { new Vertex<char>('A'), new Vertex<char>('B'), new Vertex<char>('C'), new Vertex<char>('D'), new Vertex<char>('E'), };
            var graph = new GraphWithAdjacentList<char>(
                vertices,
                new List<Edge<char>>
                {
                    new Edge<char> { From = vertices[0], To = vertices[1], Weight = 6 },
                    new Edge<char> { From = vertices[0], To = vertices[2], Weight = 7 },
                    new Edge<char> { From = vertices[1], To = vertices[2], Weight = 8 },
                    new Edge<char> { From = vertices[1], To = vertices[3], Weight = -4 },
                    new Edge<char> { From = vertices[1], To = vertices[4], Weight = 5 },
                    new Edge<char> { From = vertices[2], To = vertices[3], Weight = 9 },
                    new Edge<char> { From = vertices[2], To = vertices[4], Weight = -3 },
                    new Edge<char> { From = vertices[3], To = vertices[0], Weight = 2 },
                    new Edge<char> { From = vertices[3], To = vertices[4], Weight = 7 },
                    new Edge<char> { From = vertices[4], To = vertices[1], Weight = -6 },
                },
                true);
            var res = GraphAlgoSet.BellmanFord(graph, vertices[0]);
            Assert.Equal(res, null);
        }

        [Fact]
        public void TestBellmanFordOnDirectedGraphWithAdjacentMatrix()
        {
            var vertices = new List<Vertex<char>> { new Vertex<char>('A'), new Vertex<char>('B'), new Vertex<char>('C'), new Vertex<char>('D'), new Vertex<char>('E'), };
            var graph = new GraphWithAdjacentMatrix<char>(
                vertices,
                new int[,]
                {
                    { 0, 6, 7, int.MaxValue, int.MaxValue },
                    { int.MaxValue, 0, 8, -4, 5 },
                    { int.MaxValue, int.MaxValue, 0, 9, -3 },
                    { 2, int.MaxValue, int.MaxValue, 0, 7 },
                    { int.MaxValue, -2, int.MaxValue, int.MaxValue, 0 },
                },
                true);
            var res = GraphAlgoSet.BellmanFord(graph, vertices[0]);
            Assert.True(res != null);
            Assert.Equal(0, res[vertices[0]]);
            Assert.Equal(2, res[vertices[1]]);
            Assert.Equal(7, res[vertices[2]]);
            Assert.Equal(-2, res[vertices[3]]);
            Assert.Equal(4, res[vertices[4]]);
        }

        [Fact]
        public void TestBellmanFordOnDirectedNegativeCyclicGraphWithAdjacentMatrix()
        {
            var vertices = new List<Vertex<char>> { new Vertex<char>('A'), new Vertex<char>('B'), new Vertex<char>('C'), new Vertex<char>('D'), new Vertex<char>('E'), };
            var graph = new GraphWithAdjacentMatrix<char>(
                vertices,
                new int[,]
                {
                    { 0, 6, 7, int.MaxValue, int.MaxValue },
                    { int.MaxValue, 0, 8, -4, 5 },
                    { int.MaxValue, int.MaxValue, 0, 9, -3 },
                    { 2, int.MaxValue, int.MaxValue, 0, 7 },
                    { int.MaxValue, -6, int.MaxValue, int.MaxValue, 0 },
                },
                true);
            var res = GraphAlgoSet.BellmanFord(graph, vertices[0]);
            Assert.Equal(res, null);
        }

        [Fact]
        public void TestSSPTOnDirectedGraphWithAdjacentList()
        {
            var vertices = new List<Vertex<char>> { new Vertex<char>('A'), new Vertex<char>('B'), new Vertex<char>('C'), new Vertex<char>('D'), new Vertex<char>('E'), new Vertex<char>('F'), };
            var graph = new GraphWithAdjacentList<char>(
                vertices,
                new List<Edge<char>>
                {
                    new Edge<char> { From = vertices[0], To = vertices[1], Weight = 5 },
                    new Edge<char> { From = vertices[0], To = vertices[2], Weight = 3 },
                    new Edge<char> { From = vertices[1], To = vertices[2], Weight = 2 },
                    new Edge<char> { From = vertices[1], To = vertices[3], Weight = 6 },
                    new Edge<char> { From = vertices[2], To = vertices[3], Weight = 7 },
                    new Edge<char> { From = vertices[2], To = vertices[4], Weight = 4 },
                    new Edge<char> { From = vertices[2], To = vertices[5], Weight = 2 },
                    new Edge<char> { From = vertices[3], To = vertices[4], Weight = -1 },
                    new Edge<char> { From = vertices[3], To = vertices[5], Weight = 1 },
                    new Edge<char> { From = vertices[4], To = vertices[5], Weight = -2 },
                },
                true);
            var res = GraphAlgoSet.SingleSourceShortestPathWithTopologicalSort(graph, vertices[1]);
            Assert.True(res != null);
            Assert.Equal(int.MaxValue, res[vertices[0]]);
            Assert.Equal(0, res[vertices[1]]);
            Assert.Equal(2, res[vertices[2]]);
            Assert.Equal(6, res[vertices[3]]);
            Assert.Equal(5, res[vertices[4]]);
            Assert.Equal(3, res[vertices[5]]);
        }

        [Fact]
        public void TestSSPTOnDirectedGraphWithAdjacentMatrix()
        {
            var vertices = new List<Vertex<char>> { new Vertex<char>('A'), new Vertex<char>('B'), new Vertex<char>('C'), new Vertex<char>('D'), new Vertex<char>('E'), new Vertex<char>('F'), };
            var graph = new GraphWithAdjacentMatrix<char>(
                vertices,
                new int[,]
                {
                    { 0, 5, 3, int.MaxValue, int.MaxValue, int.MaxValue },
                    { int.MaxValue, 0, 2, 6, int.MaxValue, int.MaxValue },
                    { int.MaxValue, int.MaxValue, 0, 7, 4, 2 },
                    { int.MaxValue, int.MaxValue, int.MaxValue, 0, -1, 1 },
                    { int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, 0, -2 },
                    { int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, 0 },
                },
                true);
            var res = GraphAlgoSet.SingleSourceShortestPathWithTopologicalSort(graph, vertices[1]);
            Assert.True(res != null);
            Assert.Equal(int.MaxValue, res[vertices[0]]);
            Assert.Equal(0, res[vertices[1]]);
            Assert.Equal(2, res[vertices[2]]);
            Assert.Equal(6, res[vertices[3]]);
            Assert.Equal(5, res[vertices[4]]);
            Assert.Equal(3, res[vertices[5]]);
        }

        [Fact]
        public void TestFloydWarshallOnDirectedGraphWithAdjacentMatrix()
        {
            var vertices = new List<Vertex<char>> { new Vertex<char>('A'), new Vertex<char>('B'), new Vertex<char>('C'), new Vertex<char>('D'), new Vertex<char>('E'), };
            var graph = new GraphWithAdjacentMatrix<char>(
                vertices,
                new int[,]
                {
                    { 0, 3, 8, int.MaxValue, -4 },
                    { int.MaxValue, 0, int.MaxValue, 1, 7 },
                    { int.MaxValue, 4, 0, int.MaxValue, int.MaxValue },
                    { 2, int.MaxValue, -5, 0, int.MaxValue },
                    { int.MaxValue, int.MaxValue, int.MaxValue, 6, 0, },
                },
                true);
            var res = GraphAlgoSet.FloydWarshall(graph);
            Assert.True(res != null);
            Assert.Equal(
                new long[,]
                {
                    { 0, 1, -3, 2, -4 },
                    { 3, 0, -4, 1, -1 },
                    { 7, 4, 0, 5, 3 },
                    { 2, -1, -5, 0, -2 },
                    { 8, 5, 1, 6, 0 },
                },
                res);
        }

        [Fact]
        public void TestKruskalOnUnDirectedConnectedGraphWithAdjacentList()
        {
            var vertices = new List<Vertex<char>>
            {
                new Vertex<char>('A'), new Vertex<char>('B'), new Vertex<char>('C'), new Vertex<char>('D'),
                new Vertex<char>('E'), new Vertex<char>('F'), new Vertex<char>('G'), new Vertex<char>('H'),
                new Vertex<char>('I'),
            };
            var edges = new List<Edge<char>>
                {
                    new Edge<char> { From = vertices[0], To = vertices[1], Weight = 4 },
                    new Edge<char> { From = vertices[1], To = vertices[0], Weight = 4 },
                    new Edge<char> { From = vertices[0], To = vertices[7], Weight = 8 },
                    new Edge<char> { From = vertices[7], To = vertices[0], Weight = 8 },
                    new Edge<char> { From = vertices[1], To = vertices[2], Weight = 8 },
                    new Edge<char> { From = vertices[2], To = vertices[1], Weight = 8 },
                    new Edge<char> { From = vertices[1], To = vertices[7], Weight = 11 },
                    new Edge<char> { From = vertices[7], To = vertices[1], Weight = 11 },
                    new Edge<char> { From = vertices[2], To = vertices[3], Weight = 7 },
                    new Edge<char> { From = vertices[3], To = vertices[2], Weight = 7 },
                    new Edge<char> { From = vertices[2], To = vertices[5], Weight = 4 },
                    new Edge<char> { From = vertices[5], To = vertices[2], Weight = 4 },
                    new Edge<char> { From = vertices[2], To = vertices[8], Weight = 2 },
                    new Edge<char> { From = vertices[8], To = vertices[2], Weight = 2 },
                    new Edge<char> { From = vertices[3], To = vertices[4], Weight = 9 },
                    new Edge<char> { From = vertices[4], To = vertices[3], Weight = 9 },
                    new Edge<char> { From = vertices[3], To = vertices[5], Weight = 14 },
                    new Edge<char> { From = vertices[5], To = vertices[3], Weight = 14 },
                    new Edge<char> { From = vertices[4], To = vertices[5], Weight = 10 },
                    new Edge<char> { From = vertices[5], To = vertices[4], Weight = 10 },
                    new Edge<char> { From = vertices[5], To = vertices[6], Weight = 2 },
                    new Edge<char> { From = vertices[6], To = vertices[5], Weight = 2 },
                    new Edge<char> { From = vertices[6], To = vertices[7], Weight = 1 },
                    new Edge<char> { From = vertices[7], To = vertices[6], Weight = 1 },
                    new Edge<char> { From = vertices[6], To = vertices[8], Weight = 6 },
                    new Edge<char> { From = vertices[8], To = vertices[6], Weight = 6 },
                    new Edge<char> { From = vertices[7], To = vertices[8], Weight = 7 },
                    new Edge<char> { From = vertices[8], To = vertices[7], Weight = 7 },
                };
            var graph = new GraphWithAdjacentList<char>(vertices, edges, false);
            var res = GraphAlgoSet.Kruskal(graph);
            Assert.True(res != null);
            Assert.Equal(
                res,
                new List<Edge<char>>
                {
                    edges[22],
                    edges[12],
                    edges[20],
                    edges[0],
                    edges[10],
                    edges[8],
                    edges[2],
                    edges[14],
                });
        }

        [Fact]
        public void TestKruskalOnUnDirectedConnectedGraphWithAdjacentMatrix()
        {
            var vertices = new List<Vertex<char>>
            {
                new Vertex<char>('A'), new Vertex<char>('B'), new Vertex<char>('C'), new Vertex<char>('D'),
                new Vertex<char>('E'), new Vertex<char>('F'), new Vertex<char>('G'), new Vertex<char>('H'),
                new Vertex<char>('I'),
            };
            int M = int.MaxValue;
            var edges = new int[,]
                {
                    { 0, 4, M, M, M,  M, M,  8, M },
                    { 4, 0, 8, M, M,  M, M, 11, M },
                    { M, 8, 0, 7, M,  4, M,  M, 2 },
                    { M, M, 7, 0, 9, 14, M,  M, M },
                    { M, M, M, 9, 0, 10, M,  M, M },
                    { M, M, 4, 14,10, 0, 2,  M, M },
                    { M, M, M, M, M,  2, 0,  1, 6 },
                    { 8, 11,M, M, M,  M, 1,  0, 7 },
                    { M, M, 2, M, M,  M, 6,  7, 0 }
                };
            var graph = new GraphWithAdjacentMatrix<char>(vertices, edges, false);
            var res = GraphAlgoSet.Kruskal(graph);
            Assert.True(res != null);
            Assert.Equal(res.Count, 8);
            Assert.Equal(
                res.Select(r => string.Concat(r.From.Data, r.To.Data, r.Weight)),
                new List<string>
                {
                    "GH1",
                    "CI2",
                    "FG2",
                    "AB4",
                    "CF4",
                    "CD7",
                    "AH8",
                    "DE9",
                });
        }
    }
}
