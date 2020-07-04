using BaseAPI.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace BaseAPI.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {

        private readonly ILogger<HttpGlobalExceptionFilter> logger;

        public HttpGlobalExceptionFilter(ILogger<HttpGlobalExceptionFilter> logger)
        {
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType() == typeof(ItemNotFoundException))
            {
                context.Result = new NotFoundResult();
            }
            else
            {
                logger.LogError(context.Exception, context.Exception.Message);
            }
        }
    }
}
