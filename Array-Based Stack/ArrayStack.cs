using System;
using System.Collections;
using System.Collections.Generic;

public class ArrayStack<T> : IEnumerable<T>
{
    private const int InitialCapacity = 16;

    private T[] elements;

    public int Count { get; private set; }

    public ArrayStack(int capacity = InitialCapacity)
    {
        this.elements = new T[capacity];
    }

    public void Push(T element)
    {
        if (this.Count == this.elements.Length)
        {
            this.Grow();
        }

        this.elements[this.Count++] = element;
    }

    public T Pop()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        this.Count--;
        return this.elements[this.Count];
    }

    public T[] ToArray()
    {
        T[] array = new T[this.Count];
        for (int i = 0; i < this.Count; i++)
        {
            array[i] = this.elements[this.Count - 1 - i];
        }

        return array;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = this.Count -1; i >= 0; i--)
        {
            yield return this.elements[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    private void Grow()
    {
        T[] newElements = new T[this.elements.Length * 2];
        for (int i = 0; i < this.Count; i++)
        {
            newElements[i] = this.elements[i];
        }

        this.elements = newElements;
    }
}