using OpenQA.Selenium;
using System.Threading;

namespace AutomationFramework.Configuration.DriverConfig
{
    public class DriverManager
    {
        private DriverManager() { }

        private static ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();

        public static IWebDriver GetDriver()
        {
            return driver.Value;
        }

        public static void SetDriver(IWebDriver broserRef)
        {
            driver.Value = broserRef;
        }
    }
}
