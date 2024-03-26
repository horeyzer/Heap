using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heap
{
    public class MinHeap
    {
        private int[] heapArray;
        private int size;
        private int capacity;

        public MinHeap(int capacity)
        {
            this.capacity = capacity;
            heapArray = new int[capacity];
            size = 0;
        }

        private int Parent(int index)
        {
            return (index - 1) / 2;
        }

        private int LeftChild(int index)
        {
            return 2 * index + 1;
        }

        private int RightChild(int index)
        {
            return 2 * index + 2;
        }

        private void Swap(int index1, int index2)
        {
            int temp = heapArray[index1];
            heapArray[index1] = heapArray[index2];
            heapArray[index2] = temp;
        }

        public void Insert(int value)
        {
            if (size >= capacity)
            {
                throw new InvalidOperationException("Heap is full.");
            }

            int currentIndex = size;
            heapArray[currentIndex] = value;
            size++;

            while (currentIndex > 0 && heapArray[currentIndex] < heapArray[Parent(currentIndex)])
            {
                Swap(currentIndex, Parent(currentIndex));
                currentIndex = Parent(currentIndex);
            }
        }

        public bool Contains(int value)
        {
            for (int i = 0; i < size; i++)
            {
                if (heapArray[i] == value)
                {
                    for (int j = 0; j < i; j++)
                    {
                        Console.Write(heapArray[j] + " ");
                    }
                    Console.WriteLine();
                    return true;
                }
            }
            return false;
        }

        public void Delete(int value)
        {
            int index = -1;
            for (int i = 0; i < size; i++)
            {
                if (heapArray[i] == value)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                throw new InvalidOperationException("Value not found in the heap.");
            }

            heapArray[index] = heapArray[size - 1];
            size--;

            HeapifyDown(index);
        }

        private void HeapifyDown(int index)
        {
            int smallest = index;
            int left = LeftChild(index);
            int right = RightChild(index);

            if (left < size && heapArray[left] < heapArray[smallest])
            {
                smallest = left;
            }

            if (right < size && heapArray[right] < heapArray[smallest])
            {
                smallest = right;
            }

            if (smallest != index)
            {
                Swap(index, smallest);
                HeapifyDown(smallest);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MinHeap minHeap = new MinHeap(20);

            minHeap.Insert(15);
            minHeap.Insert(40);
            minHeap.Insert(30);
            minHeap.Insert(50);
            minHeap.Insert(10);
            minHeap.Insert(100);
            minHeap.Insert(40);

            Console.Write("Elements visited before finding 40: ");
            bool contains40 = minHeap.Contains(40);
            Console.WriteLine("Does the heap contain 40? " + (contains40 ? "Yes" : "No"));

            minHeap.Delete(10);

            Console.ReadLine();
        }
    }

}
