namespace Lab1.ListException
{
    class ListElementNotFoundException : Exception
    {
        public ListElementNotFoundException(string message) : base(message) { }
    }
}
