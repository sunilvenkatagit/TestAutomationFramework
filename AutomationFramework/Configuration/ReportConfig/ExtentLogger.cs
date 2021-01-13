using AutomationFramework.Libraries;
using AventStack.ExtentReports;


namespace AutomationFramework.Configuration.ReportConfig
{
    public class ExtentLogger
    {
        private ExtentLogger() { }

        public static void Pass(string message)
        {
            ExtentManager.GetExtentTest().Pass(message);
        }

        public static void Fail(string message)
        {
            ExtentManager.GetExtentTest().Fail(message);
        }

        public static void Skip(string message)
        {
            ExtentManager.GetExtentTest().Skip(message);
        }

        public static void Pass(string message, bool isScreenshotNeeded)
        {
            if (isScreenshotNeeded)
                ExtentManager.GetExtentTest().Pass(message, MediaEntityBuilder.CreateScreenCaptureFromBase64String(UtilityLibrary.Capture()).Build());
            else
                Pass(message);
        }

        public static void Fail(string message, bool isScreenshotNeeded)
        {
            if (isScreenshotNeeded)
                ExtentManager.GetExtentTest().Fail(message, MediaEntityBuilder.CreateScreenCaptureFromBase64String(UtilityLibrary.Capture()).Build());
            else
                Fail(message);
        }

        public static void Skip(string message, bool isScreenshotNeeded)
        {
            if (isScreenshotNeeded)
                ExtentManager.GetExtentTest().Skip(message, MediaEntityBuilder.CreateScreenCaptureFromBase64String(UtilityLibrary.Capture()).Build());
            else
                Skip(message);
        }
    }
}
