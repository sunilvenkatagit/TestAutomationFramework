using AutomationFramework.Configuration.DriverConfig;
using AutomationFramework.Configuration.YamlConfig;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace CompassCardTest
{
    public class DriverFactory
    {
        private DriverFactory() { }

        public static void InitializeDriver()
        {
            PropertyConfig myConfig = new PropertyConfig();

            switch (myConfig.settings.Browser)
            {
                case Constants.TestBrowsers.CHROME:
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("no-sandbox");
                    DriverManager.SetDriver(new ChromeDriver(chromeOptions));
                    break;
                case Constants.TestBrowsers.FIREFOX:
                    var firefoxOptions = new FirefoxOptions();
                    firefoxOptions.AddAdditionalCapability("acceptInsecureCerts", true, true);
                    firefoxOptions.AddArgument("no-sandbox");
                    DriverManager.SetDriver(new FirefoxDriver(firefoxOptions));
                    break;
            }

            DriverManager.GetDriver().Manage().Window.Maximize();
        }

        public static void QuitDriver()
        {
            if (DriverManager.GetDriver() != null)
            {
                DriverManager.GetDriver().Quit();
            }
        }
    }
}
