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
                    new Edge<char> { From = vertices[1], To = vertices[2], Weight = 1 },
                    new Edge<char> { From = vertices[0], To = vertices[2], Weight = 1 },
                    new Edge<char> { From = vertices[3], To = vertices[2], Weight = 1 },
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
                    new Edge<char> { From = vertices[1], To = vertices[2], Weight = 1 },
                    new Edge<char> { From = vertices[3], To = vertices[2], Weight = 1 },
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
    }
}
