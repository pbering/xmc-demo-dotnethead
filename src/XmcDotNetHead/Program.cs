global using Microsoft.AspNetCore.Localization;
global using Microsoft.AspNetCore.Mvc;
global using Sitecore.AspNet.ExperienceEditor;
global using Sitecore.AspNet.RenderingEngine;
global using Sitecore.AspNet.RenderingEngine.Binding.Attributes;
global using Sitecore.AspNet.RenderingEngine.Extensions;
global using Sitecore.AspNet.RenderingEngine.Filters;
global using Sitecore.AspNet.RenderingEngine.Localization;
global using Sitecore.LayoutService.Client.Exceptions;
global using Sitecore.LayoutService.Client.Extensions;
global using Sitecore.LayoutService.Client.Response.Model.Fields;
global using System.Globalization;

// create app and load config
var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration.GetSection(SitecoreSettings.Key).Get<SitecoreSettings>();

// configure services
builder.Services.AddLocalization()
    .AddControllersWithViews();

builder.Services.AddSitecoreLayoutService()
    .AddGraphQlHandler("default", configuration.DefaultSiteName, configuration.ExperienceEdgeToken, configuration.LayoutServiceUri)
    .AsDefaultHandler();

builder.Services.AddSitecoreRenderingEngine(options =>
{
    options.AddModelBoundView<TitleModel>("Title")
        .AddModelBoundView<RichTextModel>("RichText")
        .AddDefaultPartialView("_ComponentNotFound");
})
.WithExperienceEditor(options =>
{
    options.JssEditingSecret = configuration.JssEditingSecret;
});

// configure app
var app = builder.Build();

if (configuration.EnableExperienceEditor)
{
    app.UseSitecoreExperienceEditor();
}

app.UseMiddleware<EnsureAcceptLanguageHeaderMiddleware>();
app.UseStaticFiles();
app.UseRequestLocalization(options =>
{
    var defaultLanguage = "en";
    var supportedCultures = new[] { new CultureInfo(defaultLanguage) };

    options.DefaultRequestCulture = new RequestCulture(defaultLanguage, defaultLanguage);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.UseSitecoreRequestLocalization();
});

app.MapControllerRoute("healthz", "healthz", new { controller = "Default", action = "Healthz" });
app.MapSitecoreLocalizedRoute("sitecore", "Index", "Default");
app.MapFallbackToController("Index", "Default");

// start app
app.Run();
