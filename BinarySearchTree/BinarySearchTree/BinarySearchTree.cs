using System;
using System.Collections.Generic;

namespace BinarySearchTree
{
    public class BinarySearchTree<T> : IBinarySearchTree<T> where T : IComparable //! T трябва да може да се сравнява
    {
        private class Node
        {
            public Node(T value)
            {
                this.Value = value;
            }

            public T Value { get; set; }

            public Node Left { get; set; }

            public Node Right { get; set; }
        }

        private Node root;

        public BinarySearchTree()
        {
        }

        private BinarySearchTree(Node node) //не може public constructor да приема обект от private class
        {
            //this.root = node;  //ако го оставим така ще ни счупи логиката на дървото. Tрябва да копираме елементите
            this.Copy(node);
        }

        public bool Contains(T value)
        {
            Node node = this.FindElement(value);
            return node != null;
        }

        public void DeleteMin()
        {
            if(this.root == null)
            {
                return;
            }

            Node parent = null;
            Node current = this.root;
            while (current.Left != null)
            {
                parent = current;
                current = current.Left;
            }

            if(parent == null) //Ако сме на корена
            {
                this.root = this.root.Right;
            }
            else //Ако не сме на корена, родителя на текущия нод получава за ляво дете дясното дете на текущия нод (който, го изтриваме защото е най-малкия елемент, а неговото дясно дете е следващото по-големина след него
            {
                parent.Left = current.Right;
            }
        }

        public void EachInOrder(Action<T> action) // left-root-right
        {
            this.EachInOrder(this.root, action);
        }

        public void Insert(T value)
        {
            if(this.root == null)
            {
                this.root = new Node(value);
                return;
            }

            Node parent = null;
            Node current = this.root;
            while (current != null)
            {
                parent = current;
                if(value.CompareTo(current.Value) < 0)
                {
                    current = current.Left;
                }
                else if(value.CompareTo(current.Value) > 0)
                {
                    current = current.Right;
                }
                else
                {
                    //break; // така ще вкарва повтарящи се стойности
                    return;
                }
            }

            Node newNode = new Node(value);
            if(value.CompareTo(parent.Value) > 0)
            {
                parent.Right = newNode;
            }
            else
            {
                parent.Left = newNode;
            }
        }

        public IEnumerable<T> Range(T startRange, T endRange)  //включително горната и долната граница
        {
            Queue<T> range = new Queue<T>();

            this.Range(startRange, endRange, range, this.root);

            return range;

            //втори начин с yield return
            //return this.Range(this.root, startRange, endRange);
        }

        public BinarySearchTree<T> Search(T item)
        {
            Node node = this.FindElement(item);
            return new BinarySearchTree<T>(node);
        }

        private void EachInOrder(Node node, Action<T> action)
        {
            if(node == null)
            {
                return;
            }

            this.EachInOrder(node.Left, action);
            action(node.Value);
            this.EachInOrder(node.Right, action);
        }

        private Node FindElement(T value)
        {
            Node current = this.root;
            while (current != null)
            {
                if(value.CompareTo(current.Value) < 0)
                {
                    current = current.Left;
                }
                else if (value.CompareTo(current.Value) > 0)
                {
                    current = current.Right;
                }
                else
                {
                    break;
                }
            }

            return current;
        }

        private void Copy(Node node)
        {
            if(node == null)
            {
                return;
            }

            //Pre-order traversal
            this.Insert(node.Value);
            this.Copy(node.Left);
            this.Copy(node.Right);
        }

        private void Range(T startRange, T endRange, Queue<T> range, Node node)
        {
            if(node == null)
            {
                return;
            }

            int compareLow = startRange.CompareTo(node.Value);
            int compareHigh = endRange.CompareTo(node.Value);

            if(compareLow < 0)
            {
                this.Range(startRange, endRange, range, node.Left);
            }
            if(compareLow <= 0 && compareHigh >= 0) //inclusive
            {
                range.Enqueue(node.Value); //този елемент вече е в обхвата ни
            }
            if (compareHigh > 0)
            {
                this.Range(startRange, endRange, range, node.Right);
            }
        }

        //втори начин с yield return
        private IEnumerable<T> Range(Node node, T startRange, T endRange)
        {
            if (node == null)
            {
                yield break;
            }

            int compareLow = startRange.CompareTo(node.Value);
            int compareHigh = endRange.CompareTo(node.Value);

            if (compareLow < 0)
            {
                foreach (T item in this.Range(node.Left, startRange, endRange))
                {
                    yield return item;
                }
            }
            if (compareLow <= 0 && compareHigh >= 0) //inclusive
            {
                yield return node.Value;
            }
            if (compareHigh > 0)
            {
                foreach (T item in this.Range(node.Right, startRange, endRange))
                {
                    yield return item;
                }
            }
        }
    }
}
