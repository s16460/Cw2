using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwiczenia2.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request != null)
            {
                context.Request.EnableBuffering();
                string path = context.Request.Path;
                string method = context.Request.Method;
                string queryString = context.Request.QueryString.ToString();
                string body = "";

                using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    body = await reader.ReadToEndAsync();
                    reader.Close();
                    context.Request.Body.Position = 0;
                }


                using (var writer = new StreamWriter("Log-" + DateTime.Now.ToString("MM-dd-yyyy") + ".txt", true))
                {
                    writer.WriteLine("Method= " + method);
                    writer.WriteLine("Path= " + path);
                    writer.WriteLine("Body= " + body);
                    writer.WriteLine("QueryString= " + queryString);
                    writer.Close();
                }
            }

            await _next(context);
        }

    }
}
