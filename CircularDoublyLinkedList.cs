using Lab1.ListException;
using System.Collections;

namespace Lab1
{
    public delegate void CollectionOperationEventHandler(object sender, CollectionOperationEventArgs e);

    public class CircularDoublyLinkedList<T> : IEnumerable<T>
    {
        public event CollectionOperationEventHandler CollectionOperation;

        private CircularDoublyLinkedListNode<T> _head; 
        private int _count;

        public CircularDoublyLinkedList() => _count = 0;

        public void AddAfter(CircularDoublyLinkedListNode<T> existingNode, CircularDoublyLinkedListNode<T> newNode)
        {
            if (IsEmpty()) throw new EmptyListException("The list is empty.");

            if (!Contains(existingNode)) throw new ListElementNotFoundException("The list doesn't contain the specified node.");

            if (Contains(newNode)) newNode = new CircularDoublyLinkedListNode<T>(newNode.Value);

            newNode.Previous = existingNode;
            newNode.Next = existingNode.Next;
            existingNode.Next.Previous = newNode;
            existingNode.Next = newNode;

            _count++;

            CollectionOperation?.Invoke(this, new CollectionOperationEventArgs($"Added element \"{newNode.Value}\" after element \"{existingNode.Value}\"."));
        }

        public void AddAfter(CircularDoublyLinkedListNode<T> existingNode, T value)
        {
            if (IsEmpty()) throw new EmptyListException("The list is empty.");

            if (!Contains(existingNode)) throw new ListElementNotFoundException("The list doesn't contain the specified node.");

            CircularDoublyLinkedListNode<T> newNode = new(value)
            {
                Previous = existingNode,
                Next = existingNode.Next
            };

            existingNode.Next.Previous = newNode;
            existingNode.Next = newNode;

            _count++;

            CollectionOperation?.Invoke(this, new CollectionOperationEventArgs($"Added element \"{value}\" after element \"{existingNode.Value}\"."));
        }

        public void AddBefore(CircularDoublyLinkedListNode<T> existingNode, CircularDoublyLinkedListNode<T> newNode)
        {
            if (IsEmpty()) throw new EmptyListException("The list is empty.");

            if (!Contains(existingNode)) throw new ListElementNotFoundException("The list doesn't contain the specified node.");

            if (Contains(newNode)) newNode = new CircularDoublyLinkedListNode<T>(newNode.Value);

            newNode.Previous = existingNode.Previous;
            newNode.Next = existingNode;
            existingNode.Previous.Next = newNode;
            existingNode.Previous = newNode;

            if (existingNode == _head) _head = _head.Previous;

            _count++;

            CollectionOperation?.Invoke(this, new CollectionOperationEventArgs($"Added element \"{newNode.Value}\" before element \"{existingNode.Value}\"."));
        }

        public void AddBefore(CircularDoublyLinkedListNode<T> existingNode, T value)
        {
            if (IsEmpty()) throw new EmptyListException("The list is empty.");

            if (!Contains(existingNode)) throw new ListElementNotFoundException("The list doesn't contain the specified node.");

            CircularDoublyLinkedListNode<T> newNode = new(value)
            {
                Previous = existingNode.Previous,
                Next = existingNode
            };

            existingNode.Previous.Next = newNode;
            existingNode.Previous = newNode;

            if (existingNode == _head) _head = _head.Previous;

            _count++;

            CollectionOperation?.Invoke(this, new CollectionOperationEventArgs($"Added element \"{value}\" before element \"{existingNode.Value}\"."));
        }

        public void AddFirst(CircularDoublyLinkedListNode<T> node)
        {
            AddLastNode(node);
            _head = _head.Previous;

            CollectionOperation?.Invoke(this, new CollectionOperationEventArgs($"Added element \"{node.Value}\" to the beginning of the list."));
        }

        public void AddFirst(T value)
        {
            AddLastNode(new CircularDoublyLinkedListNode<T>(value));
            _head = _head.Previous;

            CollectionOperation?.Invoke(this, new CollectionOperationEventArgs($"Added element \"{value}\" to the beginning of the list."));
        }

        public void AddLast(CircularDoublyLinkedListNode<T> node)
        {
            AddLastNode(node);

            CollectionOperation?.Invoke(this, new CollectionOperationEventArgs($"Added element \"{node.Value}\" to the end of the list."));
        }

        public void AddLast(T value)
        {
            AddLastNode(new CircularDoublyLinkedListNode<T>(value));

            CollectionOperation?.Invoke(this, new CollectionOperationEventArgs($"Added element \"{value}\" to the end of the list."));
        }

        public void Clear()
        {
            _head = null;
            _count = 0;

            CollectionOperation?.Invoke(this, new CollectionOperationEventArgs($"List cleared."));
        }

        public bool Contains(T value)
        {
            if (IsEmpty()) return false;

            CircularDoublyLinkedListNode<T> currentNode = _head;

            do
            {
                if (currentNode != null)
                {
                    if (currentNode.Value != null && currentNode.Value.Equals(value)) return true;

                    currentNode = currentNode.Next;
                }
            } while (currentNode != _head);

            return false;
        }

