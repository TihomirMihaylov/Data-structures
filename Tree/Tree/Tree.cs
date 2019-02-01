using System;
using System.Collections.Generic;

namespace Tree
{
    public class Tree<T>
    {
        public Tree(T value, params Tree<T>[] children)
        {
            this.Value = value;
            this.Children = new List<Tree<T>>(children);
        }

        public T Value { get; set; }

        public IList<Tree<T>> Children { get; private set; }

        public void Print(int indent = 0)
        {
            Console.WriteLine(new string(' ', indent) + this.Value);

            foreach (Tree<T> child in this.Children)
            {
                child.Print(indent + 2); //Recursion
            }
        }

        public void Each(Action<T> action)
        {
            //обработвам текущия ноуд с подадения action. След това минавам през всяко дете на текущия ноуд и рекурсивно му викам същия метод
            action(this.Value);

            foreach (Tree<T> child in this.Children)
            {
                child.Each(action);
            }
        }

        public IEnumerable<T> OrderDFS()
        {
            List<T> result = new List<T>();

            this.DFS(this, result); //recursive DFS

            //this.DFS2(this, result); //втори начин със Stack: Итеративен DFS

            return result;
        }

        private void DFS2(Tree<T> tree, List<T> result)
        {
            Stack<Tree<T>> stack = new Stack<Tree<T>>();
            stack.Push(tree);
            while (stack.Count > 0)
            {
                Tree<T> current = stack.Pop();
                foreach (Tree<T> child in current.Children)
                {
                    stack.Push(child);
                }

                result.Add(current.Value);
            }

            result.Reverse();
        }

        private void DFS(Tree<T> tree, List<T> result) //с това рекурсивно извикване подаваме референция към този лист, който е един и същ през цялото време
        {
            foreach (Tree<T> child in tree.Children)
            {
                this.DFS(child, result);
            }
            //след като сме обходили всички деца трябва да си свършим работата
            result.Add(tree.Value);
        }

        public IEnumerable<T> OrderBFS() //този метод а абсолютно същия като итеративния DFS, но с опашна вместо стек!
        {
            List<T> result = new List<T>();
            Queue<Tree<T>> queue = new Queue<Tree<T>>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                Tree<T> current = queue.Dequeue();
                result.Add(current.Value);
                foreach (Tree<T> child in current.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return result;
        }
    }
}
