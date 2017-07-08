using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EIS.API.Controllers
{
    [EnableCors("*","*","*")]
    public class UploadController : ApiController
    {
        public HttpResponseMessage Get(string Id)
        {
            return GetFile(Id);
        }
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

                //result = Request.CreateResponse(HttpStatusCode.Created, docFiles);
                return GetFile(id);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return result;
        }

        private static HttpResponseMessage GetFile(string id)
        {
            var filePath = HttpContext.Current.Server.MapPath("~/Files/ProfilePics/");
            Byte[] binaryPic;
            if (File.Exists(filePath + id + ".jpeg"))
            {
                binaryPic = File.ReadAllBytes(filePath + id + ".jpeg");
            }
            else
            {
                binaryPic = File.ReadAllBytes((filePath + "anonymous.png"));
            }

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            //Client Side can read Base64String and convert it to image
            response.Content = new StringContent(Convert.ToBase64String(binaryPic));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            return response;
        }
    }
}
