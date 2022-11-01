using System.Collections;
using System.Collections.Generic;

namespace Pool
{
    public class Pool<T>
    {
        private Node<T> _current;
        private int _count;
        
        public void Add(T data)
        {
            Node<T> node = new Node<T>(data);
            // if list empty
            if (_current == null)
            {
                _current = node;
                _current.Next = _current;
            }
            else
            {
                node.Next = _current.Next;
                _current.Next = node;
                _current = node;
            }
            _count++;
        }
        
        public T GetValue()
        {
            return _current.Data;
        }

        public T GetNext()
        {
            return _current.Next.Data;
        }

        public void Next()
        {
            _current = _current.Next;
        }

        public int Count => _count;
        public bool IsEmpty => _count == 0;
    }
}
