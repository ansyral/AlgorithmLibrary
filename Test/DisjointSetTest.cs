namespace XuanLibrary.Test
{
    using System.Collections.Generic;
    using XuanLibrary.DataStructure.DisjointSet;

    using Xunit;

    public class DisjointSetTest
    {
        [Fact]
        public void TestDisjointSetLinkedList()
        {
            var list = new DisjointSetLinkedList<string>();
            list.MakeSet("here");
            list.MakeSet("hree");
            list.MakeSet("heer");
            list.MakeSet("ehre");
            list.MakeSet("eher");
            list.MakeSet("jump");
            list.MakeSet("umpj");
            list.MakeSet("upmj");
            list.Union("here", "hree");
            list.Union("heer", "ehre");
            list.Union("ehre", "eher");
            list.Union("here", "ehre");
            list.Union("jump", "umpj");
            list.Union("jump", "upmj");
            Assert.Equal(list.FindSet("here"), list.FindSet("hree"));
            Assert.Equal(list.FindSet("here"), list.FindSet("heer"));
            Assert.Equal(list.FindSet("here"), list.FindSet("ehre"));
            Assert.Equal(list.FindSet("here"), list.FindSet("eher"));
            Assert.Equal(list.FindSet("hree"), list.FindSet("eher"));
            Assert.Equal(list.FindSet("jump"), list.FindSet("umpj"));
            Assert.Equal(list.FindSet("jump"), list.FindSet("upmj"));
            Assert.NotEqual(list.FindSet("here"), list.FindSet("jump"));
        }

        [Fact]
        public void TestDisjointSetForests()
        {
            var list = new DisjointSetForests<string>();
            list.MakeSet("here");
            list.MakeSet("hree");
            list.MakeSet("heer");
            list.MakeSet("ehre");
            list.MakeSet("eher");
            list.MakeSet("jump");
            list.MakeSet("umpj");
            list.MakeSet("upmj");
            list.Union("here", "hree");
            list.Union("heer", "ehre");
            list.Union("ehre", "eher");
            list.Union("here", "ehre");
            list.Union("jump", "umpj");
            list.Union("jump", "upmj");
            Assert.Equal(list.FindSet("here"), list.FindSet("hree"));
            Assert.Equal(list.FindSet("here"), list.FindSet("heer"));
            Assert.Equal(list.FindSet("here"), list.FindSet("ehre"));
            Assert.Equal(list.FindSet("here"), list.FindSet("eher"));
            Assert.Equal(list.FindSet("hree"), list.FindSet("eher"));
            Assert.Equal(list.FindSet("jump"), list.FindSet("umpj"));
            Assert.Equal(list.FindSet("jump"), list.FindSet("upmj"));
            Assert.NotEqual(list.FindSet("here"), list.FindSet("jump"));
        }

        [Fact]
        public void TestDisjointSetForestsWithIndex()
        {
            var nodes = new List<string>
            {
                "here", "hree", "heer", "ehre", "eher", "jump", "umpj", "upmj"
            };
            var list = new DisjointSetForestsWithIndex();
            list.MakeSet(8);
            list.Union(0, 1);
            list.Union(2, 3);
            list.Union(3, 4);
            list.Union(0, 3);
            list.Union(5, 6);
            list.Union(5, 7);
            Assert.Equal(list.FindSet(0), list.FindSet(1));
            Assert.Equal(list.FindSet(0), list.FindSet(2));
            Assert.Equal(list.FindSet(0), list.FindSet(3));
            Assert.Equal(list.FindSet(0), list.FindSet(4));
            Assert.Equal(list.FindSet(1), list.FindSet(4));
            Assert.Equal(list.FindSet(5), list.FindSet(6));
            Assert.Equal(list.FindSet(5), list.FindSet(7));
            Assert.NotEqual(list.FindSet(0), list.FindSet(5));
        }
    }
}
