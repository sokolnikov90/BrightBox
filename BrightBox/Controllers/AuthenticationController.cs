using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace BrightBox.Controllers
{
    using BrightBox.Models;
    using BrightBox.Services;

    public class AuthenticationController : ApiController
    {
        IAuthenticationRepository authenticationRepository = new AuthenticationRepository();

        public HttpResponseMessage Post(User user)
        {
            HttpResponseMessage response;

            if (authenticationRepository.Authentication(user))
            {
                string token = authenticationRepository.CreateToken();

                response = Request.CreateResponse<string>(System.Net.HttpStatusCode.Accepted, token);
            }
            else
            {
                response = Request.CreateResponse<string>(System.Net.HttpStatusCode.Unauthorized, null);                
            }

            return response;
        }
    }
}
