// See https://aka.ms/new-console-template for more information
using Lab1;

class Program
{
    public static void Main()
    {
        CircularDoublyLinkedList<string> strings = new();
        strings.CollectionOperation += OnCollectionOperation;

        strings.AddFirst("a");
        strings.AddFirst("b");

        strings.AddLast("c");
        strings.AddLast("d");

        //CircularDoublyLinkedListNode<string> node = strings.Find("d");
        CircularDoublyLinkedListNode<string> node = strings.Find("c");

        strings.AddAfter(node, new CircularDoublyLinkedListNode<string>("e"));
        strings.AddAfter(node, "f");

        strings.AddBefore(node, "g");
        strings.AddBefore(node, "h");

        strings.Clear();

        //strings.FindLast("f");

        //strings.Find("f");

        Console.WriteLine(strings.Contains("a"));

        strings.AddLast("a");
        strings.AddLast("b");
        strings.AddLast("c");
        strings.AddLast("d");
        strings.AddLast("e");
        strings.AddLast("f");
        strings.AddLast("g");

        Console.WriteLine(strings.Contains("a"));

        strings.Find("f");

        Console.WriteLine(strings.FindLast("a").Next.Value);

        strings.AddAfter(strings.Find("b"), "a");

        Console.WriteLine(strings.FindLast("g").Next.Value); 

        strings.Remove(strings.FindLast("a"));

        strings.Remove(strings.FindLast("g"));

        //strings.Remove(new CircularDoublyLinkedListNode<string>("e"));

        Console.WriteLine("--ggggg-------------------------");

        strings.AddLast("a");
        strings.AddLast("b");
        strings.AddLast("c");
        strings.AddLast("d");
        strings.AddLast("e");
        strings.AddLast("f");
        strings.AddLast("g");

        strings.Clear();

        strings.AddLast("a");
        strings.AddLast("b");
        strings.AddLast("c");
        strings.AddLast("d");
        strings.AddLast("e");
        strings.AddLast("f");
        strings.AddLast("g");

        strings.Remove(strings.Find("d").Value);

        strings.Remove(strings.Find("e").Previous.Value);

        //strings.Remove("r");

        Console.WriteLine("--ggggg-------------------------");

        strings.RemoveFirst();

        strings.Clear();

        strings.AddFirst("a");

        strings.RemoveLast();

        strings.AddLast("a");
        strings.AddLast("b");
        strings.AddLast("c");
        strings.AddLast("d");
        strings.AddLast("e");
        strings.AddLast("f");
        strings.AddLast("g");

        strings.RemoveLast();

        strings.Clear();

        strings.AddFirst("a");

        strings.RemoveLast();

    }

    public static void OnCollectionOperation(object sender, CollectionOperationEventArgs e)
    {
        Console.WriteLine(e.Message);
        Console.WriteLine(sender);
        Console.WriteLine(((CircularDoublyLinkedList<string>) sender).Count());
        Console.WriteLine($"-------------------------");
    }
}

