using AutomationFramework.Configuration.ReportConfig;
using NUnit.Framework;

[assembly: LevelOfParallelism(3)]
[assembly: Parallelizable(ParallelScope.All)]

namespace CompassCardTest
{
    public class BaseTest
    {

        [SetUp]
        public void RunBeforeEachTest()
        {
            DriverFactory.InitializeDriver();
            ExtentReport.StartTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void RunAfterEachTest()
        {
            DriverFactory.QuitDriver();
            ExtentReport.EndTest(TestContext.CurrentContext.Test.Name);
        }

    }
}
