using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace IntergrateMongodb.Models
{
    public class AppUser : MongoIdentityUser<Guid>
    { 
        [MaxLength(100)]
        public string? FirstName { get; set; }
        [MaxLength(100)]
        public string? LastName { get; set; }
        public int Age { get; set; }
        [BsonDateTimeOptions]
        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}
