using System.Collections;

namespace Collection
{
    public delegate void CollectionOperationEventHandler(object sender, CollectionOperationEventArgs e);

    public class CircularDoublyLinkedList<T> : IEnumerable<T>
    {
        public event CollectionOperationEventHandler? CollectionOperation;
        private CircularDoublyLinkedListNode<T>? _head;
        private int _count;

        public CircularDoublyLinkedList()
        {
        }

        public CircularDoublyLinkedList(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            foreach (T item in collection)
                AddLast(item);
        }

        public int Count 
        { 
            get { return _count; } 
        }

        public CircularDoublyLinkedListNode<T>? First
        {
            get { return _head; }
        }

        public CircularDoublyLinkedListNode<T>? Last
        {
            get { return _head?.Previous; }
        }

        public void AddAfter(CircularDoublyLinkedListNode<T> node, CircularDoublyLinkedListNode<T> newNode)
        { 
            ValidateNode(node);
            ValidateNewNode(newNode);
            InsertNodeBefore(node.Next!, newNode);
            CollectionOperation?.Invoke(this, new CollectionOperationEventArgs(CollectionChangedAction.Add, newNode));
        }

        public CircularDoublyLinkedListNode<T> AddAfter(CircularDoublyLinkedListNode<T> node, T value)
        {
            ValidateNode(node);
            CircularDoublyLinkedListNode<T> newNode = new(value);
            InsertNodeBefore(node.Next!, newNode);
            CollectionOperation?.Invoke(this, new CollectionOperationEventArgs(CollectionChangedAction.Add, value));
            return newNode;
        }

        public void AddBefore(CircularDoublyLinkedListNode<T> node, CircularDoublyLinkedListNode<T> newNode)
        {
            ValidateNode(node);
            ValidateNewNode(newNode);
            InsertNodeBefore(node!, newNode);
            if (node == _head)
                _head = newNode;
            CollectionOperation?.Invoke(this, new CollectionOperationEventArgs(CollectionChangedAction.Add, newNode));
        }

        public CircularDoublyLinkedListNode<T> AddBefore(CircularDoublyLinkedListNode<T> node, T value)
        {
            ValidateNode(node);
            CircularDoublyLinkedListNode<T> newNode = new(value);
            InsertNodeBefore(node!, newNode);
            if (node == _head)
                _head = newNode;
            CollectionOperation?.Invoke(this, new CollectionOperationEventArgs(CollectionChangedAction.Add, value));
            return newNode;
        }

        public void AddFirst(CircularDoublyLinkedListNode<T> node)
        {
            ValidateNewNode(node);
            if (_head == null)
                InsertNodeToEmptyList(node);
            else
            {
                InsertNodeBefore(_head, node);
                _head = _head.Previous;
            }
            CollectionOperation?.Invoke(this, new CollectionOperationEventArgs(CollectionChangedAction.Add, node));
        }

        public CircularDoublyLinkedListNode<T> AddFirst(T value)
        {
            CircularDoublyLinkedListNode<T> node = new(value);
            if (_head == null)
                InsertNodeToEmptyList(node);
            else
            {
                InsertNodeBefore(_head, node);
                _head = _head.Previous;
            }
            CollectionOperation?.Invoke(this, new CollectionOperationEventArgs(CollectionChangedAction.Add, value));
            return node;
        }

        public void AddLast(CircularDoublyLinkedListNode<T> node)
        {
            ValidateNewNode(node);
            if (_head == null)
                InsertNodeToEmptyList(node);
            else
                InsertNodeBefore(_head, node);
            CollectionOperation?.Invoke(this, new CollectionOperationEventArgs(CollectionChangedAction.Add, node));
        }

        public CircularDoublyLinkedListNode<T> AddLast(T value)
        {
            CircularDoublyLinkedListNode<T> node = new(value);
            if (_head == null)
                InsertNodeToEmptyList(node);
            else
                InsertNodeBefore(_head, node);
            CollectionOperation?.Invoke(this, new CollectionOperationEventArgs(CollectionChangedAction.Add, value));
            return node;
        }

        public void Clear()
        {
            _head = null;
            _count = 0;
            CollectionOperation?.Invoke(this, new CollectionOperationEventArgs(CollectionChangedAction.Clear));
        }

        public bool Contains(T value)
        {
            return Find(value) != null;
        }

