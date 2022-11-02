class SitecoreSettings
{
    public static readonly string Key = "Sitecore";

    public Uri LayoutServiceUri { get; set; } = new Uri("https://edge.sitecorecloud.io/api/graphql/v1");

    public string DefaultSiteName { get; set; } = string.Empty;

    public string JssEditingSecret { get; set; } = string.Empty;

    public string ExperienceEdgeToken { get; set; } = string.Empty;

    public bool EnableExperienceEditor { get; set; } = false;
}
