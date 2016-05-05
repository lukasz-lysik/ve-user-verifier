using System;
using System.Globalization;
using RestSharp;
using UserVerifier.ValueObjects;

namespace UserVerifier.AcceptanceTests.Helpers
{
    public class TestClient
    {
        private readonly RestClient _client;

        public TestClient(string baseUrl)
        {
            _client = new RestClient(baseUrl);
        }

        public IRestResponse<OneTimePassword> Generate(int userId)
        {
            var request = new RestRequest("api/generate/{id}", Method.POST);
            request.AddUrlSegment("id", userId.ToString(CultureInfo.InvariantCulture));

            return _client.Execute<OneTimePassword>(request);
        }

        public IRestResponse<bool> Validate(int userId, string password, DateTime? dateTimeOverride = null)
        {
            var request = new RestRequest("api/validate/{id}", Method.GET);
            request.AddUrlSegment("id", userId.ToString(CultureInfo.InvariantCulture));
            request.AddQueryParameter("password", password);

            if (dateTimeOverride != null)
            {
                request.AddHeader("TestCommand.CurrentDateTime", dateTimeOverride.Value.ToString(CultureInfo.InvariantCulture));
            }

            return _client.Execute<bool>(request);
        }
    }
}
