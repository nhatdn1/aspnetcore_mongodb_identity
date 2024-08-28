using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.AspNetCore.Identity;

namespace IntergrateMongodb.Models
{
    public class AppRole : MongoIdentityRole<Guid>
    {
    }
}
