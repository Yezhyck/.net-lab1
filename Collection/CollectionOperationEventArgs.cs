namespace Collection
{
    public class CollectionOperationEventArgs : EventArgs
    {
        private readonly CollectionChangedAction _action;
        private readonly object? _item;

        public CollectionOperationEventArgs()
        {
        }

        public CollectionOperationEventArgs(CollectionChangedAction action)
        {
            _action = action;
        }

        public CollectionOperationEventArgs(CollectionChangedAction action, object item)
        {
            _action = action;
            _item = item;
        }

        public CollectionChangedAction Action
        {
            get { return _action; }
        }

        public object? Item
        {
            get { return _item; }
        }
    }
}
