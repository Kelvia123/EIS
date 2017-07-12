using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace EIS.API
{
    // Any request comes to the API will go through DeletgatingHandler
    public class APIKeyHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // If it's a preflight request than pass it through
            if (request.Headers.Contains("Access-Control-Request-Headers"))
            {
                return base.SendAsync(request, cancellationToken);
            }

            // If request contains Token then verify the Token
            if (request.Headers.Contains("my_Token"))
            {
                var apiKey = request.Headers.GetValues("my_Token").FirstOrDefault();

                if (apiKey == "123456789")
                {
                    return base.SendAsync(request, cancellationToken);
                }
            }

            // Reject Request
            var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            var taskObj = new TaskCompletionSource<HttpResponseMessage>();
            taskObj.SetResult(response);
            return taskObj.Task;
        }
    }
}