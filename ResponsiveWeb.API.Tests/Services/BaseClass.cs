using AutomationFramework.Configuration.YamlConfig;

namespace ResponsiveWeb.API.Services
{
    public abstract class BaseClass
    {
        protected string URL;
        private readonly PropertyConfig _config = new PropertyConfig();

        protected string GetProxyUrl(string proxyName)
        {
            switch (_config.site.Environment)
            {
                case Constants.TestEnvironment.QA_ENVIRONMENT:
                    URL = $"https://{ proxyName }.qa.translink.ca/api";
                    break;
                case Constants.TestEnvironment.DEV_ENVIRONMENT:
                    URL = $"https://{ proxyName }.dev.translink.ca/api";
                    break;
                case Constants.TestEnvironment.PRODUCTION_ENVIRONMENT:
                    URL = $"https://{ proxyName }.translink.ca/api";
                    break;
            }

            return URL;
        }

        protected string GetAppService()
        {
            switch (_config.site.Environment)
            {
                case Constants.TestEnvironment.QA_ENVIRONMENT:
                    URL = $"https://tlweb-gtfsapi-qa.azurewebsites.net";
                    break;
                case Constants.TestEnvironment.DEV_ENVIRONMENT:
                    URL = $"https://tlweb-gtfsapi-dev.azurewebsites.net";
                    break;
                case Constants.TestEnvironment.PRODUCTION_ENVIRONMENT:
                    URL = $"https://tlweb-gtfsapi.azurewebsites.net";
                    break;
            }

            return URL;
        }
    }
}
