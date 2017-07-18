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
            var httpRequest = HttpContext.Current.Request;

            if (httpRequest.Files.Count <= 0) return Request.CreateResponse(HttpStatusCode.BadRequest);

            var postedFile = httpRequest.Files[0];
            var filePath = "";

            //Excel
            if (postedFile.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                filePath = HttpContext.Current.Server.MapPath("~/Files/BulkData/" + postedFile.FileName);
                postedFile.SaveAs(filePath);
                return Request.CreateResponse(HttpStatusCode.Created, postedFile.FileName);
            }

            // Image
            filePath = HttpContext.Current.Server.MapPath("~/Files/ProfilePics/" + id + ".jpeg");
            postedFile.SaveAs(filePath);
            return GetFile(id);
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
