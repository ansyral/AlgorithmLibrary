namespace XuanLibrary.Test.TestDataStructure
{
    using System;

    internal class Node : IComparable<Node>, IEquatable<Node>
    {
        public string Name { get; set; }

        public int Value { get; set; }

        public Node(string name, int val)
        {
            Name = name;
            Value = val;
        }

        public static explicit operator Node(string str)
        {
            string[] parts = str.Split('-');
            return new Node(parts[0], int.Parse(parts[1]));
        }

        public int CompareTo(Node other)
        {
            return this.Value - other.Value;
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
            return Name == other.Name && Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Node);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ (Value.GetHashCode() << 1);
        }
    }
}
