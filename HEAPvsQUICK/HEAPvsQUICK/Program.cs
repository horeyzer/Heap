using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HEAPvsQUICK
{
    internal class Program
    {
        class SortingAlgorithms
        {

            static void Main(string[] args)
            {
                // Create arrays
                int[] randomArray = GenerateRandomArray(50);
                int[] ascendingArray = GenerateAscendingArray(50);
                int[] descendingArray = GenerateDescendingArray(50);

                // Test Heap Sort
                TestSortingAlgorithm("Sorting random array using Heap Sort", randomArray);
                TestSortingAlgorithm("Sorting ascending array using Heap Sort", ascendingArray);
                TestSortingAlgorithm("Sorting descending array using Heap Sort", descendingArray);

                // Test Quick Sort
                TestQuickSort("Sorting random array using Quick Sort", randomArray);
                TestQuickSort("Sorting ascending array using Quick Sort", ascendingArray);
                TestQuickSort("Sorting descending array using Quick Sort", descendingArray);
            } // End of Main

            public static int[] HeapSort(int[] arr, out int comparisons, out int swaps)
            {
                comparisons = 0;
                swaps = 0;

                int n = arr.Length;

                for (int i = n / 2 - 1; i >= 0; i--) // Build heap (rearrange array)
                {
                    (int cmp, int swap) = Heapify(arr, n, i);
                    comparisons += cmp;
                    swaps += swap;
                }

                for (int i = n - 1; i > 0; i--) // One by one extract an element from heap
                {
                    Swap(ref arr[0], ref arr[i]); // Move current root to end
                    swaps++;
                    (int cmp, int swap) = Heapify(arr, i, 0); // Heapify the reduced heap
                    comparisons += cmp;
                    swaps += swap;
                }

                return arr;
            } // End of HeapSort

            private static (int, int) Heapify(int[] arr, int n, int i)
            {
                int comparisons = 0;
                int swaps = 0;

                int largest = i;
                int left = 2 * i + 1;
                int right = 2 * i + 2;

                if (left < n)
                {
                    comparisons++;
                    if (arr[left] > arr[largest])
                    {
                        largest = left;
                    }
                }

                if (right < n)
                {
                    comparisons++;
                    if (arr[right] > arr[largest])
                    {
                        largest = right;
                    }
                }

                if (largest != i)
                {
                    Swap(ref arr[i], ref arr[largest]);
                    swaps++;
                    (int cmp, int swap) = Heapify(arr, n, largest); // Recursively heapify the affected sub-tree
                    comparisons += cmp;
                    swaps += swap;
                }

                return (comparisons, swaps);
            } // End of Heapify

            public static (int[], int, int) QuickSort(int[] arr)
            {
                return QuickSortRecursive(arr, 0, arr.Length - 1);
            } // End of QuickSort

            private static int ChoosePivot(int[] arr, int low, int high) // Choose pivot as the median of the first, middle, and last elements
            {
                int mid = (low + high) / 2;
                int a = arr[low];
                int b = arr[mid];
                int c = arr[high];
                if ((a <= b && b <= c) || (c <= b && b <= a))
                {
                    return mid;
                }
                else if ((b <= a && a <= c) || (c <= a && a <= b))
                {
                    return low;
                }
                else
                {
                    return high;
                }
            } // End of ChoosePivot

            private static (int[], int, int) QuickSortRecursive(int[] arr, int low, int high)
            {
                if (low >= high)
                {
                    return (arr, 0, 0);
                }

                int comparisons = 0;
                int swaps = 0;

                int pivotIndex = ChoosePivot(arr, low, high);
                int pivot = arr[pivotIndex];

                Swap(ref arr[pivotIndex], ref arr[low]); // Move pivot to the first element
                swaps++;

                List<int> left = new List<int>();
                List<int> right = new List<int>();

                for (int i = low + 1; i <= high; i++)
                {
                    comparisons++;
                    if (arr[i] < pivot)
                    {
                        left.Add(arr[i]);
                    }
                    else
                    {
                        right.Add(arr[i]);
                    }
                }

                comparisons += left.Count + right.Count;

                (int[] leftSorted, int leftCmp, int leftSwap) = QuickSortRecursive(left.ToArray(), 0, left.Count - 1);
                (int[] rightSorted, int rightCmp, int rightSwap) = QuickSortRecursive(right.ToArray(), 0, right.Count - 1);

                comparisons += leftCmp + rightCmp;
                swaps += leftSwap + rightSwap;

                List<int> sortedArray = new List<int>(leftSorted);
                sortedArray.Add(pivot);
                sortedArray.AddRange(rightSorted);

                swaps += sortedArray.Count - 1;

                return (sortedArray.ToArray(), comparisons, swaps);
            } // End of QuickSortRecursive

            static void TestSortingAlgorithm(string algorithm, int[] arr)
            {
                Console.WriteLine("Testing " + algorithm);

                int[] copy = new int[arr.Length];
                Array.Copy(arr, copy, arr.Length);

                int comparisons = 0, swaps = 0;

                if (algorithm.Contains("Heap Sort"))
                {
                    HeapSort(copy, out comparisons, out swaps);
                }
                else if (algorithm.Contains("Quick Sort"))
                {
                    (int[] sortedArray, int quickSortComparisons, int quickSortSwaps) = QuickSort(copy);
                    comparisons = quickSortComparisons;
                    swaps = quickSortSwaps;
                }

                Console.WriteLine("Comparisons: " + comparisons);
                Console.WriteLine("Swaps: " + swaps);
                Console.WriteLine();
            } // End of TestSortingAlgorithm

            static void TestQuickSort(string algorithm, int[] arr)
            {
                Console.WriteLine("Testing " + algorithm);

                int[] copy = new int[arr.Length];
                Array.Copy(arr, copy, arr.Length);

                (int[] sortedArray, int comparisons, int swaps) = QuickSort(copy);

                Console.WriteLine("Comparisons: " + comparisons);
                Console.WriteLine("Swaps: " + swaps);
                Console.WriteLine();
            } // End of TestQuickSort

            static void Swap(ref int a, ref int b)
            {
                int temp = a;
                a = b;
                b = temp;
            }

            static int[] GenerateRandomArray(int size)
            {
                Random random = new Random();
                int[] arr = new int[size];
                for (int i = 0; i < size; i++)
                {
                    arr[i] = random.Next(100);
                }
                return arr;
            } // End of GenerateRandomArray

            static int[] GenerateAscendingArray(int size)
            {
                int[] arr = new int[size];
                for (int i = 0; i < size; i++)
                {
                    arr[i] = i;
                }
                return arr;
            } // End of GenerateAscendingArray

            static int[] GenerateDescendingArray(int size)
            {
                int[] arr = new int[size];
                for (int i = size - 1, j = 0; i >= 0; i--, j++)
                {
                    arr[j] = i;
                }
                return arr;
            } // End of GenerateDescendingArray
        }
    }
}
