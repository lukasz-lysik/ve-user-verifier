using UserVerifier.Common;
using UserVerifier.Password.Generators;
using UserVerifier.Password.Policies;
using UserVerifier.Repositories;

namespace UserVerifier.Main
{
    public class UserVerifierDependencies : UserVerifier.IDependencies
    {
        public UserVerifierDependencies(
            IUserPasswordRepository userPasswordRepository,
            IPasswordGenerator passwordGenerator,
            IPasswordPolicy passwordPolicy,
            IDateTimeProvider dateTimeProvider)
        {
            UserPasswordRepository = userPasswordRepository;
            PasswordGenerator = passwordGenerator;
            PasswordPolicy = passwordPolicy;
            DateTimeProvider = dateTimeProvider;
        }

        public IUserPasswordRepository UserPasswordRepository { get; private set; }
        public IPasswordGenerator PasswordGenerator { get; private set; }
        public IPasswordPolicy PasswordPolicy { get; private set; }
        public IDateTimeProvider DateTimeProvider { get; private set; }
    }
}