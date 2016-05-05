using System.Net;
using System.Net.Http;
using UserVerifier.ValueObjects;

namespace UserVerifier.Api.Controllers
{
    public class GenerateController : BaseApiController
    {
        public HttpResponseMessage Post(int id)
        {
            var password = BuildVerifier().Generate(new UserId(id));
            return Request.CreateResponse(HttpStatusCode.OK, password);
        }
    }
}