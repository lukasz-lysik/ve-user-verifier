using System.Linq;
using UserVerifier.Common;
using UserVerifier.Password.Policies;
using UserVerifier.ValueObjects;

namespace UserVerifier.Password.Generators
{
    public class SimplePasswordGenerator : IPasswordGenerator
    {
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        private readonly IRandomNumberGenerator _randomGenerator;

        public SimplePasswordGenerator(IRandomNumberGenerator randomGenerator)
        {
            _randomGenerator = randomGenerator;
        }

        public OneTimePassword Generate(
            IPasswordPolicy passwordPolicy,
            UserId userId)
        {
            var password = new string(Enumerable.Repeat(Chars, passwordPolicy.Length)
                .Select(s => s[_randomGenerator.Next(s.Length)]).ToArray());

            return new OneTimePassword(password);
        }
    }
}