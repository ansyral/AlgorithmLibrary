namespace XuanLibrary.DataStructure.Tree
{
    using System;
    using System.Collections.Generic;

    public class BinarySearchTree<T>
    {
        public TreeNodeWithParent<T> Root { get; }

        public IComparer<T> Comparer { get; }

        public BinarySearchTree(T[] nodes, IComparer<T> comparer = null)
        {
            Comparer = comparer ?? Comparer<T>.Default;
            if (nodes == null || nodes.Length == 0)
            {
                Root = null;
            }
            else
            {
                Root = From(nodes[0]);
                BuildTree(Root, 0, nodes);
            }
        }

        public TreeNodeWithParent<T> Search(T value)
        {
            TreeNodeWithParent<T> cur = Root;
            while (cur != null)
            {
                int c = Comparer.Compare(cur.Value, value);
                if (c == 0)
                {
                    return cur;
                }
                if (c > 0)
                {
                    cur = cur.Left;
                }
                else
                {
                    cur = cur.Right;
                }
            }
            return null;
        }

        public TreeNodeWithParent<T> Minimum()
        {
            throw new NotImplementedException();
        }

        public TreeNodeWithParent<T> Maximum()
        {
            throw new NotImplementedException();
        }

        public TreeNodeWithParent<T> Successor(TreeNodeWithParent<T> node)
        {
            throw new NotImplementedException();
        }

        public TreeNodeWithParent<T> Predecessor(TreeNodeWithParent<T> node)
        {
            throw new NotImplementedException();
        }

        public TreeNodeWithParent<T> Insert(TreeNodeWithParent<T> node)
        {
            throw new NotImplementedException();
        }

        public TreeNodeWithParent<T> Delete(TreeNodeWithParent<T> node)
        {
            throw new NotImplementedException();
        }

        private TreeNodeWithParent<T> BuildTree(TreeNodeWithParent<T> cur, int index, T[] nodes)
        {
            if (cur == null)
            {
                return null;
            }
            if (2 * index + 1 < nodes.Length)
            {
                cur.Left = BuildTree(From(nodes[2 * index + 1]), 2 * index + 1, nodes);
                cur.Left.Parent = cur;
            }
            if (2 * index + 2 < nodes.Length)
            {
                cur.Right = BuildTree(From(nodes[2 * index + 2]), 2 * index + 2, nodes);
                cur.Right.Parent = cur;
            }
            return cur;
        }

        private TreeNodeWithParent<T> From(T value)
        {
            if (Comparer.Compare(value, default(T)) == 0)
            {
                return null;
            }
            return new TreeNodeWithParent<T>
            {
                Value = value,
            };
        }
    }
}
