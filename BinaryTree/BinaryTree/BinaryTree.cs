using System;

namespace BinaryTree
{
    public class BinaryTree<T>
    {
        public BinaryTree(T value, BinaryTree<T> leftChild = null, BinaryTree<T> rightChild = null)
        {
            this.Value = value;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
        }

        public T Value { get; set; }

        public BinaryTree<T> LeftChild { get; set; }

        public BinaryTree<T> RightChild { get; set; }

        //Root -> Left -> Right
        public void PrintIndentedPreOrder(int indent = 0)
        {
            Console.WriteLine(new string(' ', indent) + this.Value); //принтираме текущия елемент

            if (this.LeftChild != null)
            {
                this.LeftChild.PrintIndentedPreOrder(indent + 2);
            }
            if (this.RightChild != null)
            {
                this.RightChild.PrintIndentedPreOrder(indent + 2);
            }
        }

        //Left -> Root -> Right
        //може и направя и по горния образец!
        public void EachInOrder(Action<T> action) // Action<T> - делегат; референция към метод. На един метод можем да подадем друг метод
        {
            this.EachInOrder(this, action);
        }

        //Left -> Right -> Root
        public void EachPostOrder(Action<T> action)
        {
            EachPostOrder(this, action);
        }

        //правим допълнителен helper метод, който всъщност е рекурсията, а горния само го извиква
        private void EachInOrder(BinaryTree<T> node, Action<T> action) //вече имаме референция към това, което искаме да проверяваме дали е null
        {
            if (node == null) //тази проверка ще ни предпази от Null reference exception; ще ни изкара от рекурсията.
            {
                return;
            }

            EachInOrder(node.LeftChild, action);
            action(node.Value);
            EachInOrder(node.RightChild, action);
        }

        private void EachPostOrder(BinaryTree<T> node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            EachPostOrder(node.LeftChild, action);
            EachPostOrder(node.RightChild, action);
            action(node.Value);
        }
    }

}
