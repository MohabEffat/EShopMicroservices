using User.Api.Data.Dtos;
using User.Api.Data.Models;

namespace User.Api.Actions.Login
{
    public record LoginCommand(LoginDto LoginDto) : ICommand<LoginResult>;
    public record LoginResult(UserDto UserDto);
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.LoginDto.EmailAddress)
                .NotEmpty().WithMessage("EmailAddress is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.LoginDto.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
        }
    }
    public class LoginCommandHandler(SignInManager<ApplicationUser> signInManager,
        IJwtService jwtService): ICommandHandler<LoginCommand, LoginResult>
    {
        public async Task<LoginResult> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var user = await signInManager.UserManager.FindByEmailAsync(command.LoginDto.EmailAddress);

            if (user is null)
                throw new NotFoundException($"User not found - {command.LoginDto.EmailAddress}");

            var result = await signInManager
                .CheckPasswordSignInAsync(user, command.LoginDto.Password, false);

            if (!result.Succeeded)
                throw new BadRequestException("Invalid password.");

            var userDto = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName!,
                EmailAddress = user.Email!,
                Token = await jwtService.GenerateTokenAsync(user)
            };
            return new LoginResult(userDto);
        }
    }
}
