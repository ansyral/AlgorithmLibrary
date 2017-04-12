namespace XuanLibrary.Test
{
    using System;
    using XuanLibrary.DataStructure.Heap;

    using Xunit;

    public class HeapTest
    {
        [Fact]
        public void TestHeapSortAscending()
        {
            var heap = new MaxHeap<string>(
                new[] { "hello", "hero", "fun", "test", "xamarin", "woohoo", "Animal" },
                StringComparer.OrdinalIgnoreCase);
            var sorted = heap.Sort();
            Assert.Equal(
                new[] { "Animal", "fun", "hello", "hero", "test", "woohoo", "xamarin" },
                sorted);
        }

        [Fact]
        public void TestHeapSortDescending()
        {
            var heap = new MinHeap<string>(
                new[] { "hello", "hero", "fun", "test", "xamarin", "woohoo", "Animal" },
                StringComparer.OrdinalIgnoreCase);
            var sorted = heap.Sort();
            Assert.Equal(
                new[] { "xamarin", "woohoo", "test", "hero", "hello", "fun", "Animal" },
                sorted);
        }
    }
}
