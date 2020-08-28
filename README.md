# Before running the app

Create an Azure AD app registration, allow it the User.Read premission for Microsoft Graph and create a client secret. 

Add the correct `tenantId` and `clientId` in `appSettings.json`, it also expects a `AzureAd:ClientSecret` as a user secret (or in the `appSettings.json` file).
