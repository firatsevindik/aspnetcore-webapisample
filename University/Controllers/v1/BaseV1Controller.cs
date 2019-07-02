using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;
using University.Models;

namespace University.Controllers.v1
{
    [Route("v1/[controller]")]
    [ApiController]
    public class BaseV1Controller : Controller
    {
        public static List<StudentModel> Students { get; set; }
        public string AppId { get; set; }
        public string AppKey { get; set; }

        public BaseV1Controller()
        {
            if(Students == null)
            {
                Students = new List<StudentModel>();
            }
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            const string AppIdField = "App-Id";
            const string AppKeyField = "App-Key";

            if(context.HttpContext.Request.Headers.ContainsKey(AppIdField) && context.HttpContext.Request.Headers.ContainsKey(AppKeyField))
            {
                AppId = context.HttpContext.Request.Headers[AppIdField];
                AppKey = context.HttpContext.Request.Headers[AppKeyField];
            }

            return base.OnActionExecutionAsync(context, next);
        }
    }
}
