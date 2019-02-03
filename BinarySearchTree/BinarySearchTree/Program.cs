using System;

namespace BinarySearchTree
{
    class Program
    {
        static void Main(string[] args)
        {
            BinarySearchTree<int> bst = new BinarySearchTree<int>();
            bst.Insert(10);
            bst.Insert(5);
            bst.Insert(3);
            bst.Insert(1);
            bst.Insert(4);
            bst.Insert(8);
            bst.Insert(9);
            bst.Insert(37);
            bst.Insert(39);
            bst.Insert(45);

            bst.EachInOrder(Console.WriteLine);
            Console.WriteLine();

            BinarySearchTree<int> search = bst.Search(5);
            search.Insert(50);
            Console.WriteLine(bst.Contains(50));
            search.EachInOrder(Console.WriteLine);
            Console.WriteLine();

            foreach (int item in bst.Range(4, 37))
            {
                Console.WriteLine(item);
            }
        }
    }
}
