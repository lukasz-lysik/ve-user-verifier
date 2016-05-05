using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UserVerifier.Common;
using UserVerifier.Password.Generators;
using UserVerifier.Password.Policies;
using UserVerifier.ValueObjects;

namespace UserVerifier.UnitTests.Password.SimplePasswordGeneratorTests
{
    [TestFixture]
    public class when_password_length_is_6_and_random_generator_returns_0_always
    {
        private Func<OneTimePassword> _subject;
            
        [SetUp]
        public void because()
        {
            var policy = Substitute.For<IPasswordPolicy>();
            policy.Length.Returns(6);

            var randomGenerator = Substitute.For<IRandomNumberGenerator>();
            randomGenerator.Next(Arg.Any<int>()).Returns(0);

            var generator = new SimplePasswordGenerator(randomGenerator);

            _subject = () => generator.Generate(policy, new UserId(1));
        }

        [Test]
        public void it_should_have_correct_length()
        {
            _subject().Value.Length.Should().Be(6);
        }

        [Test]
        public void it_should_have_correct_value()
        {
            _subject().Value.Should().Be("AAAAAA");
        }
    }
}
