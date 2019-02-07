﻿using System;
using System.Collections.Generic;

public class RedBlackTree<T> : IBinarySearchTree<T> where T : IComparable
{
    private const bool Red = true;
    private const bool Black = false;

    private Node root;

    private Node FindElement(T element)
    {
        Node current = this.root;

        while (current != null)
        {
            if (current.Value.CompareTo(element) > 0)
            {
                current = current.Left;
            }
            else if (current.Value.CompareTo(element) < 0)
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

    private void PreOrderCopy(Node node)
    {
        if (node == null)
        {
            return;
        }

        this.Insert(node.Value);
        this.PreOrderCopy(node.Left);
        this.PreOrderCopy(node.Right);
    }

    private Node Insert(T element, Node node)
    {
        if (node == null)
        {
            node = new Node(element, Red);
        }
        else if (element.CompareTo(node.Value) < 0)
        {
            node.Left = this.Insert(element, node.Left);
        }
        else if (element.CompareTo(node.Value) > 0)
        {
            node.Right = this.Insert(element, node.Right);
        }

        //ROTATIONS
        if (this.IsRed(node.Right) && !this.IsRed(node.Left)) //дясното дете е червено, лявото е черно 
        {
            node = this.RotateLeft(node);
        }
        if (this.IsRed(node.Left) && this.IsRed(node.Left.Left)) //две последователни леви връзки от ляво
        {
            node = this.RotateRight(node);
        }
        if (this.IsRed(node.Left) && this.IsRed(node.Right))//и двете деца са червени
        {
            this.FlipColors(node);
        }

        node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);
        return node;
    }

    private void Range(Node node, Queue<T> queue, T startRange, T endRange)
    {
        if (node == null)
        {
            return;
        }

        int nodeInLowerRange = startRange.CompareTo(node.Value);
        int nodeInHigherRange = endRange.CompareTo(node.Value);

        if (nodeInLowerRange < 0)
        {
            this.Range(node.Left, queue, startRange, endRange);
        }
        if (nodeInLowerRange <= 0 && nodeInHigherRange >= 0)
        {
            queue.Enqueue(node.Value);
        }
        if (nodeInHigherRange > 0)
        {
            this.Range(node.Right, queue, startRange, endRange);
        }
    }

    private void EachInOrder(Node node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Value);
        this.EachInOrder(node.Right, action);
    }

    private int Count(Node node)
    {
        if (node == null)
        {
            return 0;
        }

        return node.Count;
    }

    private RedBlackTree(Node node)
    {
        this.PreOrderCopy(node);
    }

    public RedBlackTree()
    {
    }

    public void Insert(T element)
    {
        this.root = this.Insert(element, this.root);
        this.root.Color = Black; //Ако рекурсията ни върне червен корен на самото дърво го правим черен
    }

    public bool Contains(T element)
    {
        Node current = this.FindElement(element);

        return current != null;
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }

    public IBinarySearchTree<T> Search(T element)
    {
        Node current = this.FindElement(element);

        return new RedBlackTree<T>(current);
    }

    //public void DeleteMin()
    //{
    //    if (this.root == null)
    //    {
    //        throw new InvalidOperationException();
    //    }

    //    this.root = this.DeleteMin(this.root);
    //}

    //private Node DeleteMin(Node node)
    //{
    //    if (node.Left == null)
    //    {
    //        return node.Right;
    //    }

    //    node.Left = this.DeleteMin(node.Left);
    //    node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);

    //    return node;
    //}

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        Queue<T> queue = new Queue<T>();

        this.Range(this.root, queue, startRange, endRange);

