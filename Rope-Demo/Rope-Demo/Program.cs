using System;
using System.Diagnostics;
using System.Text;
using Wintellect.PowerCollections;

namespace Rope_Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            int operationsCount = 50_000;
            BigList<char> rope = new BigList<char>(); //имплементирано е с въже.

            //За добавяне в началото въжето винаги ще е по-бързо
            #region Add in the beginning
            Console.WriteLine("At the beginning");

            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < operationsCount; i++)
            {
                rope.Insert(0, 'a');
            }

            stopwatch.Stop();
            Console.WriteLine($"Rope: {stopwatch.ElapsedMilliseconds} milliseconds");

            StringBuilder sb = new StringBuilder();
            stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < operationsCount; i++)
            {
                sb.Insert(0, 'a');
            }

            stopwatch.Stop();
            Console.WriteLine($"StringBuilder: {stopwatch.ElapsedMilliseconds} milliseconds");


            string str = string.Empty;
            stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < operationsCount; i++)
            {
                str = str.Insert(0, "a");
            }

            stopwatch.Stop();
            Console.WriteLine($"String: {stopwatch.ElapsedMilliseconds} milliseconds");
            #endregion

            //За добавяне по средата StringBuilder-a е най-бърз, но колкото са повече елементите въжето ще става все по-бързо
            #region Add in the middle
            Console.WriteLine("In the middle");
            rope = new BigList<char>();
            stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < operationsCount; i++)
            {
                rope.Insert(rope.Count / 2, 'a');
            }

            stopwatch.Stop();
            Console.WriteLine($"Rope: {stopwatch.ElapsedMilliseconds} milliseconds");

            sb = new StringBuilder();
            stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < operationsCount; i++)
            {
                sb.Insert(sb.Length / 2, 'a');
            }

            stopwatch.Stop();
            Console.WriteLine($"StringBuilder: {stopwatch.ElapsedMilliseconds} milliseconds");


            str = string.Empty;
            stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < operationsCount; i++)
            {
                str = str.Insert(str.Length / 2, "a");
            }

            stopwatch.Stop();
            Console.WriteLine($"String: {stopwatch.ElapsedMilliseconds} milliseconds");
            #endregion

            //За добавяне в края StringBuilder-a винаги ще е най-бърз 
            #region Add in the end
            Console.WriteLine("In the end");
            rope = new BigList<char>();
            stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < operationsCount; i++)
            {
                rope.Add('a');
            }

            stopwatch.Stop();
            Console.WriteLine($"Rope: {stopwatch.ElapsedMilliseconds} milliseconds");

            sb = new StringBuilder();
            stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < operationsCount; i++)
            {
                sb.Append('a');
            }

            stopwatch.Stop();
            Console.WriteLine($"StringBuilder: {stopwatch.ElapsedMilliseconds} milliseconds");


            str = string.Empty;
            stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < operationsCount; i++)
            {
                str += "a";
            }

            stopwatch.Stop();
            Console.WriteLine($"String: {stopwatch.ElapsedMilliseconds} milliseconds");
            #endregion
        }
    }
}
