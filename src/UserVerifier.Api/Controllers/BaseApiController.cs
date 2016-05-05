using System;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using UserVerifier.Main;

namespace UserVerifier.Api.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        private readonly UserVerifierFactory _factory = new UserVerifierFactory();

        protected UserVerifier BuildVerifier()
        {
            if (!TestCommandsEnabled)
                return _factory.Create();

            var currentDateTime = GetHeader("TestCommand.CurrentDateTime");

            return string.IsNullOrWhiteSpace(currentDateTime) 
                ? _factory.Create() 
                : _factory.Create(DateTime.Parse(currentDateTime));
        }

        private bool TestCommandsEnabled
        {
            get { return Convert.ToBoolean(ConfigurationManager.AppSettings["TestCommandsEnabled"] ?? "false"); }
        }

        private string GetHeader(string header)
        {
            return Request.Headers.Contains(header)
                ? Request.Headers.GetValues(header).FirstOrDefault()
                : null;
        }
    }
}