using System;
using System.Collections.Generic;
using System.Linq;

namespace Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree =
                new Tree<int>(7,
                    new Tree<int>(19,
                        new Tree<int>(1),
                        new Tree<int>(12),
                        new Tree<int>(31)),
                    new Tree<int>(21),
                    new Tree<int>(14,
                        new Tree<int>(23),
                        new Tree<int>(6)));

            Console.WriteLine(String.Join(" ", tree.Children.Select(a => a.Value))); //с този подход не можем да обиколим цялото дърво.
            tree.Print();

            IEnumerable<int> resultDFS = tree.OrderDFS();
            Console.WriteLine(String.Join(" ", resultDFS));

            IEnumerable<int> resultBFS = tree.OrderBFS();
            Console.WriteLine(String.Join(" ", resultBFS));
            Console.WriteLine();
            Console.WriteLine("_____________________________________");
            Console.WriteLine("Tree (indented):");
            tree.Print();

            Console.Write("Tree nodes:");
            tree.Each(c => Console.Write(" " + c));
        }
    }
}
