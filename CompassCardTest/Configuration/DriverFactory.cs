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
            Config myConfig = new Config();

            switch (myConfig.settings.Browser)
            {
                case Constants.TestBrowsers.CHROME:
                    DriverManager.SetDriver(new ChromeDriver());
                    break;
                case Constants.TestBrowsers.FIREFOX:
                    var firefoxOptions = new FirefoxOptions();
                    firefoxOptions.AddAdditionalCapability("acceptInsecureCerts", true, true);
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
