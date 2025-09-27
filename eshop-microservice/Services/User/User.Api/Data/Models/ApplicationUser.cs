using Microsoft.AspNetCore.Identity;
using User.Api.Data.Enums;

namespace User.Api.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int age { get; set; }
        public Gender Gender { get; set; }
        public string DateOfBirth { get; set; } = null!;
        public DateTime CreatedAt { get; set; } 

    }
}
