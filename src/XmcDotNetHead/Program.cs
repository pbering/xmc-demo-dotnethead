global using System.Globalization;
global using Microsoft.AspNetCore.Localization;
global using Microsoft.AspNetCore.Mvc;
global using Sitecore.AspNet.RenderingEngine;
global using Sitecore.AspNet.RenderingEngine.Filters;
global using Sitecore.AspNet.RenderingEngine.Binding.Attributes;
global using Sitecore.AspNet.RenderingEngine.Extensions;
global using Sitecore.AspNet.RenderingEngine.Localization;
global using Sitecore.AspNet.ExperienceEditor;
global using Sitecore.LayoutService.Client.Exceptions;
global using Sitecore.LayoutService.Client.Response.Model.Fields;
global using Sitecore.LayoutService.Client.Extensions;

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
}).WithExperienceEditor(options => {
    options.JssEditingSecret = configuration.JssEditingSecret;
});

// configure app
var app = builder.Build();

if (configuration.EnableExperienceEditor)
{
    app.UseSitecoreExperienceEditor();
}

app.UseStaticFiles();
app.UseRouting();
app.UseRequestLocalization(options =>
{
    var defaultLanguage = "en";
    var supportedCultures = new[] { new CultureInfo(defaultLanguage) };

    options.DefaultRequestCulture = new RequestCulture(defaultLanguage, defaultLanguage);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.UseSitecoreRequestLocalization();
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapSitecoreLocalizedRoute("sitecore", "Index", "Default");
    endpoints.MapFallbackToController("Index", "Default");
});

// start app
app.Run();

class SitecoreSettings
{
    public static readonly string Key = "Sitecore";

    public Uri LayoutServiceUri { get; set; } = new Uri("https://edge.sitecorecloud.io/api/graphql/v1");

    public string DefaultSiteName { get; set; } = string.Empty;

    public string JssEditingSecret { get; set; } = string.Empty;

    public string ExperienceEdgeToken { get; set; } = string.Empty;

    public bool EnableExperienceEditor { get; set; } = false;
}
