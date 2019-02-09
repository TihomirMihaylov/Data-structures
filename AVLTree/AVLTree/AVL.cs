using System;

public class AVL<T> where T : IComparable<T>
{
    private Node<T> root;

    public Node<T> Root
    {
        get
        {
            return this.root;
        }
    }

    public bool Contains(T item)
    {
        var node = this.Search(this.root, item);
        return node != null;
    }

    public void Insert(T item)
    {
        this.root = this.Insert(this.root, item);
    }

    public void Delete(int item)
    {
        this.root = this.Delete(this.root, item);
    }

    public void DeleteMin()
    {
        this.root = this.DeleteMin(this.root);
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }

    private Node<T> Insert(Node<T> node, T item)
    {
        if (node == null)
        {
            return new Node<T>(item);
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            node.Left = this.Insert(node.Left, item);
        }
        else if (cmp > 0)
        {
            node.Right = this.Insert(node.Right, item);
        }

        node = Balance(node);
        return node;
    }

    private Node<T> Search(Node<T> node, T item)
    {
        if (node == null)
        {
            return null;
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            return Search(node.Left, item);
        }
        else if (cmp > 0)
        {
            return Search(node.Right, item);
        }

        return node;
    }

    private void EachInOrder(Node<T> node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Value);
        this.EachInOrder(node.Right, action);
    }

    private static int Height(Node<T> node)
    {
        if(node == null)
        {
            return 0;
        }

        return node.Height;
    }

    private static void UpdateHeight(Node<T> node)
    {   //взимам височината на детето с по-голяма височина и я увеличавам с 1
        node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
    }

    private static Node<T> RotateRight(Node<T> node)
    {
        Node<T> left = node.Left;
        node.Left = left.Right;
        left.Right = node;

        UpdateHeight(node);

        return left;
    }

    private static Node<T> RotateLeft(Node<T> node)
    {
        Node<T> right = node.Right;
        node.Right = right.Left;
        right.Left = node;

        UpdateHeight(node);

        return right;
    }

    private static Node<T> Balance(Node<T> node)
    {
        int balance = Height(node.Left) - Height(node.Right);
        if(balance > 1) // Left child is heavy --> Right rotation
        {
            balance = Height(node.Left.Left) - Height(node.Left.Right);
            if (balance < 0) //Double right rotation
            {
                node.Left = RotateLeft(node.Left);
            }

            node = RotateRight(node);
        }
        else if(balance < -1) // Right child is heavy --> Left rotation
        {
            balance = Height(node.Right.Left) - Height(node.Right.Right);
            if(balance > 0) //Double left rotation
            {
                node.Right = RotateRight(node.Right);
            }

            node = RotateLeft(node);
        }

        UpdateHeight(node);
        return node;
    }

    private Node<T> DeleteMin(Node<T> node)
    {
        if(node == null)
        {
            return null;
        }

        if(node.Left == null)
        {
            return node.Right;
        }

        node.Left = this.DeleteMin(node.Left);
        node = Balance(node);
        return node;
    }

    private Node<T> Delete(Node<T> node, int item)
    {
        if (node == null)
        {
            return null;
        }

        int compare = item.CompareTo(node.Value);
        if(compare < 0)
        {
            node.Left = this.Delete(node.Left, item);
        }
        else if (compare > 0)
        {
            node.Right = this.Delete(node.Right, item);
        }
        else
        {
            if(node.Left == null)
            {
                return node.Right;
            }
            else if(node.Right == null)
            {
                return node.Left;
            }
            else
            {
                //Взимаме най-малкия елемент в дясното поддърво и го разменяме с елемента, който трием
                //Същото става ако вземем най-големия елемент в лявото поддърво и го разменяме с елемента, който трием
                Node<T> min = this.GetMin(node.Right);
                min.Right = this.DeleteMin(node.Right);
                min.Left = node.Left;
                node = min;
            }
        }

        node = Balance(node);
        return node;
    }

    private Node<T> GetMin(Node<T> node)
    {
        if(node == null)
        {
            return null;
        }

        if(node.Left == null)
        {
            return node;
        }

        return this.GetMin(node.Left);
    }
}
