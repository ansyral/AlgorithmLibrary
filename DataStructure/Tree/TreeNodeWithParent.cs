namespace XuanLibrary.DataStructure.Tree
{
    public class TreeNodeWithParent<T>
    {
        public T Value { get; set; }

        public TreeNodeWithParent<T> Left { get; set; }

        public TreeNodeWithParent<T> Right { get; set; }

        public TreeNodeWithParent<T> Parent { get; set; }
    }
}
