# Minimal .NET 6 head for Sitecore XM Cloud / Experience Edge

... TODO link to blog post...

## Getting started

1. Add a new `appsettings.Development.json` file in the root and fill out the blanks:

    ```json
    {
        "Sitecore": {
            "DefaultSiteName": "<INSERT YOUR SITE NAME HERE>",
            "ExperienceEdgeToken": "<INSERT YOUR EDGE TOKEN HERE>"
        }
    }
    ```
1. TODO: add serialized itmes? remeber dotnet tool restore + dotnet sitecore login and ser push
1. Done, ready to go!
