namespace XuanLibrary.Test
{
    using XuanLibrary.Algorithm.SortAlgoSet;

    using Xunit;

    public class HeapSortAlgoTest
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
        public void TestHeapSortBasic(int[] source, int[] expected)
        {
            HeapSort.Sort(source);
            Assert.Equal(
                expected,
                source);
        }
    }
}
