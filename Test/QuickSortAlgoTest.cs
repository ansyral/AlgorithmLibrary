namespace XuanLibrary.Test
{
    using System;
    using System.Linq;
    using XuanLibrary.Algorithm.SortAlgoSet;

    using Xunit;

    public class QuickSortAlgoTest
    {
        [Theory]
        [InlineData(
            new int[] { 2, 8, 7, 1, 3, 5, 6, 4 },
            new[] { 1, 2, 3, 4, 5, 6, 7, 8 })]
        [InlineData(
            new int[] { 1, 2, 3, 4, 5, 6, 7, 8 },
            new int[] { 1, 2, 3, 4, 5, 6, 7, 8 })]
        [InlineData(
            new int[] { 8, 7, 6, 5, 4, 3, 2, 1 },
            new int[] { 1, 2, 3, 4, 5, 6, 7, 8 })]
        [InlineData(
            new int[] { 8, 7, 6, 5, 1, 2, 3, 4 },
            new int[] { 1, 2, 3, 4, 5, 6, 7, 8 })]
        public void TestQuickSortLastAsPivotBasic(int[] source, int[] expected)
        {
            QuickSort.SortLastAsPivot(source);
            Assert.Equal(
                expected,
                source);
        }

        [Theory]
        [InlineData(
            new int[] { 2, 8, 7, 1, 3, 5, 6, 4 },
            new[] { 1, 2, 3, 4, 5, 6, 7, 8 })]
        [InlineData(
            new int[] { 1, 2, 3, 4, 5, 6, 7, 8 },
            new int[] { 1, 2, 3, 4, 5, 6, 7, 8 })]
        [InlineData(
            new int[] { 8, 7, 6, 5, 4, 3, 2, 1 },
            new int[] { 1, 2, 3, 4, 5, 6, 7, 8 })]
        [InlineData(
            new int[] { 8, 7, 6, 5, 1, 2, 3, 4 },
            new int[] { 1, 2, 3, 4, 5, 6, 7, 8 })]
        public void TestQuickSortFirstAsPivotBasic(int[] source, int[] expected)
        {
            QuickSort.SortFirstAsPivot(source);
            Assert.Equal(
                expected,
                source);
        }

        [Theory]
        [InlineData(
            new int[] { 2, 8, 7, 1, 3, 5, 6, 4 },
            new[] { 1, 2, 3, 4, 5, 6, 7, 8 })]
        [InlineData(
            new int[] { 1, 2, 3, 4, 5, 6, 7, 8 },
            new int[] { 1, 2, 3, 4, 5, 6, 7, 8 })]
        [InlineData(
            new int[] { 8, 7, 6, 5, 4, 3, 2, 1 },
            new int[] { 1, 2, 3, 4, 5, 6, 7, 8 })]
        [InlineData(
            new int[] { 8, 7, 6, 5, 1, 2, 3, 4 },
            new int[] { 1, 2, 3, 4, 5, 6, 7, 8 })]
        public void TestQuickSortTwoWayAsPivotBasic(int[] source, int[] expected)
        {
            QuickSort.SortTwoWay(source);
            Assert.Equal(
                expected,
                source);
        }

        [Theory]
        [InlineData(
            new string[] { "tank-2", "hello-8", "test-7", "zoo-1", "park-3", "hi-2", "tuck-5", "tree-6", "book-4" },
            new string[] { "zoo-1", "tank-2", "hi-2", "park-3", "book-4", "tuck-5", "tree-6", "test-7", "hello-8" })]
        [InlineData(
            new string[] { "tank-2", "hello-8", "test-7", "zoo-1", "park-3", "hi-4", "tuck-5", "tree-6", "book-4" },
            new string[] { "zoo-1", "tank-2", "park-3", "hi-4", "book-4", "tuck-5", "tree-6", "test-7", "hello-8" })]
        [InlineData(
            new string[] { "tank-2", "hi-4", "hello-8", "test-7", "zoo-1", "park-3", "tuck-5", "tree-6", "book-4" },
            new string[] { "zoo-1", "tank-2", "park-3", "hi-4", "book-4", "tuck-5", "tree-6", "test-7", "hello-8" })]
        [InlineData(
            new string[] { "tank-2", "hello-8", "test-7", "zoo-1", "spec-8", "park-3", "tuck-5", "tree-6", "book-4" },
            new string[] { "zoo-1", "tank-2", "park-3", "book-4", "tuck-5", "tree-6", "test-7", "spec-8", "hello-8" })]
        public void TestQuickSortLastAsPivotDuplicate(string[] source, string[] expected)
        {
            // cannot use Enumearble.Cast<T>
            var s = source.Select(i => (Node)i).ToArray();
            var e = expected.Select(i => (Node)i).ToArray();
            QuickSort.SortLastAsPivot(s);
            Assert.Equal(
                e,
                s);
        }

        [Theory]
        [InlineData(
            new string[] { "tank-2", "hello-8", "test-7", "zoo-1", "park-3", "hi-2", "tuck-5", "tree-6", "book-4" },
            new string[] { "zoo-1", "tank-2", "hi-2", "park-3", "book-4", "tuck-5", "tree-6", "test-7", "hello-8" })]
        [InlineData(
            new string[] { "tank-2", "hello-8", "test-7", "zoo-1", "park-3", "hi-4", "tuck-5", "tree-6", "book-4" },
            new string[] { "zoo-1", "tank-2", "park-3", "book-4", "hi-4", "tuck-5", "tree-6", "test-7", "hello-8" })]
        [InlineData(
            new string[] { "tank-2", "hi-4", "hello-8", "test-7", "zoo-1", "park-3", "tuck-5", "tree-6", "book-4" },
            new string[] { "zoo-1", "tank-2", "park-3", "book-4", "hi-4", "tuck-5", "tree-6", "test-7", "hello-8" })]
        public void TestQuickSortFirstAsPivotDuplicate(string[] source, string[] expected)
        {
            // cannot use Enumearble.Cast<T>
            var s = source.Select(i => (Node)i).ToArray();
            var e = expected.Select(i => (Node)i).ToArray();
            QuickSort.SortFirstAsPivot(s);
            Assert.Equal(
                e,
                s);
        }


        public class Node : IComparable<Node>, IEquatable<Node>
        {
            public string Name { get; set; }

            public int Value { get; set; }

            public Node(string name, int val)
            {
                Name = name;
                Value = val;
            }

            public static explicit operator Node(string str)
            {
                string[] parts = str.Split('-');
                return new Node(parts[0], int.Parse(parts[1]));
            }

            public int CompareTo(Node other)
            {
                return this.Value - other.Value;
            }

            public bool Equals(Node other)
            {
                if (other == null)
                {
                    return false;
                }
                if (ReferenceEquals(this, other))
                {
                    return true;
                }
                return Name == other.Name && Value == other.Value;
            }

            public override bool Equals(object obj)
            {
                return Equals(obj as Node);
            }

            public override int GetHashCode()
            {
                return Name.GetHashCode() ^ (Value.GetHashCode() << 1);
            }
        }
    }
}
