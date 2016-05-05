using UserVerifier.ValueObjects;

namespace UserVerifier
{
    public interface IUserVerifier
    {
        OneTimePassword Generate(UserId userId);
        bool IsValid(UserId userId, OneTimePassword password);
    }
}