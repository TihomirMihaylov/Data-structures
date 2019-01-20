using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkedQueue
{
    public class LinkedQueue<T> : IEnumerable<T>
    {
        private class QueueNode<T>
        {
            public QueueNode(T value)
            {
                this.Value = value;
            }

            public T Value { get; private set; }

            public QueueNode<T> NextNode { get; set; }

            public QueueNode<T> PrevNode { get; set; }
        }

        private QueueNode<T> head;
        private QueueNode<T> tail;

        public int Count { get; private set; }

        public void Enqueue(T element) // == AddLast()
        {
            if (this.Count == 0)
            {
                this.head = this.tail = new QueueNode<T>(element);
            }
            else
            {
                QueueNode<T> newTail = new QueueNode<T>(element);
                newTail.PrevNode = this.tail;
                this.tail.NextNode = newTail;
                this.tail = newTail;
            }

            this.Count++;
        }

        public T Dequeue() // == RemoveFirst()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Queue is empty.");
            }

            T firstElement = this.head.Value;
            this.head = this.head.NextNode;
            if (this.head != null)
            {
                this.head.PrevNode = null;
            }
            else
            {
                this.tail = null;
            }

            this.Count--;
            return firstElement;
        }

        public T[] ToArray()
        {
            T[] array = new T[this.Count];
            int index = 0;
            QueueNode<T> currentNode = this.head;
            while (currentNode != null)
            {
                array[index++] = currentNode.Value;
                currentNode = currentNode.NextNode;
            }

            return array;
        }

        public IEnumerator<T> GetEnumerator()
        {
            QueueNode<T> currentNode = this.head;
            while (currentNode != null)
            {
                yield return currentNode.Value;
                currentNode = currentNode.NextNode;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
