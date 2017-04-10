namespace XuanLibrary.DataStructure.DisjointSet
{
    using System;
    using System.Collections.Generic;

    public class DisjointSetForests<T> : IDisjointSet<T>
    {
        private Dictionary<T, T> _parents = new Dictionary<T, T>();
        private Dictionary<T, int> _ranks = new Dictionary<T, int>();

        public T FindSet(T x)
        {
            if (x == null)
            {
                throw new ArgumentNullException(nameof(x));
            }
            if (!_parents[x].Equals(x))
            {
                _parents[x] = FindSet(_parents[x]);
            }
            return _parents[x];
        }

        public void MakeSet(T x)
        {
            _parents[x] = x;
            _ranks[x] = 0;
        }

        public void Union(T x, T y)
        {
            Link(FindSet(x), FindSet(y));
        }

        private void Link(T x, T y)
        {
            int rx = _ranks[x];
            int ry = _ranks[y];
            if (rx > ry)
            {
                _parents[y] = x;
            }
            else
            {
                _parents[x] = y;
                if (rx == ry)
                {
                    _ranks[y] = ry + 1;
                }
            }

        }
    }
}
