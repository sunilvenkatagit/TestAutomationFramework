using NUnit.Framework;
using AutomationFramework.Libraries;
using ResponsiveWeb.API.Configuration.TestConfig;
using ResponsiveWeb.API.Models.Response;
using ResponsiveWeb.API.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static ResponsiveWeb.API.Data.TestData;

namespace ResponsiveWeb.API.Tests
{
    [TestFixture]
    [Category("Getaway")]
    [Category("All")]
    public class TwitterTests : BaseTest
    {
        [Test]
        public void St_45973_RetrieveLatestTwitterTweet()
        {
            // Arrange
            var requestUrl = new GetawayProxy().TwitterTweetsFor();
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            { "origin",  OriginHeaderValue }
                                        };
            // Act
            var response = new ApiActionsLibrary().PerformGetRequest<List<TwitterTweetsModel>>(requestUrl, requestHeaders);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(200, (int)response.StatusCode, "Status code is not equal to 200");
                // Headers
                Assert.True(response.Headers.Any(ele => ele.Name == "Content-Encoding" && Regex.IsMatch(ele.Value.ToString(), @"^\w{0,4}$")), "Header: 'Content-Encoding' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "strict-transport-security" && Regex.IsMatch(ele.Value.ToString(), @"max-age=[\d]+$")), "Header: 'strict-transport-security' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "access-control-expose-headers" && ele.Value.ToString() == "*"), "Header: 'access-control-expose-headers' is missing or value doesn't macth. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Access-Control-Allow-Origin" && ele.Value.ToString() == "*"), "Header: 'Access-Control-Allow-Origin' is missing or value doesn't macth. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Cache-Control" && ele.Value.ToString() == "private, no-cache"), "Header: 'Cache-Control' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Content-Type" && ele.Value.ToString() == "application/json; charset=utf-8"), "Header: 'Content-Type' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "timing-allow-origin" && ele.Value.ToString().Length > 10), "Header: 'timing-allow-origin' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Date" && Regex.IsMatch(ele.Value.ToString(), @"\w{3},\s[0-3][0-9]\s\w{3,9}\s\d{4}\s[0-2][0-9]:[0-5][0-9]:[0-5][0-9]\sGMT")), "Header: 'Date' is missing or value doesn't match. Please check!");
                // Body
                Assert.IsNotEmpty(response.Content, "Received an EMPTY response. Please check!");
                Assert.IsNotEmpty(response.Data[0].FullText, "Tweet text is empty!");
                Assert.AreEqual("translink", response.Data[0].User.ScreenName.ToLower(), "ScreenName doesn't match!");
            });
        }

        [Test]
        public void St_45973_GetTweetsFromTranslinkTwitterAccount()
        {
            // Arrange
            var screenName = "translink";
            var count = 1;
            var requestUrl = new GetawayProxy().TwitterTweetsFor(screenName, count);
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            { "origin",  OriginHeaderValue }
                                        };
            // Act
            var response = new ApiActionsLibrary().PerformGetRequest<List<TwitterTweetsModel>>(requestUrl, requestHeaders);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(200, (int)response.StatusCode, "Status code is not equal to 200");
                // Headers
                Assert.True(response.Headers.Any(ele => ele.Name == "Content-Encoding" && Regex.IsMatch(ele.Value.ToString(), @"^\w{0,4}$")), "Header: 'Content-Encoding' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "strict-transport-security" && Regex.IsMatch(ele.Value.ToString(), @"max-age=[\d]+$")), "Header: 'strict-transport-security' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "access-control-expose-headers" && ele.Value.ToString() == "*"), "Header: 'access-control-expose-headers' is missing or value doesn't macth. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Access-Control-Allow-Origin" && ele.Value.ToString() == "*"), "Header: 'Access-Control-Allow-Origin' is missing or value doesn't macth. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Cache-Control" && ele.Value.ToString() == "private, no-cache"), "Header: 'Cache-Control' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Content-Type" && ele.Value.ToString() == "application/json; charset=utf-8"), "Header: 'Content-Type' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "timing-allow-origin" && ele.Value.ToString().Length > 10), "Header: 'timing-allow-origin' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Date" && Regex.IsMatch(ele.Value.ToString(), @"\w{3},\s[0-3][0-9]\s\w{3,9}\s\d{4}\s[0-2][0-9]:[0-5][0-9]:[0-5][0-9]\sGMT")), "Header: 'Date' is missing or value doesn't match. Please check!");
                // Body
                Assert.IsNotEmpty(response.Content, "Received an EMPTY response. Please check!");
                Assert.True(response.Data.Count == count, "Failed to retrieve the specified number of tweets. Please check!");
                Assert.True(response.Data.All(ele => !string.IsNullOrEmpty(ele.FullText)), "Tweet text is empty!");
                Assert.True(response.Data.All(ele => ele.User.ScreenName.ToLower() == screenName), "ScreenName doesn't match!");
            });
        }

        [Test]
        public void St_45973_GetTweetsFromTranslinkTwitterNewsAccount()
        {
            // Arrange
            var screenName = "translinknews";
            var count = 1;
            var requestUrl = new GetawayProxy().TwitterTweetsFor(screenName, count);
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            { "origin",  OriginHeaderValue }
                                        };
            // Act
            var response = new ApiActionsLibrary().PerformGetRequest<List<TwitterTweetsModel>>(requestUrl, requestHeaders);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(200, (int)response.StatusCode, "Status code is not equal to 200");
                // Headers
                Assert.True(response.Headers.Any(ele => ele.Name == "Content-Encoding" && Regex.IsMatch(ele.Value.ToString(), @"^\w{0,4}$")), "Header: 'Content-Encoding' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "strict-transport-security" && Regex.IsMatch(ele.Value.ToString(), @"max-age=[\d]+$")), "Header: 'strict-transport-security' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "access-control-expose-headers" && ele.Value.ToString() == "*"), "Header: 'access-control-expose-headers' is missing or value doesn't macth. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Access-Control-Allow-Origin" && ele.Value.ToString() == "*"), "Header: 'Access-Control-Allow-Origin' is missing or value doesn't macth. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Cache-Control" && ele.Value.ToString() == "private, no-cache"), "Header: 'Cache-Control' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Content-Type" && ele.Value.ToString() == "application/json; charset=utf-8"), "Header: 'Content-Type' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "timing-allow-origin" && ele.Value.ToString().Length > 10), "Header: 'timing-allow-origin' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Date" && Regex.IsMatch(ele.Value.ToString(), @"\w{3},\s[0-3][0-9]\s\w{3,9}\s\d{4}\s[0-2][0-9]:[0-5][0-9]:[0-5][0-9]\sGMT")), "Header: 'Date' is missing or value doesn't match. Please check!");
                // Body
                Assert.IsNotEmpty(response.Content, "Received an EMPTY response. Please check!");
                Assert.True(response.Data.Count == count, "Failed to retrieve the specified number of tweets. Please check!");
                Assert.True(response.Data.All(ele => !string.IsNullOrEmpty(ele.FullText)), "Tweet text is empty!");
                Assert.True(response.Data.All(ele => ele.User.ScreenName.ToLower() == screenName), "ScreenName doesn't match!");
            });
        }

        [Test]
        public void St_45973_500InternalErrorForNonExistantTwitterScreenName()
        {
            // Arrange
            var screenName = "ugIHiu1356587j";
            var count = 1;
            var requestUrl = new GetawayProxy().TwitterTweetsFor(screenName, count);
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            { "origin",  OriginHeaderValue }
                                        };
            // Act
            var response = new ApiActionsLibrary().PerformGetRequest(requestUrl, requestHeaders);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(500, (int)response.StatusCode, "Status code is NOT equal to 500. Please check!");
                // Headers
                Assert.True(response.Headers.Any(ele => ele.Name == "strict-transport-security" && Regex.IsMatch(ele.Value.ToString(), @"max-age=[\d]+$")), "Header: 'strict-transport-security' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "access-control-expose-headers" && ele.Value.ToString() == "*"), "Header: 'access-control-expose-headers' is missing or value doesn't macth. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Access-Control-Allow-Origin" && ele.Value.ToString() == "*"), "Header: 'Access-Control-Allow-Origin' is missing or value doesn't macth. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Cache-Control" && ele.Value.ToString() == "private, no-cache"), "Header: 'Cache-Control' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Content-Type" && ele.Value.ToString() == "application/json"), "Header: 'Content-Type' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "timing-allow-origin" && ele.Value.ToString().Length > 10), "Header: 'timing-allow-origin' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Date" && Regex.IsMatch(ele.Value.ToString(), @"\w{3},\s[0-3][0-9]\s\w{3,9}\s\d{4}\s[0-2][0-9]:[0-5][0-9]:[0-5][0-9]\sGMT")), "Header: 'Date' is missing or value doesn't match. Please check!");
                // Body
                Assert.IsNotEmpty(response.Content, "Received an EMPTY response. Please check!");
                Assert.True(response.Content.Contains("500") && response.Content.Contains("Application Error: The remote server returned an error: (404) Not Found."));
            });
        }
    }
}