        public CircularDoublyLinkedListNode<T> Find(T value)
        {
            CircularDoublyLinkedListNode<T> currentNode = _head;

            do
            {
                if (currentNode != null)
                {
                    if (currentNode.Value != null && currentNode.Value.Equals(value))
                    {
                        CollectionOperation?.Invoke(this, new CollectionOperationEventArgs($"Found element \"{value}\"."));
                        return currentNode;
                    }

                    currentNode = currentNode.Next;
                }
            } while (currentNode != _head);

            throw new ListElementNotFoundException("The list doesn't contain the specified value."); 
        }

        public CircularDoublyLinkedListNode<T> FindLast(T value)
        {
            CircularDoublyLinkedListNode<T> currentNode = !IsEmpty() && _count > 1 ? _head.Previous : _head;
            
            do
            {
                if (currentNode != null)
                {
                    if (currentNode.Value != null && currentNode.Value.Equals(value))
                    {
                        CollectionOperation?.Invoke(this, new CollectionOperationEventArgs($"Found the last element \"{value}\"."));
                        return currentNode;
                    }

                    currentNode = currentNode.Previous;
                }
            } while (currentNode != _head.Previous);

            throw new ListElementNotFoundException("The list doesn't contain the specified value.");
        }

        public void Remove(CircularDoublyLinkedListNode<T> node)
        {
            if (IsEmpty()) throw new EmptyListException("List is empty.");

            if (!Contains(node)) throw new ListElementNotFoundException("The list doesn't contain the specified node.");

            if (Count() == 1) _head = null;
            else RemoveExistingNode(node);

            _count--;

            CollectionOperation?.Invoke(this, new CollectionOperationEventArgs($"Removed element \"{node.Value}\".")); ;
        }

        public void Remove(T value)
        {
            if (IsEmpty()) throw new EmptyListException("List is empty.");
          
            CircularDoublyLinkedListNode<T> currentNode = Find(value);

            if (Count() == 1) _head = null;
            else RemoveExistingNode(currentNode);

            _count--;

            CollectionOperation?.Invoke(this, new CollectionOperationEventArgs($"Removed element \"{value}\"."));
        }

        public void RemoveFirst()
        {
            if (IsEmpty()) throw new EmptyListException("List is empty.");

            CircularDoublyLinkedListNode<T> removedNode = _head;

            if (Count() == 1) _head = null;
            else 
            {
                _head = _head.Next;

                RemoveLastExistingNode();
            }

            _count--; 

            CollectionOperation?.Invoke(this, new CollectionOperationEventArgs($"Removed the first element \"{removedNode.Value}\"."));
        }

        public void RemoveLast()
        {
            if (IsEmpty()) throw new EmptyListException("List is empty.");

            CircularDoublyLinkedListNode<T> removedNode;

            if (Count() == 1)
            {
                removedNode = _head;
                _head = null;
            }
            else
            {
                removedNode = _head.Previous;
                RemoveLastExistingNode();
            }

            _count--;

            CollectionOperation?.Invoke(this, new CollectionOperationEventArgs($"Removed the last element \"{removedNode.Value}\"."));
        }

        public int Count() 
        { 
            return _count; 
        }

        public bool IsEmpty()
        {
            return _count == 0;
        }

        private bool Contains(CircularDoublyLinkedListNode<T> node)
        {
            if (_head == null) return false;

            CircularDoublyLinkedListNode<T> currentNode = _head;

            do
            {
                if (currentNode != null)
                {
                    if (currentNode.Equals(node)) return true;

                    currentNode = currentNode.Next;
                }
            } while (currentNode != _head);

            return false;
        }

        private void AddLastNode(CircularDoublyLinkedListNode<T> node)
        {
            if (IsEmpty())
            {
                _head = node;
                _head.Previous = _head;
                _head.Next = _head;
            }
            else
            {
                node.Previous = _head.Previous;
                node.Next = _head;
                _head.Previous.Next = node;
                _head.Previous = node;
            }

            _count++;
        }

        private void RemoveExistingNode(CircularDoublyLinkedListNode<T> node)
        {
            if (node == _head) _head = _head.Next;

            node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;
        }

        private void RemoveLastExistingNode()
        {
            _head.Previous.Previous.Next = _head;
            _head.Previous = _head.Previous.Previous;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>) this).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            CircularDoublyLinkedListNode<T> currentNode = _head;

            do
            {
                if (currentNode != null)
                {
                    yield return currentNode.Value;
                    currentNode = currentNode.Next;
                }
            } while (currentNode != _head);
        }

        public override string ToString()
        {
            T[] values = new T[_count];
            CircularDoublyLinkedListNode<T> currentNode = _head;
            int index = 0;

            do
            {
                if (currentNode != null)
                {
                    values[index++] = currentNode.Value;
                    currentNode = currentNode.Next;
                }
            } while (currentNode != _head);

            return String.Join(", ", values);
        }
    }
}
