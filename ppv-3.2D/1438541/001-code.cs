using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector
{
    public class MergeSortTopDown : ISorter
    {
        public void Sort<K>(K[] array, int index, int num, IComparer<K> comparer) where K : IComparable<K>
        {
            //Limit sort range.

            MergeSort(array, index, index + num - 1, comparer);
        }

        private void MergeSort<K>(K[] array, int left, int right, IComparer<K> comparer) where K : IComparable<K>
        {
            //Recursion termination condition.
            if (left < right)
            {
                int mid = (left + right) / 2; // Mid point.
                MergeSort(array, left, mid, comparer); //Sorting left side.
                MergeSort(array, mid + 1, right, comparer);//Sorting right side.
                Merge(array, left, mid, right, comparer);// All together.
            }
        }

        private void Merge<K>(K[] array, int left, int mid, int right, IComparer<K> comparer)
        {
            // Create a temporary array to store data.
            K[] temp = new K[right - left + 1];
            int i = left, j = mid + 1, k = 0;

            while (i <= mid && j <= right)
            {
                // Compare of the two elements , put samll one int to temporary array.
                if (comparer.Compare(array[i], array[j]) <= 0)
                    temp[k++] = array[i++];
                else
                    temp[k++] = array[j++];
            }
            
            // Get the rest of the value in left.
            while (i <= mid) temp[k++] = array[i++];

            // Get the rest of the value in right.
            while (j <= right) temp[k++] = array[j++];

            //Copy the temporary array in to orignal array.
            Array.Copy(temp, 0, array, left, temp.Length);
        }
    }
}
