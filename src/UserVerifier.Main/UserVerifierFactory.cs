using System;
using UserVerifier.Common;
using UserVerifier.Dal;
using UserVerifier.Password.Generators;
using UserVerifier.Password.Policies;

namespace UserVerifier.Main
{
    // Poor man's dependency injection
    public class UserVerifierFactory
    {
        public UserVerifier Create()
        {
            var dependencies = new UserVerifierDependencies
                (
                    new InMemoryUserPasswordRepository(),
                    new SimplePasswordGenerator(new RandomNumberGenerator()),
                    new NormalPasswordPolicy(),
                    new DateTimeProvider()
                );

            return new UserVerifier(dependencies);
        }

        public UserVerifier Create(DateTime currentDateTime)
        {
            var dependencies = new UserVerifierDependencies
                (
                new InMemoryUserPasswordRepository(),
                new SimplePasswordGenerator(new RandomNumberGenerator()),
                new NormalPasswordPolicy(),
                new FixedDateTimeProvider(currentDateTime)
                );

            return new UserVerifier(dependencies);
        }
    }
}
