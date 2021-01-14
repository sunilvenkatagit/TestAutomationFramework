using AutomationFramework.Configuration.ReportConfig;
using AutomationFramework.Libraries;
using NUnit.Framework;

namespace CompassCardTest
{
    [SetUpFixture]
    public class TestSetUp
    {
        [OneTimeSetUp]
        public void RunBeforeAllTest()
        {
            UtilityLibrary.DeleteAllUnderResultsDir();
            ExtentReport.InitializeReport();
        }

        [OneTimeTearDown]
        public void RunAfterAllTests()
        {
            ExtentReport.FlushReport();
        }
    }
}
