using System;
using System.Configuration;
using System.Net;
using FluentAssertions;
using RestSharp;
using TechTalk.SpecFlow;
using UserVerifier.AcceptanceTests.Helpers;
using UserVerifier.ValueObjects;

namespace UserVerifier.AcceptanceTests
{
    [Binding]
    public sealed class Steps
    {
        private int _userId;
        private OneTimePassword _password;
        private OneTimePassword _differentPassword;

        private IRestResponse<OneTimePassword> _generateResponse;
        private IRestResponse<bool> _validateResponse;
 
        private readonly TestClient _testClient;

        public Steps()
        {
            _testClient = new TestClient(ConfigurationManager.AppSettings["AppBaseUrl"]);
        }

        [Given(@"I want to generate password for a test user")]
        public void GivenIWantToGeneratePasswordForATestUser()
        {
            _userId = 5;
        }

        [Given(@"I generated a password for a user")]
        public void GivenIGeneratedAPasswordForAUser()
        {
            _userId = 7;
            _generateResponse = _testClient.Generate(_userId);
            _password = _generateResponse.Data;
        }

        [Given(@"I don't generate password for a user")]
        public void GivenIDonTGeneratePasswordForAUser()
        {
            _userId = 8;
        }

        [When(@"I call the API to generate password")]
        public void WhenICallTheAPIToGeneratePassword()
        {
            _generateResponse = _testClient.Generate(_userId);
            _password = _generateResponse.Data;
        }

        [When(@"I generated a password for a different user")]
        public void WhenIGeneratedAPasswordForADifferentUser()
        {
            _differentPassword = _testClient.Generate(100).Data;
        }

        [When(@"I call the API to validate this password")]
        public void WhenICallTheAPIToValidateThisPassword()
        {
            _validateResponse = _testClient.Validate(_userId, _password.Value);
        }

        [When(@"I call the API to validate different password")]
        public void WhenICallTheAPIToValidateDifferentPassword()
        {
            _validateResponse = _testClient.Validate(_userId, "different-password");
        }

        [When(@"I call the API to validate this password after expiration time")]
        public void WhenICallTheAPIToValidateThisPasswordAfterExpirationTime()
        {
            _validateResponse = _testClient.Validate(_userId, _password.Value, DateTime.UtcNow.AddSeconds(31));
        }

        [Then(@"the password should be generated")]
        public void ThenThePasswordShouldBeGenerated()
        {
            _generateResponse.Data.Value.Should().NotBeNullOrWhiteSpace();
        }

        [Then(@"the password should be validated succesfully")]
        public void ThenThePasswordShouldBeValidatedSuccesfully()
        {
            _validateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Then(@"the password should not be validated")]
        public void ThenThePasswordShouldNotBeValidated()
        {
            _validateResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Then(@"the password should be different")]
        public void ThenThePasswordShouldBeDifferent()
        {
            _password.Value.Should().NotBe(_differentPassword.Value);
        }
    }
}
