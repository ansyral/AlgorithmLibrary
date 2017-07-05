namespace XuanLibrary.Test
{
    using System.Linq;

    using XuanLibrary.Algorithm.SortAlgoSet;
    using XuanLibrary.Test.TestDataStructure;

    using Xunit;

    public class OnSquareSortAlgoTest
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
        public void TestSelectionSortBasic(int[] source, int[] expected)
        {
            OnSquareSort.SelectionSort(source);
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
        public void TestInsertSortBasic(int[] source, int[] expected)
        {
            OnSquareSort.InsertSort(source);
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
        public void TestBubbleSortBasic(int[] source, int[] expected)
        {
            OnSquareSort.BubbleSort(source);
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
        public void TestSelectionSortDuplicate(string[] source, string[] expected)
        {
            var s = source.Select(i => (Node)i).ToArray();
            var e = expected.Select(i => (Node)i).ToArray();
            OnSquareSort.SelectionSort(s);
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
            new string[] { "zoo-1", "tank-2", "park-3", "hi-4", "book-4", "tuck-5", "tree-6", "test-7", "hello-8" })]
        [InlineData(
            new string[] { "tank-2", "hi-4", "hello-8", "test-7", "zoo-1", "park-3", "tuck-5", "tree-6", "book-4" },
            new string[] { "zoo-1", "tank-2", "park-3", "hi-4", "book-4", "tuck-5", "tree-6", "test-7", "hello-8" })]
        [InlineData(
            new string[] { "tank-2", "hello-8", "test-7", "zoo-1", "spec-8", "park-3", "tuck-5", "tree-6", "book-4" },
            new string[] { "zoo-1", "tank-2", "park-3", "book-4", "tuck-5", "tree-6", "test-7", "hello-8", "spec-8" })]
        public void TestInsertSortDuplicate(string[] source, string[] expected)
        {
            var s = source.Select(i => (Node)i).ToArray();
            var e = expected.Select(i => (Node)i).ToArray();
            OnSquareSort.InsertSort(s);
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
            new string[] { "zoo-1", "tank-2", "park-3", "hi-4", "book-4", "tuck-5", "tree-6", "test-7", "hello-8" })]
        [InlineData(
            new string[] { "tank-2", "hi-4", "hello-8", "test-7", "zoo-1", "park-3", "tuck-5", "tree-6", "book-4" },
            new string[] { "zoo-1", "tank-2", "park-3", "hi-4", "book-4", "tuck-5", "tree-6", "test-7", "hello-8" })]
        [InlineData(
            new string[] { "tank-2", "hello-8", "test-7", "zoo-1", "spec-8", "park-3", "tuck-5", "tree-6", "book-4" },
            new string[] { "zoo-1", "tank-2", "park-3", "book-4", "tuck-5", "tree-6", "test-7", "hello-8", "spec-8" })]
        public void TestBubbleSortDuplicate(string[] source, string[] expected)
        {
            var s = source.Select(i => (Node)i).ToArray();
            var e = expected.Select(i => (Node)i).ToArray();
            OnSquareSort.BubbleSort(s);
            Assert.Equal(
                e,
                s);
        }
    }
}
