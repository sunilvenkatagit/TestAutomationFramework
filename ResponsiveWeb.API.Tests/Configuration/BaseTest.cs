using AutomationFramework.Configuration.ReportConfig;
using AventStack.ExtentReports;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Linq;

[assembly: Parallelizable(ParallelScope.All)]
[assembly: LevelOfParallelism(3)]

namespace ResponsiveWeb.API.Configuration.TestConfig
{
    public abstract class BaseTest
    {
        [OneTimeSetUp]
        public void RunBeforeAllTests()
        {
            ExtentReport.InitializeReport();
        }

        [SetUp]
        public void BeforeEachTestMethod()
        {
            ExtentReport.StartTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void AfterEachTestMethod()
        {
            ExtentReport.EndTest(TestContext.CurrentContext.Test.Name);
        }

        [OneTimeTearDown]
        public void RunAfterAllTests()
        {
            ExtentReport.FlushReport();
        }

        public void WriteResposeToReport(IRestResponse response)
        {
            ExtentManager.GetExtentTest().Log(Status.Info, $"<b>Reponse Details</b>");

            ExtentManager.GetExtentTest().Log(Status.Info, $"<pre><b>Status Code:</b> { (int)response.StatusCode }<br />" +
                                                              $"<b>Status Description:</b> { response.StatusDescription }<br />" +
                                                              $"<b>Response Headers:</b><br />  { string.Join("<br />  ", response.Headers.Select(ele => "<b>. " + ele.Name + "</b>" + " = " + ele.Value)) }<br />" +
                                                              $"<b>Response Body:</b><br />{ ConvertStringResponseToJsonPrint(response.Content) }</pre>");
        }
        private string ConvertStringResponseToJsonPrint(string content)
        {
            // Print json reponse in pretty format
            try
            {
                if (!string.IsNullOrEmpty(content))
                {
                    dynamic parsedJson = JsonConvert.DeserializeObject(content);
                    string prettyPint = JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
                    return prettyPint;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return $"There is no valid response body.";
            }

            return content;
        }
    }
}
