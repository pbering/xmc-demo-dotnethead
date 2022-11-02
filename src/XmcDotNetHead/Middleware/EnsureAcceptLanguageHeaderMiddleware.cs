public class EnsureAcceptLanguageHeaderMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _defaultLang;

    public EnsureAcceptLanguageHeaderMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _defaultLang = configuration.GetValue<string>("DefaultAcceptLanguageHeader");
    }

    public async Task Invoke(HttpContext httpContext)
    {
        if (!httpContext.Request.Headers.ContainsKey("Accept-Language"))
        {
            httpContext.Request.Headers.Add("Accept-Language", _defaultLang);
        }

        await _next(httpContext).ConfigureAwait(false);
    }
}
