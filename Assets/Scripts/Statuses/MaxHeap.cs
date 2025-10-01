using System;
using System.Collections.Generic;

public class MaxHeap<T>
{
    private List<(float priority, T value)> heap = new List<(float, T)>();

    public int Count => heap.Count;

    public void Insert(T value, float priority)
    {
        heap.Add((priority, value));
        HeapifyUp(heap.Count - 1);
    }

    public T ExtractMax()
    {
        if (heap.Count == 0) throw new InvalidOperationException("Heap is empty");

        var max = heap[0].value;
        heap[0] = heap[^1];
        heap.RemoveAt(heap.Count - 1);

        if (heap.Count > 0)
            HeapifyDown(0);

        return max;
    }

    public (float priority, T value) ExtractMaxWithPriority()
    {
        if (heap.Count == 0) throw new InvalidOperationException("Heap is empty");

        var max = heap[0];
        heap[0] = heap[^1];
        heap.RemoveAt(heap.Count - 1);

        if (heap.Count > 0)
            HeapifyDown(0);

        return max;
    }

    public T PeekMax() => heap[0].value;
    public float PeekPriority() => heap[0].priority;

    private void HeapifyUp(int i)
    {
        while (i > 0)
        {
            int parent = (i - 1) / 2;
            if (heap[i].priority <= heap[parent].priority) break;
            Swap(i, parent);
            i = parent;
        }
    }

    private void HeapifyDown(int i)
    {
        int last = heap.Count - 1;

        while (true)
        {
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            int largest = i;

            if (left <= last && heap[left].priority > heap[largest].priority) largest = left;
            if (right <= last && heap[right].priority > heap[largest].priority) largest = right;

            if (largest == i) break;

            Swap(i, largest);
            i = largest;
        }
    }

    private void Swap(int a, int b)
    {
        (heap[a], heap[b]) = (heap[b], heap[a]);
    }
}
