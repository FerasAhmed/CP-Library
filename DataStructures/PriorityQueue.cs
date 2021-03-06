﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace AlgorithmsLibrary.DataStructures
{
    /// <summary>
    /// Min Priority Queue Implemented
    /// Decrease Function Takes O(V) Due to the search for Required VertexName
    /// Extract  Function Takes O(Lg V)
    /// </summary>
    /// <typeparam name="TElement">Any kind of Objects</typeparam>
    /// <typeparam name="TKey">Real Numbers</typeparam>
    class PriorityQueue<TElement,TKey>
    {
        //Store Vertices in Queue
        List<TKey> VertexKey=new List<TKey> ();
        List<TElement> VertexElement = new List<TElement>();
        //Store Vertices After Removed From Queue
        List<TKey> TempVertexKey = new List<TKey>();
        List<TElement> TempVertexElement = new List<TElement>();

        public PriorityQueue(TElement[] Elements, TKey[] Keys)
        {
            if (Elements.Length != Keys.Length) throw new Exception("Invalid Data Count");

            for (int i = 0; i < Keys.Length; i++)
            {
                VertexKey.Add(Keys[i]);
                VertexElement.Add(Elements[i]);
            }

            for (int i = VertexKey.Count / 2; i >= 0; i--)
                MinHepify(i);
        }

        public PriorityQueue(List<TElement> Elements,List<TKey> Keys)
        {
            if(Elements.Count!=Keys.Count) throw new Exception("Invalid Data Count");

            for (int i = 0; i < Keys.Count; i++)
            {
                //Vertices.Add(Elements[i],i);
                VertexKey.Add(Keys[i]);
                VertexElement.Add(Elements[i]);
            }

            for(int i=VertexKey.Count/2;i>=0;i--) 
                MinHepify(i);
        }

        public void Insert(TElement Element,TKey Cost)
        {
            VertexElement.Add(Element);
            VertexKey.Add(Cost);
            int _index = VertexKey.Count - 1;
            
            while (_index != 0 && Compare(VertexKey[Parent(_index)], VertexKey[_index]) > 0)
            {
                Swap(Parent(_index), _index);
                _index = Parent(_index);
            }
        }

        public KeyValuePair<TElement,TKey> Minimum()
        {
            if (VertexKey.Count < 1) throw new Exception("Queue is Empty !.");

            KeyValuePair<TElement, TKey> KvPair = new KeyValuePair<TElement, TKey>(VertexElement[0], VertexKey[0]);

            return KvPair;
        }

        public KeyValuePair<TElement,TKey> Extract_Minimum()
        {
            if (VertexKey.Count < 1) throw new Exception("Queue is Empty !.");

            KeyValuePair<TElement, TKey> KvPair = new KeyValuePair<TElement, TKey>(VertexElement[0], VertexKey[0]);

            TempVertexKey.Add(VertexKey[0]);
            TempVertexElement.Add(VertexElement[0]);

            int _Last=VertexElement.Count - 1;
            VertexElement[0] = VertexElement[_Last];
            VertexKey[0] = VertexKey[_Last];

            VertexElement.RemoveAt(_Last);
            VertexKey.RemoveAt(_Last);

            MinHepify(0);

            return KvPair;
        }
 
        public void Decrease_Key(TElement Element, TKey Cost)
        {
            bool _Found = false;
            int _Index = -1;

            for (int i = 0; i < VertexElement.Count; i++)
            {
                if (Compare(Element,VertexElement[i])==0)
                {
                    VertexKey[i] = Cost;
                    _Index = i;
                    _Found = true;
                    break;
                }
            }

            if (!_Found) throw new Exception("No Element With given Name.");

            while (_Index != 0 && Compare(VertexKey[Parent(_Index)],VertexKey[_Index])>0)
            {
                Swap(Parent(_Index), _Index);
                _Index = Parent(_Index);
            }
        }

        public void MinHepify(int i)
        {
            int L = 2 * i + 1;
            int R = 2 * i + 2;
            int Minimum = -1;

            if (L < VertexKey.Count && Compare(VertexKey[L], VertexKey[i]) < 0)
                Minimum = L;
            else
                Minimum = i;

            if (R < VertexKey.Count && Compare(VertexKey[R], VertexKey[Minimum]) < 0)
                Minimum = R;
            
            if (Minimum != i)
            {
                Swap(i, Minimum);
                MinHepify(Minimum);
            }
        }

        public int Count
        {
            get { return VertexKey.Count; }
        }

        public int Parent(int i)
        {
            if (i % 2 == 0)
                return (i - 2) / 2;
            else
                return (i - 1) / 2;
        }

        public void Swap(int i, int j)
        {
            TKey Temp = VertexKey[i];
            VertexKey[i] = VertexKey[j];
            VertexKey[j] = Temp;

            TElement TempElement = VertexElement[i];
            VertexElement[i] = VertexElement[j];
            VertexElement[j] = TempElement;

        }

        public List<TElement> Vertices
        {
            get { return VertexElement; }
        }
       
        public List<TKey> keys
        {
            get { return VertexKey; }
        }

        public int Compare(object x, object y)
        {
            if (x is int)
            {
                int X = (int)x;
                int Y = (int)y;

                if (X == Y) return 0;
                if (X > Y) return 2;
                else
                    return -2;
            }
            else
                if (x is string)
                {
                    string X = (string)x;
                    string Y = (string)y;

                    return X.CompareTo(Y);
                }
                else
                    throw new Exception("This Kind Not Implented int ICompare.");
        }

        public TKey this[TElement element]
        {
            get
            {
                for (int i = 0; i < VertexElement.Count; i++)
                    if (Compare(VertexElement[i], element) == 0)
                        return VertexKey[i];
                
                //search Vertices removed from queue
                for (int i = 0; i < TempVertexElement.Count; i++)
                    if (Compare(TempVertexElement[i], element) == 0)
                        return TempVertexKey[i];

                throw new Exception("Error");
            }

        }
    }
}
