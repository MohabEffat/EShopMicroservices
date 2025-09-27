namespace User.Api.Data.Dtos
{
    public class LoginDto
    {
        public string EmailAddress { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
