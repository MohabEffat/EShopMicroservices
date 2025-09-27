using User.Api.Data.Dtos;
using User.Api.Data.Models;

namespace User.Api.Actions.Register
{
    public record RegisterCommand(RegisterDto RegisterDto) : ICommand<RegisterResult>;
    public record RegisterResult (UserDto UserDto);

    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.RegisterDto.FirstName)
                .NotEmpty().WithMessage("First Name is required.")
                .MaximumLength(50).WithMessage("First Name cannot exceed 50 characters.");
            RuleFor(x => x.RegisterDto.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(30).WithMessage("Username cannot exceed 30 characters.");
            RuleFor(x => x.RegisterDto.LastName)
                .NotEmpty().WithMessage("Last Name is required.")
                .MaximumLength(50).WithMessage("Last Name cannot exceed 50 characters.");
            RuleFor(x => x.RegisterDto.EmailAddress)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.RegisterDto.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
            RuleFor(x => x.RegisterDto.ConfirmPassword)
                .Equal(x => x.RegisterDto.Password).WithMessage("Passwords do not match.");
            RuleFor(x => x.RegisterDto.Phone).Length(10).WithMessage("Phone number must be 10 digits.");
            RuleFor(x => x.RegisterDto.DateOfBirth)
                .NotEmpty().WithMessage("Date of Birth is required.")
                .Must(date => DateTime.TryParse(date, out _)).WithMessage("Invalid Date of Birth format.");
            RuleFor(x => x.RegisterDto.Age)
                .GreaterThanOrEqualTo(16).WithMessage("Age must be at least 16.")
                .LessThan(80).WithMessage("Age must be less than 80.");
        }
    }

    public class RegisterCommandHandler(UserManager<ApplicationUser> userManager, IJwtService jwtService) : ICommandHandler<RegisterCommand, RegisterResult>
    {
        public async Task<RegisterResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            var existingUser = await userManager.FindByEmailAsync(command.RegisterDto.EmailAddress);
            if (existingUser is not null)
                throw new BadRequestException("User with this email already exists.");

            var newUser = new ApplicationUser
            {
                Email = command.RegisterDto.EmailAddress,
                FirstName = command.RegisterDto.FirstName,
                LastName = command.RegisterDto.LastName,
                UserName = command.RegisterDto.UserName,
                PhoneNumber = command.RegisterDto.Phone,
                Gender = command.RegisterDto.Gender,
                DateOfBirth = command.RegisterDto.DateOfBirth
            };

            var result = await userManager.CreateAsync(newUser, command.RegisterDto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new BadRequestException($"User creation failed: {errors}");
            }

            return new RegisterResult(new UserDto
            {
                Id = newUser.Id,
                EmailAddress = newUser.Email,
                Token = await jwtService.GenerateTokenAsync(newUser)
            });
        }

    }
}
