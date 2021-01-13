using AutomationFramework.Libraries;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;

namespace AutomationFramework.Configuration.ReportConfig
{
    public class ExtentReport
    {
        private ExtentReport() { }

        private static ExtentReports extentReports;

        static readonly string fileName = "index.html";
        static readonly string path = UtilityLibrary.GetCurrentPath() + @"Results\Results_" + DateTime.Now.ToString("yyyy-MM-dd") + @"\" + fileName;

        public static void InitializeReport()
        {
            if (extentReports == null)
            {

                var htmlReporter = new ExtentHtmlReporter(path);
                htmlReporter.Config.Theme = Theme.Standard;
                htmlReporter.Config.DocumentTitle = "AutomationTesting_Report";
                htmlReporter.Config.ReportName = "CompassCard";
                extentReports = new ExtentReports();
                extentReports.AttachReporter(htmlReporter);
            }
        }

        public static void FlushReport()
        {
            if (extentReports != null)
            {
                extentReports.Flush();
            }
            System.Diagnostics.Process.Start(path);
        }

        public static void StartTest(string testCaseName)
        {
            ExtentManager.SetExtentTest(extentReports.CreateTest(testCaseName));
        }

        public static void EndTest(string testCaseName)
        {
            switch (TestContext.CurrentContext.Result.Outcome.Status)
            {
                case TestStatus.Passed:
                    //ExtentManager.GetExtentTest().Log(Status.Pass, $"{ testCaseName } test case is passed.");
                    ExtentLogger.Pass($"{ testCaseName } test case is passed.");
                    break;
                case TestStatus.Failed:
                    //ExtentManager.GetExtentTest().Log(Status.Info, $"FAILURE REASON: { TestContext.CurrentContext.Result.Message }");
                    ExtentManager.GetExtentTest().Log(Status.Fail, $"{ testCaseName } test case is failed.");
                    break;
                case TestStatus.Skipped:
                    ExtentManager.GetExtentTest().Log(Status.Skip, $"{ testCaseName } test case is skipped.");
                    break;
                default:
                    break;
            }
        }
    }
}
