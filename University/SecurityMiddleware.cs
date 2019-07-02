using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University
{
    public class SecurityMiddleware
    {
        private readonly RequestDelegate _next;
        private string AppId = "F30B36AF-AFC2-4E7B-8F0E-0074DAE2BD37";
        private string AppKey = "F1EE01C2-1C7C-4E7D-8386-141B1695E4F2";


        public SecurityMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            const string AppIdField = "App-Id";
            const string AppKeyField = "App-Key";

            if (context.Request.Headers.ContainsKey(AppIdField) && context.Request.Headers.ContainsKey(AppKeyField))
            {
                var appId = context.Request.Headers[AppIdField];
                var appKey = context.Request.Headers[AppKeyField];

                if(appId == AppId && appKey == AppKey)
                {
                    await _next(context);

                    return;
                }
            }

            var message = new { Message = "Unauthorized" };
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(message));

            return;
        }
    }
}
