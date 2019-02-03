using System;
using System.Collections.Generic;

namespace BinarySearchTree
{
    public interface IBinarySearchTree<T> where T: IComparable
    {
        //Basic Tree Operations
        void Insert(T value);

        bool Contains(T value);

        void EachInOrder(Action<T> action);

        //Binary Search Tree Operations

        BinarySearchTree<T> Search(T item);

        void DeleteMin();

        IEnumerable<T> Range(T startRange, T endRange);
    }
}
