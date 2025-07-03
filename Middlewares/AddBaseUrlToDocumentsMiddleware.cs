using BiblioPfe.Infrastructure.Entities;
using HotChocolate.Resolvers;
using Microsoft.Extensions.Options;

namespace dentalevo_backend.Middlewares
{
    public class AddBaseUrlToDocumentsMiddleware
    {
        private readonly FieldDelegate _next;

        public AddBaseUrlToDocumentsMiddleware(FieldDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(IMiddlewareContext context)
        {
            await _next(context);

            if (context.Result != null && context.Result is Document document)
            {
                // Get the HttpContext from the context
                var httpContextAccessor =
                    context.RequestServices.GetRequiredService<IHttpContextAccessor>();
                var httpContext = httpContextAccessor.HttpContext;

                if (httpContext != null)
                {
                    var scheme = httpContext.Request.Scheme;
                    var host = httpContext.Request.Host.Value;

                    var appUrl = $"https://{host}";
                    if (!string.IsNullOrEmpty(document.Url) && !document.Url.Contains(appUrl))
                    {
                        document.Url = $"{appUrl}{document.Url}";
                    }
                }
            }
        }
    }
}
