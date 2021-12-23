using AventStack.ExtentReports;
using System.Threading;

namespace AutomationFramework.Configuration.ReportConfig
{
    public class ExtentManager
    {
        private ExtentManager() { }

        private static ThreadLocal<ExtentTest> ExtTest = new ThreadLocal<ExtentTest>();

        public static ExtentTest GetExtentTest()
        {
            return ExtTest.Value;
        }

        public static void SetExtentTest(ExtentTest test)
        {
            ExtTest.Value = test;
        }
    }
}
