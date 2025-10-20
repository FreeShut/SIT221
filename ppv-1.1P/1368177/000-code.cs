using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector
{
    public class Vector<T>
    {
        // This constant determines the default number of elements in a newly created vector.
        // It is also used to extended the capacity of the existing vector
        private const int DEFAULT_CAPACITY = 10;

        // This array represents the internal data structure wrapped by the vector class.
        // In fact, all the elements are to be stored in this private  array. 
        // You will just write extra functionality (methods) to make the work with the array more convenient for the user.
        private T[] data;

        // This property represents the number of elements in the vector
        public int Count { get; private set; } = 0;

        // This property represents the maximum number of elements (capacity) in the vector
        public int Capacity { get; private set; } = 0;

        // This is an overloaded constructor
        public Vector(int capacity)
        {
            data = new T[capacity];
        }

        // This is the implementation of the default constructor
        public Vector() : this(DEFAULT_CAPACITY) { }

        // An Indexer is a special type of property that allows a class or structure to be accessed the same way as array for its internal collection. 
        // For example, introducing the following indexer you may address an element of the vector as vector[i] or vector[0] or ...
        public T this[int index]
        {
            get
            {
                if (index >= Count || index < 0) throw new IndexOutOfRangeException();
                return data[index];
            }
            set
            {
                if (index >= Count || index < 0) throw new IndexOutOfRangeException();
                data[index] = value;
            }
        }

        // This private method allows extension of the existing capacity of the vector by another 'extraCapacity' elements.
        // The new capacity is equal to the existing one plus 'extraCapacity'.
        // It copies the elements of 'data' (the existing array) to 'newData' (the new array), and then makes data pointing to 'newData'.
        private void ExtendData(int extraCapacity)
        {
            T[] newData = new T[data.Length + extraCapacity];
            for (int i = 0; i < Count; i++) newData[i] = data[i];
            data = newData;
        }

        // This method adds a new element to the existing array.
        // If the internal array is out of capacity, its capacity is first extended to fit the new element.
        public void Add(T element)
        {
            if (Count == data.Length) ExtendData(DEFAULT_CAPACITY);
            data[Count++] = element;
        }

        // This method searches for the specified object and returns the zero‐based index of the first occurrence within the entire data structure.
        // This method performs a linear search; therefore, this method is an O(n) runtime complexity operation.
        // If occurrence is not found, then the method returns –1.
        // Note that Equals is the proper method to compare two objects for equality, you must not use operator '=' for this purpose.
        public int IndexOf(T element)
        {
            for (var i = 0; i < Count; i++)
            {
                if (data[i].Equals(element)) return i;
            }
            return -1;
        }

        // TODO:********************************************************************************************
        // TODO: Your task is to implement all the remaining methods.
        // Read the instruction carefully, study the code examples from above as they should help you to write the rest of the code.
        public void Insert(int index, T element)
        {
            if (index > Count || index < 0) throw new IndexOutOfRangeException();
            if (Count == Capacity) ExtendData(DEFAULT_CAPACITY);
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
            //Count = 0;
            //data = new T[DEFAULT_CAPACITY];

            Array.Clear(data, 0, Count);// this code will clean the references in array
            Count = 0;  // set the count to 0
            
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
            if (index >= Count || index < 0) throw new IndexOutOfRangeException();
            for (int i = index; i < Count - 1; i++)
            {
                data[i] = data[i + 1];
            }
            Count--;

            data[Count] = default(T);
            // To clear the redundant references, recycle the data that no longer used.

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < Count; i++)
            {
                sb.Append(data[i]?.ToString());
                if (i < Count - 1) sb.Append(", ");
            }
            sb.Append("]");
            return sb.ToString();
        }




    }
    class Tester
    {

        private static bool CheckIntSequence(int[] certificate, Vector<int> vector)
        {
            if (certificate.Length != vector.Count) return false;
            for (int i = 0; i < certificate.Length; i++)
            {
                if (certificate[i] != vector[i]) return false;
            }
            return true;
        }

        static void Main(string[] args)
        {
            Vector<int> vector = null;
            string result = "";

            // test 1
            try
            {
                Console.WriteLine("\nTest A: Create a new vector by calling 'Vector<int> vector = new Vector<int>(50);'");
                vector = new Vector<int>(50);
                Console.WriteLine(" :: SUCCESS");
                result = result + "A";
            }
            catch (Exception exception)
            {
                Console.WriteLine(" :: FAIL");
                Console.WriteLine(exception.ToString());
                result = result + "-";
            }

            // test 2
            try
            {
                Console.WriteLine("\nTest B: Add a sequence of numbers 2, 6, 8, 5, 5, 1, 8, 5, 3, 5");
                vector.Add(2); vector.Add(6); vector.Add(8); vector.Add(5); vector.Add(5); vector.Add(1); vector.Add(8); vector.Add(5); vector.Add(3); vector.Add(5);
                if (!CheckIntSequence(new int[] { 2, 6, 8, 5, 5, 1, 8, 5, 3, 5 }, vector)) throw new Exception("Vector stores incorrect sequence of integers");
                Console.WriteLine(" :: SUCCESS");
                result = result + "B";
            }
            catch (Exception exception)
            {
                Console.WriteLine(" :: FAIL");
                Console.WriteLine(exception.ToString());
                result = result + "-";
            }

            // test 3
            try
            {
                Console.WriteLine("\nTest C: Remove number 3, 7, and then 6");
                bool check = vector.Remove(3) && (!vector.Remove(7)) && vector.Remove(6);
                if (!CheckIntSequence(new int[] { 2, 8, 5, 5, 1, 8, 5, 5 }, vector)) throw new Exception("Vector stores incorrect sequence of integers");
                Console.WriteLine(" :: SUCCESS");
                Console.WriteLine(vector.ToString());
                result = result + "C";
            }
            catch (Exception exception)
            {
                Console.WriteLine(" :: FAIL");
                Console.WriteLine(exception.ToString());
                result = result + "-";
            }

            // test 4
            try
            {
                Console.WriteLine("\nTest D: Insert number 50 at index 6, then number 0 at index 0, then number 60 at index 'vector.Count-1', then number 70 at index 'vector.Count'");
                vector.Insert(6, 50); vector.Insert(0, 0); vector.Insert(vector.Count - 1, 60); vector.Insert(vector.Count, 70);
                if (!CheckIntSequence(new int[] { 0, 2, 8, 5, 5, 1, 8, 50, 5, 60, 5, 70 }, vector)) throw new Exception("Vector stores incorrect sequence of integers");
                Console.WriteLine(" :: SUCCESS");
                Console.WriteLine(vector.ToString());
                result = result + "D";
            }
            catch (Exception exception)
            {
                Console.WriteLine(" :: FAIL");
                Console.WriteLine(exception.ToString());
                result = result + "-";
            }


            // test 5
            try
            {
                Console.WriteLine("\nTest E: Insert number -1 at index 'vector.Count+1'");
                vector.Insert(vector.Count + 1, -1);

            }
            catch (IndexOutOfRangeException exception)
            {
                Console.WriteLine(" :: SUCCESS");
                result = result + "E";
            }
            catch (Exception exception)
            {
                Console.WriteLine(" :: FAIL");
                Console.WriteLine("Last operation is invalid and must throw IndexOutOfRangeException. Your solution does not match specification.");
                result = result + "-";
            }
            Console.WriteLine(vector);

            // test 6
            try
            {
                Console.WriteLine("\nTest F: Remove number at index 4, then number index 0, and then number at index 'vector.Count-1'");
                vector.RemoveAt(4); vector.RemoveAt(0); vector.RemoveAt(vector.Count - 1);
                if (!CheckIntSequence(new int[] { 2, 8, 5, 1, 8, 50, 5, 60, 5 }, vector)) throw new Exception("Vector stores incorrect sequence of integers");
                Console.WriteLine(" :: SUCCESS");
                result = result + "F";
            }
            catch (Exception exception)
            {
                Console.WriteLine(" :: FAIL");
                Console.WriteLine(exception.ToString());
                result = result + "-";
            }
            Console.WriteLine(vector);

            // test 7
            try
            {
                Console.WriteLine("\nTest G: Remove number at index 'vector.Count'");
                vector.RemoveAt(vector.Count);
            }
            catch (IndexOutOfRangeException exception)
            {
                Console.WriteLine(" :: SUCCESS");
                result = result + "G";
            }
            catch (Exception exception)
            {
                Console.WriteLine(" :: FAIL");
                Console.WriteLine("Last operation is invalid and must throw IndexOutOfRangeException. Your solution does not match specification.");
                result = result + "-";
            }

            // test 8
            try
            {
                Console.WriteLine("\nTest H: Run a sequence of operations: ");

                Console.WriteLine("vector.Contains(1);");
                if (vector.Contains(1)) Console.WriteLine(" :: SUCCESS");
                else throw new Exception("1 must be in the vector");

                Console.WriteLine("vector.Contains(2);");
                if (vector.Contains(2)) Console.WriteLine(" :: SUCCESS");
                else throw new Exception("2 must be in the vector");

                Console.WriteLine("vector.Contains(4);");
                if (!vector.Contains(4)) Console.WriteLine(" :: SUCCESS");
                else throw new Exception("4 must not be in the vector");

                Console.WriteLine("vector.Add(4); vector.Contains(4);");
                vector.Add(4);
                if (vector.Contains(4)) Console.WriteLine(" :: SUCCESS");
                else throw new Exception("4 must be in the vector");

                result = result + "H";
            }
            catch (Exception exception)
            {
                Console.WriteLine(" :: FAIL");
                Console.WriteLine(exception.ToString());
                result = result + "-";
            }

            // test 9
            try
            {
                Console.WriteLine("\nTest I: Print the content of the vector via calling vector.ToString();");
                Console.WriteLine(vector.ToString());
                Console.WriteLine(" :: SUCCESS");
                result = result + "I";
            }
            catch (Exception exception)
            {
                Console.WriteLine(" :: FAIL");
                Console.WriteLine(exception.ToString());
                result = result + "-";
            }

            // test 10
            try
            {
                Console.WriteLine("\nTest J: Clear the content of the vector via calling vector.Clear();");
                vector.Clear();
                if (!CheckIntSequence(new int[] { }, vector)) throw new Exception("Vector stores incorrect data. It must be empty.");
                Console.WriteLine(" :: SUCCESS");
                result = result + "J";
            }
            catch (Exception exception)
            {
                Console.WriteLine(" :: FAIL");
                Console.WriteLine(exception.ToString());
                result = result + "-";
            }

            Console.WriteLine("\n\n ------------------- SUMMARY ------------------- ");
            Console.WriteLine("Tests passed: " + result);
            Console.ReadKey();
        }
    }
}
