using System;

namespace Heapy.PriorityQueues
{
    public class MaxPriorityQueue<T> : PriorityQueue<T> where T : IComparable<T>
    {
        protected override bool IsFirstItemPriorityHigherThanSecondItemPriority(T first, T second)
        {
            return first.CompareTo(second) > 0;
        }
    }
}