using NUnit.Framework;
using AutomationFramework.Libraries;
using ResponsiveWeb.API.Configuration.TestConfig;
using ResponsiveWeb.API.Models.Response;
using ResponsiveWeb.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static ResponsiveWeb.API.Data.TestData;

namespace ResponsiveWeb.API.Tests
{
    [TestFixture]
    [Category("Postaway")]
    [Category("All")]
    public class RSP_ProfileTests : BaseTest
    {
        [Test]
        public void St_45585_GetUserProfile()
        {
            // Arrange
            var requestUrl = new PostawayProxy().UserProfile(ProfileId);
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            {"referer", RefererHeaderValue},
                                            {"origin",  OriginHeaderValue}
                                        };

            // Act
            var response = new ApiActionsLibrary().PerformGetRequest<UserProfileModel>(requestUrl, requestHeaders);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
                    {
                        // Status
                        Assert.AreEqual(200, (int)response.StatusCode, "Status code is NOT 200. Please check!");
                        // Headers
                        Assert.True(response.Headers.Any(ele => ele.Name == "Content-Encoding" && Regex.IsMatch(ele.Value.ToString(), @"^\w{0,4}$")), "Header: 'Content-Encoding' is missing or value doesn't match. Please check!");
                        Assert.True(response.Headers.Any(ele => ele.Name == "strict-transport-security" && Regex.IsMatch(ele.Value.ToString(), @"max-age=[\d]+$")), "Header: 'strict-transport-security' is missing or value doesn't match. Please check!");
                        Assert.True(response.Headers.Any(ele => ele.Name == "access-control-expose-headers" && ele.Value.ToString() == "*"), "Header: 'access-control-expose-headers' is missing or value doesn't macth. Please check!");
                        Assert.True(response.Headers.Any(ele => ele.Name == "Access-Control-Allow-Origin" && ele.Value.ToString() == "*"), "Header: 'Access-Control-Allow-Origin' is missing or value doesn't macth. Please check!");
                        Assert.True(response.Headers.Any(ele => ele.Name == "server-timing" && Regex.IsMatch(ele.Value.ToString(), @"rsp;dur=[\d]{1,4}$")), "Header: 'server-timing' is missing or time exceeds 10 seconds. Please check!");
                        Assert.True(response.Headers.Any(ele => ele.Name == "Cache-Control" && ele.Value.ToString() == "private, no-cache"), "Header: 'Cache-Control' is missing or value doesn't match. Please check!");
                        Assert.True(response.Headers.Any(ele => ele.Name == "timing-allow-origin" && ele.Value.ToString().Length > 10), "Header: 'timing-allow-origin' is missing or value doesn't match. Please check!");
                        Assert.True(response.Headers.Any(ele => ele.Name == "Content-Type" && ele.Value.ToString() == "application/json; charset=utf-8"), "Header: 'Content-Type' is missing or value doesn't match. Please check!");
                        Assert.True(response.Headers.Any(ele => ele.Name == "Date" && Regex.IsMatch(ele.Value.ToString(), @"\w{3},\s[0-3][0-9]\s\w{3,9}\s\d{4}\s[0-2][0-9]:[0-5][0-9]:[0-5][0-9]\sGMT")), "Header: 'Date' is missing or value doesn't match. Please check!");
                        // Body
                        Assert.IsNotEmpty(response.Data.DisplayName, "DisplayName is EMPTY. Please check!");
                        Assert.True(Regex.IsMatch(response.Data.Email, @"^[a-zA-Z0-9.!#$%&’'*+=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$"));
                    });
        }

        [Test]
        public void St_45585_404UserProfileNotFound()
        {
            // Arrange
            // Valid profile id format but no user exists with this id
            var requestUrl = new PostawayProxy().UserProfile("16378413-3fbe-4eb5-afb6-bdbcdbf768cb");
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            {"referer", RefererHeaderValue},
                                            {"origin",  OriginHeaderValue}
                                        };

            // Act
            var response = new ApiActionsLibrary().PerformGetRequest<UserProfileModel>(requestUrl, requestHeaders);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
                    {
                        // Status
                        Assert.AreEqual(404, (int)response.StatusCode, "Status code is NOT 404. Please check!");
                        // Headers
                        Assert.True(response.Headers.Any(ele => ele.Name == "strict-transport-security" && Regex.IsMatch(ele.Value.ToString(), @"max-age=[\d]+$")), "Header: 'strict-transport-security' is missing or value doesn't match. Please check!");
                        Assert.True(response.Headers.Any(ele => ele.Name == "access-control-expose-headers" && ele.Value.ToString() == "*"), "Header: 'access-control-expose-headers' is missing or value doesn't macth. Please check!");
                        Assert.True(response.Headers.Any(ele => ele.Name == "Access-Control-Allow-Origin" && ele.Value.ToString() == "*"), "Header: 'Access-Control-Allow-Origin' is missing or value doesn't macth. Please check!");
                        Assert.True(response.Headers.Any(ele => ele.Name == "server-timing" && Regex.IsMatch(ele.Value.ToString(), @"rsp;dur=[\d]{1,4}$")), "Header: 'server-timing' is missing or time exceeds 10 seconds. Please check!");
                        Assert.True(response.Headers.Any(ele => ele.Name == "timing-allow-origin" && ele.Value.ToString().Length > 10), "Header: 'timing-allow-origin' is missing or value doesn't match. Please check!");
                        Assert.True(response.Headers.Any(ele => ele.Name == "Cache-Control" && ele.Value.ToString() == "private, no-cache"), "Header: 'Cache-Control' is missing or value doesn't match. Please check!");
                        Assert.True(response.Headers.Any(ele => ele.Name == "Date" && Regex.IsMatch(ele.Value.ToString(), @"\w{3},\s[0-3][0-9]\s\w{3,9}\s\d{4}\s[0-2][0-9]:[0-5][0-9]:[0-5][0-9]\sGMT")), "Header: 'Date' is missing or value doesn't match. Please check!");
                        // Body
                        Assert.IsEmpty(response.Content, "Response body is NOT Empty. Please check!");
                    });
        }

        [Test]
        public void St_45585_400BadRequestForInvalidProfileId()
        {
            // Arrange
            // Invalid profile id (more than 36)
            var requestUrl = new PostawayProxy().UserProfile("16378413-3fbe-4eb5-afb6-bdbcdbf768cbQ");
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            {"referer", RefererHeaderValue},
                                            {"origin",  OriginHeaderValue}
                                        };

            // Act
            var response = new ApiActionsLibrary().PerformGetRequest<UserProfileModel>(requestUrl, requestHeaders);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
                    {
                        Assert.AreEqual(400, (int)response.StatusCode, "Status code is NOT 400. Please check!");
                        // Headers
                        Assert.True(response.Headers.Any(ele => ele.Name == "strict-transport-security" && Regex.IsMatch(ele.Value.ToString(), @"max-age=[\d]+$")), "Header: 'strict-transport-security' is missing or value doesn't match. Please check!");
                        Assert.True(response.Headers.Any(ele => ele.Name == "access-control-expose-headers" && ele.Value.ToString() == "*"), "Header: 'access-control-expose-headers' is missing or value doesn't macth. Please check!");
                        Assert.True(response.Headers.Any(ele => ele.Name == "Access-Control-Allow-Origin" && ele.Value.ToString() == "*"), "Header: 'Access-Control-Allow-Origin' is missing or value doesn't macth. Please check!");
                        Assert.True(response.Headers.Any(ele => ele.Name == "server-timing" && Regex.IsMatch(ele.Value.ToString(), @"rsp;dur=[\d]{1,4}$")), "Header: 'server-timing' is missing or time exceeds 10 seconds. Please check!");
                        Assert.True(response.Headers.Any(ele => ele.Name == "Cache-Control" && ele.Value.ToString() == "private, no-cache"), "Header: 'Cache-Control' is missing or value doesn't match. Please check!");
                        Assert.True(response.Headers.Any(ele => ele.Name == "timing-allow-origin" && ele.Value.ToString().Length > 10), "Header: 'timing-allow-origin' is missing or value doesn't match. Please check!");
                        Assert.True(response.Headers.Any(ele => ele.Name == "Content-Type" && ele.Value.ToString() == "application/json; charset=utf-8"), "Header: 'Content-Type' is missing or value doesn't match. Please check!");
                        Assert.True(response.Headers.Any(ele => ele.Name == "Date" && Regex.IsMatch(ele.Value.ToString(), @"\w{3},\s[0-3][0-9]\s\w{3,9}\s\d{4}\s[0-2][0-9]:[0-5][0-9]:[0-5][0-9]\sGMT")), "Header: 'Date' is missing or value doesn't match. Please check!");
                        // Body
                        Assert.IsEmpty(response.Content, "Response body is NOT Empty. Please check!");
                    });
        }

        [Test]
        public void St_45585_UserShouldBeABleTo_UpdateProfileName()
        {
            // Arrange
            var requestUrl = new PostawayProxy().UserProfile(ProfileId);
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            {"referer", RefererHeaderValue},
                                            {"origin",  OriginHeaderValue}
                                        };
            string userName = "Testing QA" + new Random().Next(1, 20);
            string requestBody = "{\r\n  \"displayName\": \"" + userName + "\"\r\n}";

            // Act
            var response = new ApiActionsLibrary().PerformPutRequest(requestUrl, requestHeaders, requestBody);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(200, (int)response.StatusCode, "Status code is NOT 200. Please check!");
                // Headers
                Assert.True(response.Headers.Any(ele => ele.Name == "strict-transport-security" && Regex.IsMatch(ele.Value.ToString(), @"max-age=[\d]+$")), "Header: 'strict-transport-security' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "access-control-expose-headers" && ele.Value.ToString() == "*"), "Header: 'access-control-expose-headers' is missing or value doesn't macth. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Access-Control-Allow-Origin" && ele.Value.ToString() == "*"), "Header: 'Access-Control-Allow-Origin' is missing or value doesn't macth. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "server-timing" && Regex.IsMatch(ele.Value.ToString(), @"rsp;dur=[\d]{1,4}$")), "Header: 'server-timing' is missing or time exceeds 10 seconds. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "timing-allow-origin" && ele.Value.ToString().Length > 10), "Header: 'timing-allow-origin' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Cache-Control" && ele.Value.ToString() == "private, no-cache"), "Header: 'Cache-Control' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Date" && Regex.IsMatch(ele.Value.ToString(), @"\w{3},\s[0-3][0-9]\s\w{3,9}\s\d{4}\s[0-2][0-9]:[0-5][0-9]:[0-5][0-9]\sGMT")), "Header: 'Date' is missing or value doesn't match. Please check!");
                // Body
                Assert.IsEmpty(response.Content, "Response body is NOT Empty. Please check!");
            });

            var getResponse = new ApiActionsLibrary().PerformGetRequest<UserProfileModel>(requestUrl, requestHeaders, logToReport: false);
            Assert.AreEqual(userName, getResponse.Data.DisplayName, "Profile name is NOT updated properly.");
        }
    }
}
