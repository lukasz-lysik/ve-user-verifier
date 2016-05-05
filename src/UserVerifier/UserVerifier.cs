using UserVerifier.Common;
using UserVerifier.Entities;
using UserVerifier.Password.Generators;
using UserVerifier.Password.Policies;
using UserVerifier.Repositories;
using UserVerifier.ValueObjects;

namespace UserVerifier
{
    public class UserVerifier : IUserVerifier
    {
        private readonly IDependencies _dependencies;

        public interface IDependencies
        {
            IUserPasswordRepository UserPasswordRepository { get; }
            IPasswordGenerator PasswordGenerator { get; }
            IPasswordPolicy PasswordPolicy { get; }
            IDateTimeProvider DateTimeProvider { get; }
        }

        public UserVerifier(IDependencies dependencies)
        {
            _dependencies = dependencies;
        }

        public OneTimePassword Generate(UserId userId)
        {
            var newPassword = _dependencies
                .PasswordGenerator
                .Generate(_dependencies.PasswordPolicy, userId);

            var validUntil = _dependencies
                .DateTimeProvider
                .Now
                .Add(_dependencies.PasswordPolicy.ValidityPeriod);

            var userPassword = new UserPassword
            {
                UserId = userId,
                Password = newPassword,
                ValidUntil = validUntil
            };

            _dependencies.UserPasswordRepository.Save(userPassword);

            return newPassword;
        }

        public bool IsValid(UserId userId, OneTimePassword password)
        {
            var savedPassword = _dependencies
                .UserPasswordRepository
                .Get(userId);

            if (savedPassword == null)
                return false;

            if (savedPassword.IsExpired(_dependencies.DateTimeProvider.Now))
                return false;

            return savedPassword.Password.Value == password.Value;
        }
    }
}