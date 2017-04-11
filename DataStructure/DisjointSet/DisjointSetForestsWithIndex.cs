namespace XuanLibrary.DataStructure.DisjointSet
{
    using System;
    using System.Collections.Generic;

    public class DisjointSetForestsWithIndex : IDisjointSet<int>
    {
        private int[] _parents;
        private int[] _ranks;

        /// <summary>
        /// find the set of an element with specified index
        /// </summary>
        /// <param name="x">the index of an element. The index is zero based.</param>
        /// <returns>the representative element's index</returns>
        public int FindSet(int x)
        {
            if (_parents[x] != x)
            {
                _parents[x] = FindSet(_parents[x]);
            }
            return _parents[x];
        }

        /// <summary>
        /// make n sets for n elements
        /// </summary>
        /// <param name="n">the number of elements</param>
        public void MakeSet(int n)
        {
            _parents = new int[n];
            _ranks = new int[n];

            for (int i = 0; i < n; i++)
            {
                _parents[i] = i;
                _ranks[i] = 0;
            }
        }

        /// <summary>
        /// union two sets that two elements reside in
        /// </summary>
        /// <param name="x">the index of an element</param>
        /// <param name="y">the index of another element</param>
        public void Union(int x, int y)
        {
            Link(FindSet(x), FindSet(y));
        }

        private void Link(int x, int y)
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
