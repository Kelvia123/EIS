using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EIS.API.Controllers
{
    [EnableCors("*","*","*")]
    public class UploadController : ApiController
    {
        public HttpResponseMessage Post(string id)
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var docFiles = new List<string>();

                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/Files/ProfilePics/" + id + ".jpeg");
                    postedFile.SaveAs(filePath);

                    docFiles.Add(filePath);
                }

                result = Request.CreateResponse(HttpStatusCode.Created, docFiles);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return result;
        }
    }
}
