using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vector;

namespace Vector
{
    public class InsertionSort : ISorter
    {
        public void Sort<K>(K[] array, int index, int num, IComparer<K> comparer) where K : IComparable<K>
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (num < 0)
                throw new ArgumentOutOfRangeException(nameof(num));
            if (index + num > array.Length)
                throw new ArgumentException("index and num exceed array bounds");


            IComparer<K> comp = comparer ?? Comparer<K>.Default;

            for (int i = index + 1; i < index + num; i++)
            {
                K current = array[i];
                int j = i - 1;
                while (j >= index && comp.Compare(array[j], current) > 0)
                {
                    array[j + 1] = array[j];
                    j--;
                }
                array[j + 1] = current;
            }
        }
    }
}

