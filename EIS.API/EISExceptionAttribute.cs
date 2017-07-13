using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace EIS.API
{
    // We can add this as a global exception filter 
    // so that we don't have to add try-catch block to each and every method
    public class EISExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            // Log Exception
            var filePath = HttpContext.Current.Server.MapPath("~/Files/log.txt");
            var txt = DateTime.Now.ToString() + " : " + actionExecutedContext.Exception.Message + "\n";
            File.AppendAllText(filePath, txt);

            //Customize error message based on different exceptions
            if (actionExecutedContext.Exception is SqlException)
            {
                actionExecutedContext.ActionContext.ModelState.AddModelError("", "Sql Server service is not available");
                actionExecutedContext.Response =
                    actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.BadGateway,
                        actionExecutedContext.ActionContext.ModelState);
            }
            else
            {
                actionExecutedContext.Response =
                    actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                        actionExecutedContext.Exception);
            }
            
            //base.OnException(actionExecutedContext);           
        }
    }
}