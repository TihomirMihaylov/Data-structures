using System;

class Program
{
    static void Main(string[] args)
    {
        ArrayStack<int> stack = new ArrayStack<int>();
        stack.Push(2);
        stack.Push(4);
        stack.Push(-5);
        stack.Push(8);
        Console.WriteLine(stack.Count);
        Console.WriteLine(String.Join(" ", stack.ToArray()));
        Console.WriteLine();

        int popped = stack.Pop();
        Console.WriteLine(popped);
        Console.WriteLine(stack.Count);
        Console.WriteLine(String.Join(" ", stack.ToArray()));
        Console.WriteLine();

        foreach (var item in stack)
        {
            Console.WriteLine(item);
        }
    }
}
