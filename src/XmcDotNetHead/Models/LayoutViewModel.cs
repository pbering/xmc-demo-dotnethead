public class LayoutViewModel
{
    [SitecoreRouteField]
    public TextField Title { get; set; } = new TextField();

    [SitecoreRouteField]
    public TextField NavigationTitle { get; set; } = new TextField();
}
