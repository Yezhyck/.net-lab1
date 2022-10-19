using Collection;

namespace Lab1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CircularDoublyLinkedList<string> list = new(new string[] { "a", "b", "c", "d", "e", "f", "g", "h" });

            list.CollectionOperation += OnCollectionOperation;

            foreach (string item in list)
                Console.WriteLine(item);

            list.Clear();

            list.AddFirst("b");
            Console.WriteLine($"List: {list}");

            list.AddFirst(new CircularDoublyLinkedListNode<string>("a"));
            Console.WriteLine($"List: {list}");

            list.AddLast("c");
            Console.WriteLine($"List: {list}");

            list.AddLast(new CircularDoublyLinkedListNode<string>("f"));
            Console.WriteLine($"List: {list}");

            list.AddBefore(list.Find("f")!, "e");
            Console.WriteLine($"List: {list}");

            list.AddBefore(list.Find("e")!, new CircularDoublyLinkedListNode<string>("d"));
            Console.WriteLine($"List: {list}");

            list.AddAfter(list.Find("f")!, "g");
            Console.WriteLine($"List: {list}");

            list.AddAfter(list.Find("g")!, new CircularDoublyLinkedListNode<string>("h"));
            Console.WriteLine($"List: {list}");

            list.AddAfter(list.Find("b")!, "f");
            Console.WriteLine($"List: {list}");

            Console.WriteLine($"Next value after found: {list.FindLast("f")!.Next!.Value}");

            list.Remove("f");
            Console.WriteLine($"List: {list}");

            list.Remove(list.Find("g")!);
            Console.WriteLine($"List: {list}");

            list.RemoveFirst();
            Console.WriteLine($"List: {list}");

            list.RemoveLast();
            Console.WriteLine($"List: {list}");
        }

        public static void OnCollectionOperation(object sender, CollectionOperationEventArgs e)
        {
            Console.WriteLine($"Action type: {e.Action}");
            if (e.Action != CollectionChangedAction.Clear)
                Console.WriteLine($"Item: {e.Item}");
        }
    }
}
