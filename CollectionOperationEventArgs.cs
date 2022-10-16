namespace Lab1
{
    public class CollectionOperationEventArgs : EventArgs
    {
        public string Message;

        public CollectionOperationEventArgs(string message)
        {
            Message = message;
        }
    }
}
