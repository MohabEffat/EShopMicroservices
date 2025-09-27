using User.Api.Data.Enums;

namespace User.Api.Data.Dtos
{
    public class RegisterDto
    {
        public string UserName { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string EmailAddress { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string ConfirmPassword { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public string DateOfBirth { get; set; } = default!;

    }
}
