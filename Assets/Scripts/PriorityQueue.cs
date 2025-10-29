using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue<T1, T2>
{
    private List<(T1 item, int priority)> heap = new List<(T1, int)>();

    public void Enqueue(T1 item, int priority)
    {
        heap.Add((item, priority));
        int currentIndex = heap.Count - 1;
        while (currentIndex > 0)
        {
            int parentIndex = (currentIndex - 1) / 2;
            if (heap[currentIndex].priority >= heap[parentIndex].priority)
                break;
            (heap[currentIndex], heap[parentIndex]) = (heap[parentIndex], heap[currentIndex]);
            currentIndex = parentIndex;
        }
    }

    public T1 Dequeue()
    {
        if (heap.Count == 0)
            throw new InvalidOperationException("The queue is empty.");

        T1 result = heap[0].item;
        heap[0] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);

        int currentIndex = 0;
        while (true)
        {
            int leftChildIndex = 2 * currentIndex + 1;
            int rightChildIndex = 2 * currentIndex + 2;
            if (leftChildIndex >= heap.Count)
                break;

            int minChildIndex = leftChildIndex;
            if (rightChildIndex < heap.Count && heap[rightChildIndex].priority < heap[leftChildIndex].priority)
                minChildIndex = rightChildIndex;

            if (heap[currentIndex].priority <= heap[minChildIndex].priority)
                break;

            (heap[currentIndex], heap[minChildIndex]) = (heap[minChildIndex], heap[currentIndex]);
            currentIndex = minChildIndex;
        }

        return result;
    }

    public int Count => heap.Count;
}