        public CircularDoublyLinkedListNode<T>? Find(T value)
        {
            CircularDoublyLinkedListNode<T>? currentNode = _head;
            do
            {
                if (currentNode != null)
                {
                    if (currentNode.Value!.Equals(value))
                    {
                        CollectionOperation?.Invoke(this, new CollectionOperationEventArgs(CollectionChangedAction.Find, value));
                        return currentNode;
                    }

                    currentNode = currentNode.Next;
                }
            } while (currentNode != _head);
            return null;
        }

        public CircularDoublyLinkedListNode<T>? FindLast(T value)
        {
            if (_head == null)
                return null;
            CircularDoublyLinkedListNode<T>? currentNode = _head.Previous;
            do
            {
                if (currentNode != null)
                {
                    if (currentNode.Value!.Equals(value))
                    {
                        CollectionOperation?.Invoke(this, new CollectionOperationEventArgs(CollectionChangedAction.Find, value));
                        return currentNode;
                    }
                    currentNode = currentNode.Previous;
                }
            } while (currentNode != _head.Previous); 
            return null;
        }

        public void Remove(CircularDoublyLinkedListNode<T> node)
        {
            ValidateNode(node);
            RemoveNode(node);
            CollectionOperation?.Invoke(this, new CollectionOperationEventArgs(CollectionChangedAction.Remove, node));
        }

        public bool Remove(T value)
        {
            CircularDoublyLinkedListNode<T>? node = Find(value);
            if (node != null)
            {
                RemoveNode(node);
                CollectionOperation?.Invoke(this, new CollectionOperationEventArgs(CollectionChangedAction.Remove, value));
                return true;
            }
            return false;
        }

        public void RemoveFirst()
        {
            ValidateListEmptyness();
            CircularDoublyLinkedListNode<T> removedNode = _head!;
            RemoveNode(_head!);
            CollectionOperation?.Invoke(this, new CollectionOperationEventArgs(CollectionChangedAction.Remove, removedNode));
        }

        public void RemoveLast()
        {
            ValidateListEmptyness();
            CircularDoublyLinkedListNode<T> removedNode = _head!.Previous!;
            RemoveNode(_head!.Previous!);
            CollectionOperation?.Invoke(this, new CollectionOperationEventArgs(CollectionChangedAction.Remove, removedNode));
        }

        public override string ToString()
        {
            T[] values = new T[_count];
            CircularDoublyLinkedListNode<T>? currentNode = _head;
            int index = 0;
            do
            {
                if (currentNode != null)
                {
                    values[index++] = currentNode.Value;
                    currentNode = currentNode.Next;
                }
            } while (currentNode != _head);
            return $"[{String.Join(", ", values)}]";
        }

        private bool Contains(CircularDoublyLinkedListNode<T> node)
        {
            if (_head == null) return false;
            CircularDoublyLinkedListNode<T> currentNode = _head;
            do
            {
                if (currentNode != null)
                {
                    if (currentNode == node)
                        return true;
                    currentNode = currentNode.Next!;
                }
            } while (currentNode != _head);
            return false;
        }

        private void ValidateNode(CircularDoublyLinkedListNode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            if (!Contains(node))
                throw new KeyNotFoundException("The LinkedList node does not belong to current LinkedList.");
        }

        private void ValidateNewNode(CircularDoublyLinkedListNode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            if (Contains(node))
                throw new InvalidOperationException("The LinkedList node already belongs to a LinkedList.");
        }

        private void ValidateListEmptyness()
        {
            if (_head == null)
                throw new InvalidOperationException("The LinkedList is empty.");
        }

        private void InsertNodeToEmptyList(CircularDoublyLinkedListNode<T> node)
        {
            _head = node;
            _head.Previous = node;
            _head.Next = node;
            _count++;
        }

        private void InsertNodeBefore(CircularDoublyLinkedListNode<T> node, CircularDoublyLinkedListNode<T> newNode)
        {
            newNode.Previous = node.Previous;
            newNode.Next = node;
            node.Previous!.Next = newNode;
            node.Previous = newNode;
            _count++;
        }

        private void RemoveNode(CircularDoublyLinkedListNode<T> node)
        {
            if (node.Next == node)
                _head = null;
            else
            {
                node.Previous!.Next = node.Next;
                node.Next!.Previous = node.Previous;
                if (node == _head)
                    _head = _head.Next;
            }
            _count--;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>) this).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            CircularDoublyLinkedListNode<T>? currentNode = _head;
            do
            {
                if (currentNode != null)
                {
                    yield return currentNode.Value;
                    currentNode = currentNode.Next;
                }
            } while (currentNode != _head);
        }
    }
}
