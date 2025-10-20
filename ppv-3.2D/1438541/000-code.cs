using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector
{
    public class RandomizedQuickSort : ISorter
    {
        private Random random = new Random();

        public void Sort<K>(K[] array, int index, int num, IComparer<K> comparer) where K : IComparable<K>
        {
            //Limit sort range.

            QuickSort(array, index, index + num - 1, comparer); 
        }

        private void QuickSort<K>(K[] array, int left, int right, IComparer<K> comparer) where K : IComparable<K>
        {
            //Recursion termination condition.

            if (left < right)
            {
                int pivotIndex = Partition(array, left, right, comparer);
                //Recursively process the left half.

                QuickSort(array, left, pivotIndex - 1, comparer);

                //Recursively process the right half.

                QuickSort(array, pivotIndex + 1, right, comparer);
            }
        }

        private int Partition<K>(K[] array, int left, int right, IComparer<K> comparer) where K : IComparable<K>
        {
            int randomPivot = random.Next(left, right + 1);

            //Swap the pivot to the end of the subarray.
            Swap(array, randomPivot, right);

            K pivot = array[right]; // Current value.

            int i = left - 1; // Record value of right.

            for (int j = left; j < right; j++)
            {
                //If it's current value is equal or smaller, so swap to the left side.

                if (comparer.Compare(array[j], pivot) <= 0)
                {
                    i++;
                    Swap(array, i, j);
                }
            }
            Swap(array, i + 1, right);

            return i + 1;
        }

        private void Swap<K>(K[] array, int i, int j)
        {
            K temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}

