namespace PoCMultiplexer;

using System;
using Shared;
using StackExchange.Redis;

class Program
{
    static void Main(string[] args)
    {
        var config = new Config();
        var multiplexerCache = new MultiplexerCache(config.PrimaryRedisConnectionString, config.SecondaryRedisConnectionString);

        multiplexerCache.TestPutGet();
    }
}

class MultiplexerCache
{
    private const int DefaultDatabaseNumber = 0;
    private ConnectionMultiplexer ConnectionMultiplexer { get; }

    private IDatabase Database { get; }

    public MultiplexerCache(string primaryConnectionString, string secondaryConnectionString)
    {
        ConnectionMultiplexer = ConnectionMultiplexer.Connect($"{primaryConnectionString},{secondaryConnectionString}");
        Database = ConnectionMultiplexer.GetDatabase(DefaultDatabaseNumber);
    }

    private void Put(string key, string value)
    {
        Database.StringSet(key, value);
    }

    private string Get(string key)
    {
        return Database.StringGet(key).ToString();
    }

    public void TestPutGet()
    {
        var key = "key for multiplexer cache poc";
        Put(key, "example value 2");
        var value = Get(key);
        Console.WriteLine($"Returned value: {value}");
    }
}
