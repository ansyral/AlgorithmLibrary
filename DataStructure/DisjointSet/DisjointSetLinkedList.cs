namespace XuanLibrary.DataStructure.DisjointSet
{
    using System;
    using System.Collections.Generic;

    public class DisjointSetLinkedList<T> : IDisjointSet<T> where T : class
    {
        private Dictionary<T, T> _representatives = new Dictionary<T, T>();
        private Dictionary<T, T> _nexts = new Dictionary<T, T>();
        private Dictionary<T, T> _tails = new Dictionary<T, T>();
        private Dictionary<T, int> _lens = new Dictionary<T, int>();

        public T FindSet(T x)
        {
            if (x == null)
            {
                throw new ArgumentNullException(nameof(x));
            }
            return _representatives[x];
        }

        public void MakeSet(T x)
        {
            _representatives[x] = x;
            _lens[x] = 1;
            _nexts[x] = null;
            _tails[x] = x;
        }

        public void Union(T x, T y)
        {
            Link(FindSet(x), FindSet(y));
        }

        private void Link(T x, T y)
        {
            int lx = _lens[x];
            int ly = _lens[y];
            if (lx > ly)
            {
                Append(x, y);
            }
            else
            {
                Append(y, x);
            }

        }

        private void Append(T x, T y)
        {
            _nexts[_tails[x]] = y;
            _tails[x] = _tails[y];
            _lens[x] += _lens[y];
            var next = y;
            while (next != null)
            {
                _representatives[next] = x;
                next = _nexts[next];
            }
        }
    }
}
