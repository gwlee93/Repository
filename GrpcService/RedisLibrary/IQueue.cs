namespace RedisLibrary
{
    public interface IQueue : IDisposable
    {
        long GetQueueLength();
        void Enqueue(string value);
        string Dequeue();        
        int Remove(string value, long count = 1);
        string GetItem(long index);
        IList<string> GetAllItems();
    }
}
