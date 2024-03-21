using StackExchange.Redis;

namespace RedisLibrary;

public interface IStream : IDisposable
{
    Task AddStreamAsync(string streamName, string streamField, string streamValue);
    Task<bool> StreamCreateConsumerGroupAsync(string streamName, string groupName);
    Task<long> RemoveAsync(string value, string streamName, long count = 1);
    Task<string> StreamInfoAsync(string streamName);
    Task<StreamEntry[]> GetAllStreamsAsync(string streamName);
}
