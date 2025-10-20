using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vector;

namespace Vector
{
    public class BubbleSort : ISorter
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

            for (int i = 0; i < num -1 ; i++)
            {
                bool swapped = false;
                for (int j = index; j < index + num - i - 1; j++)
                {
                    if (comp.Compare(array[j], array[j + 1]) > 0)
                    {
                        (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        swapped = true;
                    }
                }
                if (!swapped) break;
            }
        }
    }
}
