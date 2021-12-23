using AutomationFramework.Configuration.YamlConfig;

namespace ResponsiveWeb.API.Data
{
    public static class TestData
    {
        private static readonly PropertyConfig config = new PropertyConfig();

        public static string ProfileId
        {
            get
            {
                string profileId = null;

                switch (config.site.Environment)
                {
                    case Constants.TestEnvironment.QA_ENVIRONMENT:
                        profileId = "00ab4fd1-1c2c-4413-9e79-e8e118466287";
                        break;
                    case Constants.TestEnvironment.DEV_ENVIRONMENT:
                        profileId = "49da7db8-629e-4ad9-882a-345510b8632e";
                        break;
                    case Constants.TestEnvironment.PRODUCTION_ENVIRONMENT:
                        profileId = "16212413-3fbe-4eb5-afb6-bdbcdbf768cb";
                        break;
                }

                return profileId;
            }
        }

        public static string RefererHeaderValue
        {
            get
            {
                string refererHeaderValue = null;

                switch (config.site.Environment)
                {
                    case Constants.TestEnvironment.QA_ENVIRONMENT:
                        refererHeaderValue = "https://qa.translink.ca/";
                        break;
                    case Constants.TestEnvironment.DEV_ENVIRONMENT:
                        refererHeaderValue = "https://dev.translink.ca/";
                        break;
                    case Constants.TestEnvironment.PRODUCTION_ENVIRONMENT:
                        refererHeaderValue = "https://www.translink.ca/";
                        break;
                }

                return refererHeaderValue;
            }
        }

        public static string OriginHeaderValue
        {
            get
            {
                string originHeaderValue = null;

                switch (config.site.Environment)
                {
                    case Constants.TestEnvironment.QA_ENVIRONMENT:
                        originHeaderValue = "https://qa.translink.ca";
                        break;
                    case Constants.TestEnvironment.DEV_ENVIRONMENT:
                        originHeaderValue = "https://dev.translink.ca";
                        break;
                    case Constants.TestEnvironment.PRODUCTION_ENVIRONMENT:
                        originHeaderValue = "https://www.translink.ca";
                        break;
                }

                return originHeaderValue;
            }
        }

        public static string SiteNavCacheApiKey
        {
            get
            {
                string iteNavCacheApiKey = null;

                switch (config.site.Environment)
                {
                    case Constants.TestEnvironment.QA_ENVIRONMENT:
                        iteNavCacheApiKey = "hCXwuDVJLQHo8RpQit6GZudYPeyKqcPKwFEuHMf51c9uPETAeVVRkQ==";
                        break;
                    case Constants.TestEnvironment.DEV_ENVIRONMENT:
                        iteNavCacheApiKey = "XhlR0i41RoVesRWb4WDYULQIYmtQxlxot6cRav7mDkIeVZjX/xOkkg==";
                        break;
                    case Constants.TestEnvironment.PRODUCTION_ENVIRONMENT:
                        iteNavCacheApiKey = "2WNHqWex49pKU0ZaN7um9grjOuKXQVJaOy8efDAOL6anyZOAhvZU9g==";
                        break;
                }

                return iteNavCacheApiKey;
            }
        }
    }
}