        return queue;
    }

    //public virtual void Delete(T element)
    //{
    //    if (this.root == null)
    //    {
    //        throw new InvalidOperationException();
    //    }
    //    this.root = this.Delete(element, this.root);
    //}

    //private Node Delete(T element, Node node)
    //{
    //    if (node == null)
    //    {
    //        return null;
    //    }

    //    int compare = element.CompareTo(node.Value);

    //    if (compare < 0)
    //    {
    //        node.Left = this.Delete(element, node.Left);
    //    }
    //    else if (compare > 0)
    //    {
    //        node.Right = this.Delete(element, node.Right);
    //    }
    //    else
    //    {
    //        if (node.Right == null)
    //        {
    //            return node.Left;
    //        }
    //        if (node.Left == null)
    //        {
    //            return node.Right;
    //        }

    //        Node temp = node;
    //        node = this.FindMin(temp.Right);
    //        node.Right = this.DeleteMin(temp.Right);
    //        node.Left = temp.Left;

    //    }
    //    node.Count = this.Count(node.Left) + this.Count(node.Right) + 1;

    //    return node;
    //}

    private Node FindMin(Node node)
    {
        if (node.Left == null)
        {
            return node;
        }

        return this.FindMin(node.Left);
    }

    //public void DeleteMax()
    //{
    //    if (this.root == null)
    //    {
    //        throw new InvalidOperationException();
    //    }

    //    this.root = this.DeleteMax(this.root);
    //}

    //private Node DeleteMax(Node node)
    //{
    //    if (node.Right == null)
    //    {
    //        return node.Left;
    //    }

    //    node.Right = this.DeleteMax(node.Right);
    //    node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);

    //    return node;
    //}

    public int Count()
    {
        return this.Count(this.root);
    }

    public int Rank(T element) //returns the count of elements smaller than a given value
    {
        return this.Rank(element, this.root);
    }

    private int Rank(T element, Node node)
    {
        if (node == null)
        {
            return 0;
        }

        int compare = element.CompareTo(node.Value);

        if (compare < 0)
        {
            return this.Rank(element, node.Left);
        }

        if (compare > 0)
        {
            return 1 + this.Count(node.Left) + this.Rank(element, node.Right);
        }

        return this.Count(node.Left);
    }

    public T Select(int rank) //and returns the first element which has exactly rank elements 
    {
        Node node = this.Select(rank, this.root);
        if (node == null)
        {
            throw new InvalidOperationException();
        }

        return node.Value;
    }

    private Node Select(int rank, Node node)
    {
        if (node == null)
        {
            return null;
        }

        int leftCount = this.Count(node.Left);
        if (leftCount == rank)
        {
            return node;
        }

        if (leftCount > rank)
        {
            return this.Select(rank, node.Left);
        }
        else
        {
            return this.Select(rank - (leftCount + 1), node.Right);
        }
    }

    public T Ceiling(T element) //returns the nearest larger value than given 
    {
        return this.Select(this.Rank(element) + 1);
    }

    public T Floor(T element) //returns the nearest smaller value than given 
    {
        return this.Select(this.Rank(element) - 1);
    }

    private bool IsRed(Node node)
    {
        if (node == null)
        {
            return false;
        }

        return node.Color == Red;
    }

    private Node RotateRight(Node node)
    {
        Node temp = node.Left;
        node.Left = temp.Right;
        temp.Right = node;

        temp.Color = node.Color;
        node.Color = Red;

        temp.Count = node.Count;
        node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);

        return temp;
    }

    private Node RotateLeft(Node node)
    {
        Node temp = node.Right;
        node.Right = temp.Left;
        temp.Left = node;

        temp.Color = node.Color;
        node.Color = Red;

        temp.Count = node.Count;
        node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);

        return temp;
    }

    private void FlipColors(Node node)
    {
        node.Color = Red;
        node.Left.Color = Black;
        node.Right.Color = Black;
    }

    private class Node
    {
        public Node(T value, bool color)
        {
            this.Value = value;
            this.Color = color;
        }

        public T Value { get; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public int Count { get; set; }

        public bool Color { get; set; }
    }

}

public class Launcher
{
    public static void Main(string[] args)
    {
    }
}