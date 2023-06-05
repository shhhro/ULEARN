using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryTrees
{
    public class BinaryTree<T> : IEnumerable<T> where T : IComparable
    {
        private Node<T> treeRoot;

        public void Add(T value)
        {
            var node = treeRoot;
            var newNode = new Node<T>(value);
            if (treeRoot == null) treeRoot = newNode;
            else
                while (true)
                {
                    node.Size++;
                    if (node.Value.CompareTo(value) > 0)
                    {
                        if (node.Left == null)
                        { node.Left = newNode; break; }
                        node = node.Left;
                    }
                    else
                    {
                        if (node.Right == null)
                        { node.Right = newNode; break; } 
                        node = node.Right;
                    }
                }
        }

        public bool Contains(T value)
        {
            var node = treeRoot;
            while (node != null)
            {
                var result = node.Value.CompareTo(value);
                if (result == 0) return true;
                if (result > 0) node = node.Left;
                else node = node.Right;
            }
            return false;
        }

        public T this[int index]
        {
            get
            {
                var root = treeRoot;
                while (true)
                {
                    if (root == null) continue;
                    var leftSize = root.Left != null ? root.Left.Size : 0;
                    if (index == leftSize) return root.Value;
                    if (index < leftSize) root = root.Left;
                    else if (index > leftSize)
                    {
                        root = root.Right;
                        index -= leftSize + 1;
                    }
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetValue(treeRoot).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static IEnumerable<T> GetValue(Node<T> node)
        {
            while (true)
            {
                if (node == null) yield break;
                foreach (var value in GetValue(node.Left))
                    yield return value;
                yield return node.Value;
                node = node.Right;
            }
        }
    }
    public class Node<T>
    {
        public T Value { get; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }

        public int Size = 1;
        public Node(T value)
        { Value = value; }
    }
}