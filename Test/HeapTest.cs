namespace XuanLibrary.Test
{
    using System;
    using XuanLibrary.DataStructure.Heap;

    using Xunit;

    public class HeapTest
    {
        [Fact]
        public void TestBasic()
        {
            var heap = new Heap<string>(
                new[] { "hello", "hero", "fun", "test", "xamarin", "woohoo", "Animal" },
                StringComparer.OrdinalIgnoreCase, true);
            Assert.Equal("xamarin", heap.Peek());
            heap.Pop();
            Assert.Equal("woohoo", heap.Peek());
            heap.Push("xunit");
            heap.Push("pycharm");
            Assert.Equal(14, heap.Length);
            Assert.Equal("xunit", heap.Peek());
            heap.Update(4, "tree");
            Assert.Equal("tree", heap[1]);
        }

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

        [Fact]
        public void TestMinPriorityQueue()
        {
            var queue = new MinPriorityQueue<int>(new int[] { 5, 6, 3, 1, 9 });
            Assert.Equal(1, queue.Min());
            queue.Insert(11);
            queue.Insert(7);
            queue.Insert(8);
            Assert.Equal(1, queue.Min());
            Assert.Equal(1, queue.ExtractMin());
            Assert.Equal(3, queue.ExtractMin());
            queue.DecreasePriority(4, 0);
            Assert.Equal(0, queue.Min());
            Assert.Equal(6, queue.Count);
        }
    }
}
