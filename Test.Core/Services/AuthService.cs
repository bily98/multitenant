using Ardalis.Result;
using Test.Core.Entities;
using Test.Core.Interfaces;
using Test.Core.Specifications.Users;
using Test.SharedKernel.Interfaces;

namespace Test.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly ISecurityAsyncRepository<User> _userRepository;
        private readonly IPasswordService _passwordService;

        public AuthService(ISecurityAsyncRepository<User> userRepository, IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
        }

        public async Task<Result<User>> Login(string email, string password)
        {
            try
            {
                var specification = new GetByEmailSpecification(email);
                var user = await _userRepository.FirstOrDefaultAsync(specification);

                if (user == null)
                    return Result<User>.NotFound();

                var isValid = _passwordService.Check(user.Password, password);

                if (!isValid)
                    return Result<User>.Invalid(new List<ValidationError>
                    {
                        new ValidationError
                        {
                            ErrorCode = "InvalidPassword",
                            ErrorMessage = "Invalid password",
                            Identifier = "Password",
                            Severity = ValidationSeverity.Error
                        }
                    });

                return Result<User>.Success(user);
            }
            catch (Exception e)
            {
                return Result<User>.Error(e.Message);
            }
        }
    }
}
