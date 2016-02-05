using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BrightBox.Models;
using BrightBox.Services;

namespace BrightBox.Controllers
{
    public class TechnicalWorksController : ApiController
    {
        private ITechnicalWorksRepository technicalWorksRepository = new TechnicalWorksRepository();
        private IAuthenticationRepository authenticationRepository = new AuthenticationRepository();

        public TechnicalWorks Get()
        {
            return this.technicalWorksRepository.GetTechnicalWorksStatus();
        }

        public HttpResponseMessage Post(TechnicalWorks technicalWorks)
        {
            HttpResponseMessage response;

            if (authenticationRepository.CheckToken(technicalWorks.Token))
            {
                this.technicalWorksRepository.SaveContact(technicalWorks);

                response = Request.CreateResponse<TechnicalWorks>(System.Net.HttpStatusCode.Accepted, technicalWorks);
            }
            else
            {
                response = Request.CreateResponse<TechnicalWorks>(System.Net.HttpStatusCode.OK, technicalWorks);
            }

            return response;
        }
    }
}
