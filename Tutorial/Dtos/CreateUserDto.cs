using System.ComponentModel.DataAnnotations;

namespace IntergrateMongodb.Dtos
{
    public class CreateUserDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Role { get; set; } = "User";
    }
}
