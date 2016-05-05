using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UserVerifier.Entities;
using UserVerifier.UnitTests.Builders;
using UserVerifier.ValueObjects;

namespace UserVerifier.UnitTests.UserVerifierTests
{
    [TestFixture]
    public class when_generating_password
    {
        private Func<OneTimePassword> _subject;

        private UserVerifier.IDependencies _dependencies;

        [SetUp]
        public void because()
        {
            var userId = new UserId(5);

            _dependencies = UserVerifierDependenciesBuilder.Do(b => b
                .with_current_datetime(new DateTime(2014, 5, 17, 15, 0, 0))
                .with_generator_result(userId, "xyz")
                .with_password_policy(3, TimeSpan.FromHours(4))
                );

            var verifier = new UserVerifier(_dependencies);

            _subject = () => verifier.Generate(userId);
        }

        [Test]
        public void it_should_generate_proper_password()
        {
            _subject().Value.Should().Be("xyz");
        }

        [Test]
        public void it_should_save_password_to_repository()
        {
            _subject();

            _dependencies
                .UserPasswordRepository
                .Received()
                .Save(Arg.Is<UserPassword>(up =>
                    up.UserId.Value == 5 &&
                    up.ValidUntil == new DateTime(2014, 5, 17, 19, 0, 0) &&
                    up.Password.Value == "xyz"
                    ));
        }
    }
}
