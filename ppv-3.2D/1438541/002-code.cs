using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector
{
    public class MergeSortBottomUp : ISorter
    {
        
        public void Sort<K>(K[] array, int index, int num, IComparer<K> comparer) where K : IComparable<K>
        {
            int n = index + num; //Get the position of the last element to be sorted.
            for (int size = 1; size < n; size *= 2)// Outer loop
            {
                for (int left = index; left < n; left += 2 * size)
                {
                    //Determine the middle and right limits of the current two subsequences respectively.
                    int mid = Math.Min(left + size - 1, n - 1);
                    int right = Math.Min(left + 2 * size - 1, n - 1);
                    //Merge two adjacent subarrays.
                    Merge(array, left, mid, right, comparer);
                }
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
