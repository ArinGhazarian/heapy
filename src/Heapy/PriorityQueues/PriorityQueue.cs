using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Heapy.PriorityQueues
{
    public abstract class PriorityQueue<T> : IReadOnlyCollection<T>
    {
        private readonly List<T> data = new List<T>();

        protected abstract bool IsFirstItemPriorityHigherThanSecondItemPriority(T first, T second);

        /// <summary>
        /// Enqueues the item into the priority queue by inserting it at the bottommost level, rightmost spot in the complete heap
        /// (or if the heap is implemented by a data list, by inserting it at the end of the list)
        /// and bubbling it up until the priority queue's condition is met.
        /// </summary>
        /// <param name="item">The item to be enqueued.</param>
        public void Enqueue(T item)
        {
            data.Add(item);

            int itemIndex = data.Count - 1;
            int parentIndex = GetParentIndex(itemIndex);
            while (parentIndex >= 0 && IsFirstItemPriorityHigherThanSecondItemPriority(item, data[parentIndex])) // item has higher priority than its parent 
            {
                (data[itemIndex], data[parentIndex]) = (data[parentIndex], data[itemIndex]); // swap with parent
                itemIndex = parentIndex;
                parentIndex = GetParentIndex(itemIndex);
            }
        }

        /// <summary>
        /// Dequeues the root element then reconstructs the heap by removing the bottommost, rightmost element
        /// (or if the heap is implemented by a data list, by removing the last element from the list)
        /// then replacing it with the root element and bubbling it down until the priority queue's condition is met.
        /// </summary>
        /// <returns>The highest priority element.</returns>
        public T Dequeue()
        {
            if (!TryDequeue(out T result))
            {
                throw new InvalidOperationException("Priority queue is empty.");
            }

            return result;
        }

        public bool TryDequeue(out T result)
        {
            if (IsEmpty)
            {
                result = default;
                return false;
            }

            result = data.First();

            var parent = data.Last();
            int parentIndex = 0;
            data[parentIndex] = parent;
            data.RemoveAt(data.Count - 1);
            int leftChildIndex = GetLeftChildIndex(parentIndex);
            int rightChildIndex = GetRightChildIndex(parentIndex);
            bool hasLeftChild = leftChildIndex < data.Count;
            bool hasRightChild = rightChildIndex < data.Count;
            while ((hasLeftChild && IsFirstItemPriorityHigherThanSecondItemPriority(data[leftChildIndex], parent)) // left child has higher priority than its parent
                || (hasRightChild && IsFirstItemPriorityHigherThanSecondItemPriority(data[rightChildIndex], parent))) // right child has higher priority than its parent
            {
                int higherPriorityChildIndex;
                if (hasRightChild)
                {
                    higherPriorityChildIndex = IsFirstItemPriorityHigherThanSecondItemPriority(data[leftChildIndex], data[rightChildIndex]) ? leftChildIndex : rightChildIndex;
                }
                else // only has a left child
                {
                    higherPriorityChildIndex = leftChildIndex;
                }

                // swap the higher priority child with its parent
                (data[parentIndex], data[higherPriorityChildIndex]) = (data[higherPriorityChildIndex], data[parentIndex]);

                parentIndex = higherPriorityChildIndex;
                parent = data[parentIndex];
                leftChildIndex = GetLeftChildIndex(parentIndex);
                rightChildIndex = GetRightChildIndex(parentIndex);
                hasLeftChild = leftChildIndex < data.Count;
                hasRightChild = rightChildIndex < data.Count;
            }

            return true;
        }

        public T Peek()
        {
            if (!TryPeek(out T result))
            {
                throw new InvalidOperationException("Priority queue is empty.");
            }

            return result;
        }

        public bool TryPeek(out T result)
        {
            if (IsEmpty)
            {
                result = default;
                return false;
            }

            result = data.First();
            return true;
        }

        public bool IsEmpty => data.Count <= 0;

        public int Count => data.Count;

        public IEnumerator<T> GetEnumerator() => data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private int GetParentIndex(int index) => (index - 1) / 2;

        private int GetLeftChildIndex(int index) => 2 * index + 1;

        private int GetRightChildIndex(int index) => 2 * index + 2;
    }
}