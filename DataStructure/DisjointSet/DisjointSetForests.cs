namespace XuanLibrary.DataStructure.DisjointSet
{
    using System;
    using System.Collections.Generic;

    public class DisjointSetForests<T> : IDisjointSet<T>
    {
        private Dictionary<T, T> _parents = new Dictionary<T, T>();
        private Dictionary<T, int> _ranks = new Dictionary<T, int>();

        /// <summary>
        /// find the set
        /// </summary>
        /// <param name="x">element</param>
        /// <returns>the representative of the set</returns>
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

        /// <summary>
        /// make a set that contains an element
        /// </summary>
        /// <param name="x">element</param>
        public void MakeSet(T x)
        {
            _parents[x] = x;
            _ranks[x] = 0;
        }

        /// <summary>
        /// union two sets that two elements reside in
        /// </summary>
        /// <param name="x">an element</param>
        /// <param name="y">another element</param>
        public void Union(T x, T y)
        {
            T sx = FindSet(x);
            T sy = FindSet(y);
            if (!sx.Equals(sy))
            {
                Link(sx, sy);
            }
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
