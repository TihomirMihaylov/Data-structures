using System;
using System.Collections.Generic;

//MAX HEAP
public class BinaryHeap<T> where T : IComparable<T>
{
    private List<T> heap;

    public BinaryHeap()
    {
        this.heap = new List<T>();
    }

    public int Count => this.heap.Count;

    public void Insert(T item)
    {
        this.heap.Add(item);
        this.HeapifyUp(this.heap.Count - 1); //индекса на елемента, който сме добавили.
    }

    public T Peek()
    {
        if(this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        return this.heap[0];
    }

    public T Pull()
    {
        T deletedElement = this.Peek(); //включва проверката за Count == null
        this.Swap(0, this.Count - 1);
        this.heap.RemoveAt(this.Count - 1);
        this.HeapifyDown(0);

        return deletedElement;
    }

    private void HeapifyUp(int index)
    {
        int parent = (index - 1) / 2;
        while (this.heap[index].CompareTo(this.heap[parent]) > 0)
        {
            Swap(index, parent);
            index = parent;
            parent = (index - 1) / 2;
        }
    }

    private void Swap(int a, int b)
    {
        T temp = this.heap[a];
        this.heap[a] = this.heap[b];
        this.heap[b] = temp;
    }

    private void HeapifyDown(int index)
    {
        while (index < this.Count / 2) //бутаме го надолу докато индекса стане по-малък от половината на брой елементи защото елемента по средата със сигурност няма деца; щом няма деца значи е най-долу;
        {
            int child = 2 * index + 1;
            bool rightChildExists = child + 1 < this.Count;
            if(rightChildExists && this.heap[child + 1].CompareTo(this.heap[child]) > 0)
            {
                child++;
            }
            if(this.heap[index].CompareTo(this.heap[child]) > 0)
            {
                break;
            }

            this.Swap(index, child);
            index = child;
        }
    }
}
