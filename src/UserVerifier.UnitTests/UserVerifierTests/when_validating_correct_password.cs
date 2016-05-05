using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UserVerifier.Common;
using UserVerifier.Entities;
using UserVerifier.Password.Generators;
using UserVerifier.Password.Policies;
using UserVerifier.Repositories;
using UserVerifier.UnitTests.Builders;
using UserVerifier.ValueObjects;

namespace UserVerifier.UnitTests.UserVerifierTests
{
    [TestFixture]
    public class when_validating_correct_password
    {
        private Func<bool> _subject;

        [SetUp]
        public void because()
        {
            var userId = new UserId(5);

            var dependencies = UserVerifierDependenciesBuilder.Do(b => b
                .with_current_datetime(new DateTime(2014, 5, 17, 15, 0, 0))
                .with_saved_password(userId, "xyz", new DateTime(2014, 5, 18, 17, 0, 0))
                );

            var verifier = new UserVerifier(dependencies);

            _subject = () => verifier.IsValid(userId, new OneTimePassword("xyz"));
        }

        [Test]
        public void is_should_return_true()
        {
            _subject().Should().BeTrue();
        }
    }
}
