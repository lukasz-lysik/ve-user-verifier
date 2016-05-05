using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserVerifier.ValueObjects;

namespace UserVerifier.Api.Controllers
{
    public class ValidateController : BaseApiController
    {
        public HttpResponseMessage Get(int id, [FromUri] string password)
        {
            var isValid = BuildVerifier().IsValid(new UserId(id), new OneTimePassword(password));

            return Request.CreateResponse(
                isValid ? HttpStatusCode.OK : HttpStatusCode.Unauthorized,
                isValid
                );
        }
    }
}