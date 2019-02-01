using System;

namespace BinaryTree
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree<int> bt =
            new BinaryTree<int>(5,
                leftChild: new BinaryTree<int>(2),
                rightChild: new BinaryTree<int>(1));

            bt.PrintIndentedPreOrder();
            Console.WriteLine();
            bt.EachInOrder(x => Console.WriteLine(x));
            Console.WriteLine();
            bt.EachPostOrder(x => Console.WriteLine(x));
            Console.WriteLine("_____________________________________");

            var binaryTree =
                new BinaryTree<string>("*",
                    new BinaryTree<string>("+",
                        new BinaryTree<string>("3"),
                        new BinaryTree<string>("2")),
                    new BinaryTree<string>("-",
                        new BinaryTree<string>("9"),
                        new BinaryTree<string>("6")));

            Console.WriteLine("Binary tree (indented, pre-order):");
            binaryTree.PrintIndentedPreOrder();

            Console.Write("Binary tree nodes (in-order):");
            binaryTree.EachInOrder(c => Console.Write(" " + c));
            Console.WriteLine();

            Console.Write("Binary tree nodes (post-order):");
            binaryTree.EachPostOrder(c => Console.Write(" " + c));
            Console.WriteLine();
        }
    }
}
