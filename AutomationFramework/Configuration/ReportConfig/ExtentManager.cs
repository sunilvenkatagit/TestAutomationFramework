using AventStack.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
