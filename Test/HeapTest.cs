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
            var heap = HeapFactory.CreateMax<string>(
                new[] { "hello", "hero", "fun", "test", "xamarin", "woohoo", "Animal" },
                StringComparer.OrdinalIgnoreCase);
            Assert.Equal("xamarin", heap.Peek());
            heap.Pop();
            Assert.Equal("woohoo", heap.Peek());
            heap.Push("xunit");
            heap.Push("pycharm");
            Assert.Equal(14, heap.Length);
            Assert.Equal("xunit", heap.Peek());

            var heapWithKey = HeapFactory.CreateMax<string, TestNode>(
                new TestNode[] { "hi", "hello", "wow", "thanks", "cat", "library", "extraordinary" });
            Assert.Equal("extraordinary", heapWithKey.Peek().Key);
            heapWithKey.Pop();
            Assert.Equal("library", heapWithKey.Peek().Key);
            heapWithKey.Push("lala");
            heapWithKey.Push("excellent");
            Assert.Equal(14, heapWithKey.Length);
            Assert.Equal("excellent", heapWithKey.Peek().Key);
            heapWithKey.Update("cat", "catcatcatcat");
            Assert.Equal("catcatcatcat", heapWithKey.Peek().Key);
        }

        [Fact]
        public void TestHeapSortAscending()
        {
            var heap = HeapFactory.CreateMax<string>(
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
            var heap = HeapFactory.CreateMin<string>(
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
            Assert.Equal(6, queue.Count);
            var queueWithkey = new MinPriorityQueue<string, TestNode>(
                new TestNode[] { "hi", "hello", "wow", "thanks", "cat", "library", "extraordinary" });
            Assert.Equal("hi", queueWithkey.ExtractMin().Key);
            Assert.Equal("wow", queueWithkey.Min().Key);
            queueWithkey.DecreasePriority("extraordinary", "ex");
            Assert.Equal("ex", queueWithkey.Min().Key);
        }

        private class TestNode : IHasKey<string>, IComparable<TestNode>
        {
            public string Key{ get; set; }

            public int Length => Key.Length;

            public TestNode(string k)
            {
                Key = k;
            }

            public static implicit operator TestNode(string k)
            {
                return new TestNode(k);
            }

            public int CompareTo(TestNode other)
            {
                return this.Length - other.Length;
            }
        }
    }
}
