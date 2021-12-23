using AutomationFramework.Configuration.DriverConfig;
using AutomationFramework.Configuration.ReportConfig;
using AutomationFramework.Configuration.YamlConfig;
using AutomationFramework.Libraries;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace CompassCardTest
{
    public class BasePage
    {
        readonly PropertyConfig myConfig = new PropertyConfig();
        protected bool isMobileDevice;
        protected WebDriverWait wait;
        protected IWebDriver driver;
        protected ExtentTest test;
        protected string URL;

        public BasePage()
        {
            URL = GetUrl();
            isMobileDevice = GetDeviceType();
            driver = DriverManager.GetDriver();
            test = ExtentManager.GetExtentTest();
            wait = new WebDriverWait(DriverManager.GetDriver(), TimeSpan.FromSeconds(10));

        }
        private string GetUrl()
        {
            switch (myConfig.site.Environment)
            {
                case Constants.TestEnvironment.QA_ENVIRONMENT:
                    URL = "https://compasscard.ca";
                    break;
                case Constants.TestEnvironment.DEV_ENVIRONMENT:
                    URL = "https://compasscard.ca";
                    break;
                case Constants.TestEnvironment.STAGING_ENVIRONMENT:
                    URL = "https://compasscard.ca";
                    break;
                case Constants.TestEnvironment.PRODUCTION_ENVIRONMENT:
                    URL = "https://compasscard.ca";
                    break;
                default:
                    URL = "https://compasscard.ca";
                    break;
            }

            return URL;
        }
        private bool GetDeviceType()
        {
            if (!myConfig.settings.DeviceType.ToLower().Equals("desktop") &&
                !myConfig.settings.DeviceType.ToLower().Equals("tablet") &&
                    myConfig.settings.DeviceType.ToLower().Equals("phone") &&
                    myConfig.settings.Browser.ToLower().Equals("chrome"))
            {
                isMobileDevice = true;
            }
            return isMobileDevice;
        }
    }
}
