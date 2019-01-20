using System;

namespace LinkedQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedQueue<int> queue = new LinkedQueue<int>();
            queue.Enqueue(2);
            queue.Enqueue(5);
            queue.Enqueue(-9);
            queue.Enqueue(0);
            queue.Dequeue();

            foreach (var item in queue)
            {
                Console.WriteLine(item);
            }
        }
    }
}
