using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using Test.Core.Interfaces;
using Test.SharedKernel.Options;

namespace Test.Core.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordOptions _options;

        public PasswordService(IOptions<PasswordOptions> options)
        {
            _options = options.Value;
        }

        public bool Check(string hash, string password)
        {
            var parts = hash.Split('.');
            if (parts.Length != 2)
                throw new FormatException("Unexpected hash format");

            var salt = Convert.FromBase64String(parts[0]);
            var key = Convert.FromBase64String(parts[1]);

            using (var algorithm =
                   new Rfc2898DeriveBytes(password, salt, _options.Iterations, HashAlgorithmName.SHA256))
            {
                var keyToCheck = algorithm.GetBytes(_options.KeySize);
                return keyToCheck.SequenceEqual(key);
            }
        }

        public string Hash(string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes(password, _options.SaltSize, _options.Iterations,
                       HashAlgorithmName.SHA256))
            {
                var salt = Convert.ToBase64String(algorithm.Salt);
                var key = Convert.ToBase64String(algorithm.GetBytes(_options.KeySize));

                return $"{salt}.{key}";
            }
        }
    }
}
