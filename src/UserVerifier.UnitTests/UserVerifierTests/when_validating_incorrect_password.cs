using System;
using FluentAssertions;
using NUnit.Framework;
using UserVerifier.UnitTests.Builders;
using UserVerifier.ValueObjects;

namespace UserVerifier.UnitTests.UserVerifierTests
{
    [TestFixture]
    public class when_validating_incorrect_password
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

            _subject = () => verifier.IsValid(userId, new OneTimePassword("abc"));
        }

        [Test]
        public void is_should_return_false()
        {
            _subject().Should().BeFalse();
        }
    }
}
