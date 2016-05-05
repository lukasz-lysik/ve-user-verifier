using UserVerifier.Password.Policies;
using UserVerifier.ValueObjects;

namespace UserVerifier.Password.Generators
{
    public interface IPasswordGenerator
    {
        OneTimePassword Generate(IPasswordPolicy policy, UserId userId);
    }
}