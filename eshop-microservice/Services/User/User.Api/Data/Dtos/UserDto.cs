namespace User.Api.Data.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = default!;
        public string EmailAddress { get; set; } = default!;
        public string Token { get; set; } = default!;
    }
}
