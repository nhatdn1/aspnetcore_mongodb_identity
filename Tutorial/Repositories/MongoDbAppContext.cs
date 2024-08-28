using IntergrateMongodb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDbGenericRepository;
using NuGet.Configuration;

namespace IntergrateMongodb.Repositories
{
    public class MongoDbAppContext : MongoDbContext
    {
        public MongoDbAppContext(IOptions<AppSettings> settings) : base(settings.Value.MongoDbSettings.ConnectionString, settings.Value.MongoDbSettings.DatabaseName)
        {

        } 
    }
}
