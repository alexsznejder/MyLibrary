using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLibrary.App_Start
{
    public class MyHttpHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Pliki *.gif nie są obsługiwane w systemie");
        }
    }
}