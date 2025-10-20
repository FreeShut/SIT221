using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heap
{
    public class Heap<K, D> where K : IComparable<K>
    {

        // This is a nested Node class whose purpose is to represent a node of a heap.
        private class Node : IHeapifyable<K, D>
        {
            // The Data field represents a payload.
            public D Data { get; set; }
            // The Key field is used to order elements with regard to the Binary Min (Max) Heap Policy, i.e. the key of the parent node is smaller (larger) than the key of its children.
            public K Key { get; set; }
            // The Position field reflects the location (index) of the node in the array-based internal data structure.
            public int Position { get; set; }

            public Node(K key, D value, int position)
            {
                Data = value;
                Key = key;
                Position = position;
            }

            // This is a ToString() method of the Node class.
            // It prints out a node as a tuple ('key value','payload','index')}.
            public override string ToString()
            {
                return "(" + Key.ToString() + "," + Data.ToString() + "," + Position + ")";
            }
        }

        // ---------------------------------------------------------------------------------
        // Here the description of the methods and attributes of the Heap<K, D> class starts

        public int Count { get; private set; }

        // The data nodes of the Heap<K, D> are stored internally in the List collection. 
        // Note that the element with index 0 is a dummy node.
        // The top-most element of the heap returned to the user via Min() is indexed as 1.
        private List<Node> data = new List<Node>();

        // We refer to a given comparer to order elements in the heap. 
        // Depending on the comparer, we may get either a binary Min-Heap or a binary  Max-Heap. 
        // In the former case, the comparer must order elements in the ascending order of the keys, and does this in the descending order in the latter case.
        private IComparer<K> comparer;

        // We expect the user to specify the comparer via the given argument.
        public Heap(IComparer<K> comparer)
        {
            this.comparer = comparer;

            // We use a default comparer when the user is unable to provide one. 
            // This implies the restriction on type K such as 'where K : IComparable<K>' in the class declaration.
            if (this.comparer == null) this.comparer = Comparer<K>.Default;

            // We simplify the implementation of the Heap<K, D> by creating a dummy node at position 0.
            // This allows to achieve the following property:
            // The children of a node with index i have indices 2*i and 2*i+1 (if they exist).
            data.Add(new Node(default(K), default(D), 0));
        }

        // This method returns the top-most (either a minimum or a maximum) of the heap.
        // It does not delete the element, just returns the node casted to the IHeapifyable<K, D> interface.
        public IHeapifyable<K, D> Min()
        {
            if (Count == 0) throw new InvalidOperationException("The heap is empty.");
            return data[1];
        }

        // Insertion to the Heap<K, D> is based on the private UpHeap() method
        public IHeapifyable<K, D> Insert(K key, D value)
        {
            Count++;
            Node node = new Node(key, value, Count);
            data.Add(node);
            UpHeap(Count);
            return node;
        }

        private void UpHeap(int start)
        {
            int position = start;
            while (position != 1)
            {
                if (comparer.Compare(data[position].Key, data[position / 2].Key) < 0) Swap(position, position / 2);
                position = position / 2;
            }
        }

        // This method swaps two elements in the list representing the heap. 
        // Use it when you need to swap nodes in your solution, e.g. in DownHeap() that you will need to develop.
        private void Swap(int from, int to)
        {
            Node temp = data[from];
            data[from] = data[to];
            data[to] = temp;
            data[to].Position = to;
            data[from].Position = from;
        }

        public void Clear()
        {
            for (int i = 0; i <= Count; i++) data[i].Position = -1;
            data.Clear();
            data.Add(new Node(default(K), default(D), 0));
            Count = 0;
        }

        public override string ToString()
        {
            if (Count == 0) return "[]";
            StringBuilder s = new StringBuilder();
            s.Append("[");
            for (int i = 0; i < Count; i++)
            {
                s.Append(data[i + 1]);
                if (i + 1 < Count) s.Append(",");
            }
            s.Append("]");
            return s.ToString();
        }

        // TODO: Your task is to implement all the remaining methods.
        // Read the instruction carefully, study the code examples from above as they should help you to write the rest of the code.
        public IHeapifyable<K, D> Delete()
        {
            if (Count == 0) throw new InvalidOperationException("The heap is empty.");
            IHeapifyable<K, D> min = data[1];
            data[1] = data[Count];
            data[1].Position = 1;
            data.RemoveAt(Count);
            Count--;
            if (Count > 0) DownHeap(1);
            return min;
        }

        // Builds a minimum binary heap using the specified data according to the bottom-up approach.
        private void DownHeap(int start)
        {
            int current = start;
            while (true)
            {
                int left = 2 * current;
                int right = 2 * current + 1;
                int smallest = current;

                if (left <= Count && comparer.Compare(data[left].Key, data[smallest].Key) < 0)
                    smallest = left;
                if (right <= Count && comparer.Compare(data[right].Key, data[smallest].Key) < 0)
                    smallest = right;

                if (smallest == current) break;

                Swap(current, smallest);
                current = smallest;
            }
        }
        public IHeapifyable<K, D>[] BuildHeap(K[] keys, D[] data)
        {
            if (Count != 0) throw new InvalidOperationException("Heap is not empty.");
            if (keys.Length != data.Length) throw new ArgumentException("Keys and data arrays must be of the same length.");

            Count = keys.Length;
            List<Node> newNodes = new List<Node>(keys.Select((k, i) => new Node(k, data[i], i + 1)));
            this.data = new List<Node> { new Node(default(K), default(D), 0) };
            this.data.AddRange(newNodes);

            for (int i = Count / 2; i >= 1; i--)
                DownHeap(i);

            return newNodes.Cast<IHeapifyable<K, D>>().ToArray();
        }

        public void DecreaseKey(IHeapifyable<K, D> element, K new_key)
        {
            Node node = element as Node;
            if (node == null || node.Position > Count || data[node.Position] != node)
                throw new InvalidOperationException("Element is inconsistent with the heap.");

            if (comparer.Compare(new_key, node.Key) >= 0)
                throw new ArgumentException("New key is not smaller than the current key.");

            node.Key = new_key;
            UpHeap(node.Position);
        }

    }
}
