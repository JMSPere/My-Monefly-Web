using Microsoft.Extensions.Configuration;
using MonefyWeb.Infraestructure.RepositoryWebPage.Contracts;
using MonefyWeb.Infrastructure.DataModels.Response;
using MonefyWeb.Utils.ServiceAgents;
using NRedisStack;
using NRedisStack.RedisStackCommands;
using StackExchange.Redis;

namespace MonefyWeb.Infraestructure.RepositoryWebPage.Implementations
{
    public class RedisCache : IRedisCache
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        private readonly IDatabase _database;
        private readonly IConfiguration _configuration;
        private readonly JsonCommands _jsonCommands;

        private readonly string _connectionString = "RedisConnection";

        public RedisCache(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionMultiplexer = ConnectionMultiplexer.Connect(_configuration.GetConnectionString(_connectionString));
            _database = _connectionMultiplexer.GetDatabase();
            _jsonCommands = _database.JSON();
            _database.Ping();
        }
        public bool AlreadyExistsInCache(string key)
        {
            return _database.KeyExists(key);
        }

        public async Task<bool> SetAsyncJson(string key, string value)
        {
            if (AlreadyExistsInCache(key))
            {
                throw new Exception("Key with same name already exists!");
            }
            await _jsonCommands.SetAsync(key, "$", value);

            return await _database.KeyExpireAsync(key, TimeSpan.FromHours(1));
        }

        public async Task<string> GetAsyncJson(string key)
        {
            if (AlreadyExistsInCache(key))
            {
                var redisResult = await _jsonCommands.GetAsync(key);

                return redisResult.ToString();
            }
            else
                throw new Exception("Non existent key!");
        }
        public async Task<List<AlphaVantageResponse>> GetAllAsyncJson()
        {
            var alphaVantageList = new List<AlphaVantageResponse>();

            var keys = _connectionMultiplexer.GetServer(_connectionMultiplexer.GetEndPoints().First()).Keys(pattern: "*");

            foreach (var key in keys)
            {
                var redisValue = await _jsonCommands.GetAsync(key);

                var alphaVantageResponses = Serialization.JsonDeserialize(redisValue.ToString());
                foreach (var alphaVantageResponse in alphaVantageResponses)
                {
                    alphaVantageList.Add(alphaVantageResponse);
                }
            }

            return alphaVantageList;
        }

        public async Task<bool> HasData()
        {
            var keys = _connectionMultiplexer.GetServer(_connectionMultiplexer.GetEndPoints().First()).Keys(pattern: "*");

            if (keys.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
