using System.ComponentModel.DataAnnotations;

namespace IntergrateMongodb.Dtos
{
    public class UpdateUserDto
    { 
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public int Age { get; set; }
    }
}
