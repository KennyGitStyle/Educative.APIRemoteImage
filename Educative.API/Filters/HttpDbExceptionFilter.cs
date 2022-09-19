using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Educative.API.Filter
{

    public class HttpDbExceptionFilter : ExceptionFilterAttribute {
        


        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is not DbUpdateException)
            {
                var sqliteException = context.Exception?.InnerException?.InnerException as SqliteException;

                if (sqliteException?.ErrorCode == 2627)
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
                }

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }


    }

}