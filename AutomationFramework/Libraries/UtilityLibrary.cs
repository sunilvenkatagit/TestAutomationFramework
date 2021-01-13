using AutomationFramework.Configuration.DriverConfig;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.IO;
using static AutomationFramework.Libraries.EnumLibrary;

namespace AutomationFramework.Libraries
{
    public static class UtilityLibrary
    {
        public static string GetCurrentPath()
        {
            var path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            var actualPath = path.Substring(0, path.LastIndexOf("bin"));
            var projectPath = new Uri(actualPath).LocalPath;

            return projectPath;
        }

        public static string GetTimeStamp()
        {
            return DateTime.Now.ToString("yyyy-MM-dd h mm ss");
        }

        public static string Capture()
        {
            ITakesScreenshot ts = (ITakesScreenshot)DriverManager.GetDriver();
            Screenshot screenshot = ts.GetScreenshot();
            return screenshot.AsBase64EncodedString.ToString();
        }

        public static IWebElement ExplicitlyWaitFor(By element, WaitStrategy waitStrategy)
        {
            IWebElement webElement = null;
            switch (waitStrategy)
            {
                case WaitStrategy.CLICKABLE:
                    webElement = new WebDriverWait(DriverManager.GetDriver(), TimeSpan.FromSeconds(10))
                                    .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
                    HighlightElement(element);
                    break;
                case WaitStrategy.PRESENT:
                    webElement = new WebDriverWait(DriverManager.GetDriver(), TimeSpan.FromSeconds(10))
                                    .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(element));
                    break;
                case WaitStrategy.VISIBLE:
                    webElement = new WebDriverWait(DriverManager.GetDriver(), TimeSpan.FromSeconds(10))
                                    .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(element));
                    break;
                default:
                    break;
            }
            return webElement;
        }

        public static IWebElement GetWebElement(By element)
        {
            return DriverManager.GetDriver().FindElement(element);
        }
        public static void HighlightElement(By element)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)DriverManager.GetDriver();
            js.ExecuteScript("arguments[0].setAttribute('style', 'background: transparent; border: 3px solid green;');", GetWebElement(element));
        }

        public static void DeleteAllUnderResultsDir()
        {

            string dire = GetCurrentPath() + "Results";

            DirectoryInfo dir = new DirectoryInfo(dire);
            if (Directory.Exists(dire))
            {
                foreach (FileInfo file in dir.EnumerateFiles())
                    file.Delete();

                foreach (DirectoryInfo dir_ in dir.EnumerateDirectories())
                    dir_.Delete(true);
            }

            foreach (var process in Process.GetProcessesByName("chromedriver"))
                process.Kill();

            foreach (var process in Process.GetProcessesByName("geckodriver"))
                process.Kill();
        }
    }
}
