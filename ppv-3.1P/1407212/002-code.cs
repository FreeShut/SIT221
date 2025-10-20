using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vector;

namespace Vector
{
    public class SelectionSort : ISorter
    {
        public void Sort<K>(K[] array, int index, int num, IComparer<K> comparer) where K : IComparable<K>
        {
            IComparer<K> comp = comparer ?? Comparer<K>.Default;

            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (num < 0)
                throw new ArgumentOutOfRangeException(nameof(num));
            if (index + num > array.Length)
                throw new ArgumentException("index and num exceed array bounds");

            for (int i = index; i < index + num - 1; i++)
            {
                int minIdx = i;
                for (int j = i + 1; j < index + num; j++)
                {
                    if (comp.Compare(array[j], array[minIdx]) < 0)
                        minIdx = j;
                }
                (array[i], array[minIdx]) = (array[minIdx], array[i]);
            }
        }
    }
}
