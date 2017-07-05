namespace XuanLibrary.Test.TestDataStructure
{
    using System;
    using XuanLibrary.DataStructure.Common;

    internal class Node : IComparable<Node>, IEquatable<Node>, IHasKey<int>
    {
        public string Name { get; set; }

        public int Key { get; set; }

        public Node(string name, int key)
        {
            Name = name;
            Key = key;
        }

        public static explicit operator Node(string str)
        {
            string[] parts = str.Split('-');
            return new Node(parts[0], int.Parse(parts[1]));
        }

        public int CompareTo(Node other)
        {
            return this.Key - other.Key;
        }

        public bool Equals(Node other)
        {
            if (other == null)
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Name == other.Name && Key == other.Key;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Node);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ (Key.GetHashCode() << 1);
        }
    }
}
