public class DefaultController : Controller
{
    private readonly ILogger<DefaultController> _logger;

    public DefaultController(ILogger<DefaultController> logger)
    {
        _logger = logger;
    }

    [UseSitecoreRendering]
    public IActionResult Index(LayoutViewModel model)
    {
        var request = HttpContext.GetSitecoreRenderingContext();

        if (request.Response?.HasErrors ?? false)
        {
            foreach (var error in request.Response.Errors)
            {
                switch (error)
                {
                    case ItemNotFoundSitecoreLayoutServiceClientException notFound:

                        _logger.LogInformation(notFound, notFound.Message);

                        return NotFound();
                    default:
                        throw error;
                }
            }
        }

        return View(model);
    }
}
