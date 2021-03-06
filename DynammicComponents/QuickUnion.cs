﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmsLibrary.DynammicComponents
{
    class QuickUnion
    {
        int[] id;
        int[] size;
        int count;
        public QuickUnion(int N)
        {
            count = N;
            id = new int[N];
            size = new int[N];
            for (int i = 0; i < N; i++)
            {
                id[i] = i;
                size[i] = 1;
            }
        }

        public int root(int p)
        {
            while (p != id[p])
            {
                //Compression 
                id[p] = id[id[p]];
                p = id[p];
            }
            return p;
        }

        public int Count() { return count; }

        public bool Find(int p, int q)
        {
            return root(p) == root(q);
        }

        public void Union(int p, int q)
        {
            int pRoot = root(p);
            int qRoot = root(q);
            
            if (pRoot == qRoot) return;
            if (size[pRoot] < size[qRoot])
            {
                id[pRoot] = qRoot;
                size[qRoot] += size[pRoot];
            }
            else
            {
                id[qRoot] = pRoot;
                size[pRoot] += size[qRoot];
            }
            // decrease connected components
            count--;
        }
    }
}
