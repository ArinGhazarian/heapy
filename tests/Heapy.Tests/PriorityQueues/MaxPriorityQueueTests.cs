using System;
using System.Linq;
using FluentAssertions;
using Heapy.PriorityQueues;
using Xunit;

namespace Heapy.Tests.PriorityQueues
{
    public class MaxPriorityQueueTests
    {
        [Fact]
        public void Enqueue_Should_Create_Max_Priority_Queue_With_Max_Value_In_Root()
        {
            var pq = new MaxPriorityQueue<int>();

            pq.Enqueue(6);
            pq.Enqueue(9);
            pq.Enqueue(8);
            pq.Enqueue(5);
            pq.Enqueue(1);
            pq.Enqueue(3);

            pq.Count.Should().Be(6);

            pq.First().Should().Be(9);
        }

        [Fact]
        public void Dequeue_Should_Always_Return_The_Max_Value()
        {
            var pq = new MaxPriorityQueue<int>();

            pq.Enqueue(6);
            pq.Enqueue(9);
            pq.Enqueue(8);
            pq.Enqueue(3);
            pq.Enqueue(5);
            pq.Enqueue(1);
            pq.Enqueue(3);

            pq.Count.Should().Be(7);

            pq.Dequeue().Should().Be(9);
            pq.Count.Should().Be(6);
            pq.Dequeue().Should().Be(8);
            pq.Count.Should().Be(5);
            pq.Dequeue().Should().Be(6);
            pq.Count.Should().Be(4);
            pq.Dequeue().Should().Be(5);
            pq.Count.Should().Be(3);
            pq.Dequeue().Should().Be(3);
            pq.Count.Should().Be(2);
            pq.Dequeue().Should().Be(3);
            pq.Count.Should().Be(1);
            pq.Dequeue().Should().Be(1);
            pq.Count.Should().Be(0);
        }

        [Fact]
        public void TryDequeue_Should_Return_True_And_Outs_The_Max_Value_When_Queue_Is_Not_Empty()
        {
            var pq = new MaxPriorityQueue<int>();

            pq.Enqueue(5);
            pq.Enqueue(3);
            pq.Enqueue(8);
            pq.Enqueue(1);

            var result = pq.TryDequeue(out var value);

            result.Should().BeTrue();
            value.Should().Be(8);
            pq.Count.Should().Be(3);
        }

        [Fact]
        public void Peek_Should_Return_The_Max_Value_Without_Removing()
        {
            var pq = new MaxPriorityQueue<int>();

            pq.Enqueue(5);
            pq.Enqueue(3);
            pq.Enqueue(8);
            pq.Enqueue(1);

            var result = pq.Peek();

            result.Should().Be(8);
            pq.Count.Should().Be(4);
        }

        [Fact]
        public void TryPeek_Should_Return_True_And_Outs_The_Max_Value_When_Queue_Is_Not_Empty()
        {
            var pq = new MaxPriorityQueue<int>();

            pq.Enqueue(5);
            pq.Enqueue(3);
            pq.Enqueue(8);
            pq.Enqueue(1);

            var result = pq.TryPeek(out var value);

            result.Should().BeTrue();
            value.Should().Be(8);
            pq.Count.Should().Be(4);
        }
    }
}