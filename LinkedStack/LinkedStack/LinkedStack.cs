using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkedStack
{
    public class LinkedStack<T> : IEnumerable<T>
    {
        private Node firstNode;

        public int Count { get; private set; }

        public void Push(T element) // == AddLast()
        {
            this.firstNode = new Node(element, this.firstNode); //движим се отгоре надолу в смисъл следващия е долния

            this.Count++;
        }

        public T Pop() // == RemoveLast()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            T result = this.firstNode.Value;
            this.firstNode = this.firstNode.NextNode; // върха на стека става следващия елемент на досегашния връх
            this.Count--;

            return result;
        }

        public T Peek()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return this.firstNode.Value;
        }

        public T[] ToArray()
        {
            T[] array = new T[this.Count];

            int index = 0;
            Node current = this.firstNode;
            while (current != null)
            {
                array[index++] = current.Value;
                current = current.NextNode;
            }

            return array;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node current = this.firstNode;
            while (current != null)
            {
                yield return current.Value;
                current = current.NextNode;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private class Node
        {
            public Node(T value, Node nextNode = null)
            {
                this.Value = value;
                this.NextNode = nextNode;
            }

            public T Value { get; set; }
            public Node NextNode { get; set; }
        }
    }
}
