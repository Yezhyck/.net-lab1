namespace Collection
{
    public class CircularDoublyLinkedListNode<T>
    {
        public CircularDoublyLinkedListNode(T value) => Value = value;
        public T Value { get; set; }
        public CircularDoublyLinkedListNode<T>? Previous { get; set; }
        public CircularDoublyLinkedListNode<T>? Next { get; set; }
    }
}
