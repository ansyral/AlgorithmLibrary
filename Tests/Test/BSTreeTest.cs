namespace XuanLibrary.Test
{
    using System;
    using XuanLibrary.DataStructure.Tree;

    using Xunit;

    public class BSTreeTest
    {
        [Fact]
        public void TestBasic()
        {
            var tree = new BinarySearchTree<string>(
                new string[] { "scan", "pick", "Zoo", "apple", "pocket", "swim", null, null, "cat", null, "rock", "sport", null, "book", null },
                StringComparer.OrdinalIgnoreCase);
            Assert.Equal(tree.Minimum(tree.Root).Value, "apple");
            Assert.Equal(tree.Maximum(tree.Root).Value, "Zoo");
            var find = tree.Search("notexisted");
            Assert.Null(find);
            find = tree.Search("cat");
            Assert.Equal(find.Value, "cat");
            Assert.Equal(find.Left.Value, "book");
            Assert.Null(find.Right);
            Assert.Equal(tree.Successor(find).Value, "pick");
            Assert.Equal(tree.Predecessor(find).Value, "book");
            Assert.Null(tree.Predecessor(tree.Search("apple")));
            Assert.Null(tree.Successor(tree.Search("zoo")));
            Assert.Equal(tree.Insert(new TreeNodeWithParent<string> { Value = "zero" }).Value, "swim");
            tree.Delete(tree.Search("pick"));
            Assert.Equal(tree.Root.Left.Value, "pocket");
            Assert.Equal(tree.Search("pocket").Left.Value, "apple");
        }
    }
}
