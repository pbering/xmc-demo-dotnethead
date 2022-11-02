# Minimal .NET 6 head for Sitecore XM Cloud / Experience Edge

...

## Getting started

1. On <https://deploy.sitecorecloud.io/> create a new project and an environment named `dev`.
1. Clone.
1. Then authenticate, connect, push and publish items:

    ```powershell
    dotnet tool restore
    dotnet sitecore cloud login
    dotnet sitecore cloud project list
    dotnet sitecore cloud environment list --project-id <INSERT PROJECT ID>
    dotnet sitecore cloud environment connect --environment-id <INSERT ENVIRONMENT ID> --allow-write
    dotnet sitecore ser push -n dev
    dotnet sitecore publish -n dev
    ```

1. To get the edge token, download <https://github.com/sitecorelabs/sxa-starter/blob/main/New-EdgeToken.ps1> into root and run it: `.\New-EdgeToken.ps1 -EnvironmentId <INSERT ENVIRONMENT ID>`

1. To get the jss editing secret, open "showconfig.aspx" and grab the value from the setting named `JavaScriptServices.ViewEngine.Http.JssEditingSecret`,

1. Add a new `appsettings.Development.json` file in the root:

   ```json
   {
     "Sitecore": {
       "ExperienceEdgeToken": "<INSERT YOUR EDGE TOKEN HERE>",
       "JssEditingSecret": "<INSERT JSS EDITING SECRET HERE>"
     }
   }
   ```

1. That's it, run and you should see a site!
