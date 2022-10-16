namespace Lab1
{
    public class CircularDoublyLinkedListNode<T>
    {
        public T Value { get; set; }
        public CircularDoublyLinkedListNode<T> Previous { get; set; }
        public CircularDoublyLinkedListNode<T> Next { get; set; }

        public CircularDoublyLinkedListNode(T value) => Value = value;
    }
}
