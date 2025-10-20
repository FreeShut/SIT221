using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIT221_4._1P
{
    public interface ISorter
    {
        void Sort<K>(K[] array, int index, int num, IComparer<K> comparer) where K : IComparable<K>;
    }
    public class Vector<T> where T : IComparable<T>
    {
        private const int DEFAULT_CAPACITY = 10;
        private T[] data;

        public int Count { get; private set; } = 0;

        public int Capacity { get; private set; } = 0;

        public Vector(int capacity)
        {
            data = new T[capacity];
            Capacity = capacity;
        }
        public Vector() : this(DEFAULT_CAPACITY) { }
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new IndexOutOfRangeException();
                return data[index];
            }
            set
            {
                if (index < 0 || index >= Count)
                    throw new IndexOutOfRangeException();
                data[index] = value;
            }
        }
        private void ExtendData(int extraCapacity)
        {
            T[] newData = new T[data.Length + extraCapacity];
            for (int i = 0; i < Count; i++)
                newData[i] = data[i];
            data = newData;
            Capacity = data.Length;
        }
        public void Add(T element)
        {
            if (Count == data.Length)
                ExtendData(DEFAULT_CAPACITY);
            data[Count++] = element;
        }
        public int IndexOf(T element)
        {
            for (int i = 0; i < Count; i++)
            {
                if (data[i].Equals(element))
                    return i;
            }
            return -1;
        }


        public void Insert(int index, T element)
        {
            if (index < 0 || index > Count)
                throw new IndexOutOfRangeException();
            if (Count == data.Length)
                ExtendData(DEFAULT_CAPACITY);
            if (index == Count)
            {
                Add(element);
                return;
            }
            for (int i = Count; i > index; i--)
            {
                data[i] = data[i - 1];
            }
            data[index] = element;
            Count++;
        }


        public void Clear()
        {
            Array.Clear(data, 0, Count);
            Count = 0;
        }


        public bool Contains(T element)
        {
            return IndexOf(element) != -1;
        }


        public bool Remove(T element)
        {
            int index = IndexOf(element);
            if (index != -1)
            {
                RemoveAt(index);
                return true;
            }
            return false;
        }


        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();
            for (int i = index; i < Count - 1; i++)
            {
                data[i] = data[i + 1];
            }
            Count--;
            data[Count] = default(T);
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < Count; i++)
            {
                sb.Append(data[i]?.ToString());
                if (i < Count - 1)
                    sb.Append(", ");
            }
            sb.Append("]");
            return sb.ToString();
        }
        //public void Sort()
        //{
        //    Array.Sort(data, 0, Count);
        //}
        //public void Sort(IComparer<T> comparer)
        //{
        //    // if (comparer == null)
        //    //  throw new argumentnullexception(nameof(comparer));
        //    Array.Sort(data, 0, Count, comparer);
        //}
        public void Sort(ISorter algorithm, IComparer<T> comparer)
        {
            if (algorithm == null)
            {
                Array.Sort(data, 0, Count, comparer ?? Comparer<T>.Default);
            }
            else
            {
                algorithm.Sort<T>(data, 0, Count, comparer ?? Comparer<T>.Default);
            }
        }
        public class Student : IComparable<Student>
        {
            public string Name { get; set; }
            public int Id { get; set; }

            public override string ToString()
            {
                return $"{Id}[{Name}]";
            }

            public int CompareTo(Student other)
            {
                if (other == null) return 1;
                return Id.CompareTo(other.Id);
            }
        }
        public int BinarySearch(T item, IComparer<T> comparer)
        {
            IComparer<T> comp = comparer ?? Comparer<T>.Default;
            int left = 0;
            int right = Count - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                int comparison = comp.Compare(data[mid], item);

                if (comparison == 0)
                    return mid; 
                else if (comparison < 0)
                    left = mid + 1; 
                else
                    right = mid - 1; 
            }
            return -1; 
        }
    }

}


