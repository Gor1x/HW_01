using System;
using System.Collections.Generic;

namespace HW1.MinStack
{
    public class MinStack<T> where T : IComparable<T>
    {
        private Stack<T> _data = new Stack<T>();
        private Stack<T> _minimums = new Stack<T>();
        public int Size { get; private set; }

        public void Clear()
        {
            _data.Clear();
            _minimums.Clear();
            Size = 0;
        }

        public void Push(T value)
        {
            if (_minimums.Count == 0 || value.CompareTo(_minimums.Peek()) < 0)
            {
                _minimums.Push(value);
            }
            else
            {
                _minimums.Push(_minimums.Peek());
            }
            _data.Push(value);
            Size++;
        }

        public T MinValue()
        {
            return _minimums.Peek();
        }

        public T Peek()
        {
            return _data.Peek();
        }

        public T Pop()
        {
            Size--;
            _minimums.Pop();
            return _data.Pop();
        }
    }
}
