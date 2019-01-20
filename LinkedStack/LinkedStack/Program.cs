namespace LinkedStack
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedStack<int> stack = new LinkedStack<int>();
            stack.Push(-10);
            stack.Push(5);
            stack.Push(0);
            stack.Push(2);
            stack.Pop();

            foreach (var item in stack)
            {
                System.Console.WriteLine(item);
            }
        }
    }
}
