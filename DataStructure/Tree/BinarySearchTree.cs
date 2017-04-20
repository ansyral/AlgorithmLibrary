namespace XuanLibrary.DataStructure.Tree
{
    using System;
    using System.Collections.Generic;

    public class BinarySearchTree<T>
    {
        public TreeNodeWithParent<T> Root { get; private set; }

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
                if (Root != null)
                {
                    var queue = new Queue<TreeNodeWithParent<T>>();
                    queue.Enqueue(Root);
                    BuildTree(queue, nodes, 1);
                }
            }
        }

        private void BuildTree(Queue<TreeNodeWithParent<T>> queue, T[] nodes, int i)
        {
            while (queue.Count > 0)
            {
                var cur = queue.Dequeue();
                if (i >= nodes.Length)
                {
                    break;
                }
                cur.Left = From(nodes[i++]);
                if (i >= nodes.Length)
                {
                    break;
                }
                cur.Right = From(nodes[i++]);
                if (cur.Left != null)
                {
                    queue.Enqueue(cur.Left);
                    cur.Left.Parent = cur;
                }
                if (cur.Right != null)
                {
                    queue.Enqueue(cur.Right);
                    cur.Right.Parent = cur;
                }
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

        public TreeNodeWithParent<T> Minimum(TreeNodeWithParent<T> root)
        {
            var cur = root;
            if (cur == null)
            {
                return null;
            }
            while (cur.Left != null)
            {
                cur = cur.Left;
            }
            return cur;
        }

        public TreeNodeWithParent<T> Maximum(TreeNodeWithParent<T> root)
        {
            var cur = root;
            if (cur == null)
            {
                return null;
            }
            while (cur.Right != null)
            {
                cur = cur.Right;
            }
            return cur;
        }

        public TreeNodeWithParent<T> Successor(TreeNodeWithParent<T> node)
        {
            var cur = node;
            if (cur == null)
            {
                return null;
            }
            if (cur.Right != null)
            {
                return Minimum(cur.Right);
            }
            while (cur.Parent != null)
            {
                if (cur == cur.Parent.Left)
                {
                    return cur.Parent;
                }
                cur = cur.Parent;
            }
            return null;
        }

        public TreeNodeWithParent<T> Predecessor(TreeNodeWithParent<T> node)
        {
            var cur = node;
            if (cur == null)
            {
                return null;
            }
            if (cur.Left != null)
            {
                return Maximum(cur.Left);
            }
            while (cur.Parent != null)
            {
                if (cur == cur.Parent.Right)
                {
                    return cur.Parent;
                }
                cur = cur.Parent;
            }
            return null;
        }

        public TreeNodeWithParent<T> Insert(TreeNodeWithParent<T> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }
            var cur = Root;
            TreeNodeWithParent<T> parent = null;
            while (cur != null)
            {
                parent = cur;
                int c = Comparer.Compare(cur.Value, node.Value);
                if (c > 0)
                {
                    cur = cur.Left;
                }
                else
                {
                    cur = cur.Right;
                }
            }
            node.Parent = parent;
            if (parent == null)
            {
                Root = node;
            }
            else if (Comparer.Compare(parent.Value, node.Value) > 0)
            {
                parent.Left = node;
            }
            else
            {
                parent.Right = node;
            }
            return parent;
        }

        public TreeNodeWithParent<T> Delete(TreeNodeWithParent<T> node)
        {
            var deleted = node;
            if (deleted == Root)
            {
                Root = null;
                return deleted;
            }
            if (deleted.Left != null && deleted.Right != null)
            {
                var item = Successor(deleted);
                T tmp = deleted.Value;
                deleted.Value = item.Value;
                item.Value = tmp;
                deleted = item;
            }
            var child = deleted.Left ?? deleted.Right;
            if (child != null)
            {
                child.Parent = deleted.Parent;
            }
            if (deleted == deleted.Parent.Left)
            {
                deleted.Parent.Left = child;
            }
            else
            {
                deleted.Parent.Right = child;
            }
            return deleted;
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
