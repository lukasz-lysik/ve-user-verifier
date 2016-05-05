using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using UserVerifier.Common;
using UserVerifier.Entities;
using UserVerifier.Password.Generators;
using UserVerifier.Password.Policies;
using UserVerifier.Repositories;
using UserVerifier.ValueObjects;

namespace UserVerifier.UnitTests.Builders
{
    public class UserVerifierDependenciesBuilder
    {
        private DateTime _now;
        private readonly List<UserPassword> _savedPasswords = new List<UserPassword>();
        private readonly IDictionary<UserId, OneTimePassword> _generatorResults = new Dictionary<UserId, OneTimePassword>();
        private IPasswordPolicy _passwordPolicy;

        public static UserVerifier.IDependencies Do(Func<UserVerifierDependenciesBuilder, UserVerifierDependenciesBuilder> func)
        {
            return func(new UserVerifierDependenciesBuilder()).Build();
        }

        private UserVerifier.IDependencies Build()
        {
            var dependencies = Substitute.For<UserVerifier.IDependencies>();

            var repository = Substitute.For<IUserPasswordRepository>();
            _savedPasswords
                .ForEach(p => repository.Get(p.UserId).Returns(p));
            dependencies.UserPasswordRepository.Returns(repository);

            var passwordGenerator = Substitute.For<IPasswordGenerator>();
            _generatorResults
                .ToList()
                .ForEach(p => passwordGenerator
                    .Generate(Arg.Any<IPasswordPolicy>(), p.Key)
                    .Returns(p.Value));
            dependencies.PasswordGenerator.Returns(passwordGenerator);

            dependencies.PasswordPolicy.Returns(_passwordPolicy);

            var dateTimeProvider = Substitute.For<IDateTimeProvider>();
            dateTimeProvider.Now.Returns(_now);
            dependencies.DateTimeProvider.Returns(dateTimeProvider);

            return dependencies;
        }

        public UserVerifierDependenciesBuilder with_current_datetime(DateTime now)
        {
            _now = now;
            return this;
        }

        public UserVerifierDependenciesBuilder with_saved_password(UserId userId, string password, DateTime validUntil)
        {
            _savedPasswords.Add(new UserPassword
            {
                UserId = userId,
                Password = new OneTimePassword(password),
                ValidUntil = validUntil
            });

            return this;
        }

        public UserVerifierDependenciesBuilder with_generator_result(UserId userId, string password)
        {
            _generatorResults[userId] = new OneTimePassword(password);
            return this;
        }

        public UserVerifierDependenciesBuilder with_password_policy(int length, TimeSpan validityPeriod)
        {
            _passwordPolicy = Substitute.For<IPasswordPolicy>();
            _passwordPolicy.Length.Returns(length);
            _passwordPolicy.ValidityPeriod.Returns(validityPeriod);
            return this;
        }
    }
}
