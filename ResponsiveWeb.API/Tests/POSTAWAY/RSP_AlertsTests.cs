using AventStack.ExtentReports;
using NUnit.Framework;
using AutomationFramework.Libraries;
using ResponsiveWeb.API.Configuration.TestConfig;
using ResponsiveWeb.API.Models.Response;
using ResponsiveWeb.API.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static ResponsiveWeb.API.Data.TestData;
using AutomationFramework.Configuration.ReportConfig;

namespace ResponsiveWeb.API.Tests
{
    [TestFixture]
    [NonParallelizable]
    [Category("Postaway")]
    [Category("All")]
    public class RSP_AlertsTests : BaseTest
    {
        readonly string routeNumber = "050";

        [Test]
        public void St_45588_RetrieveAllSubscribedAlertsForAnUser()
        {
            // Arrange
            var requestUrl = new PostawayProxy().RspAlerts(ProfileId);
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            {"referer", RefererHeaderValue},
                                            {"origin",  OriginHeaderValue}
                                        };

            // Act
            var response = new ApiActionsLibrary().PerformGetRequest<List<RegisteredAlertsModel>>(requestUrl, requestHeaders);

            WriteResposeToReport(response);

            // Assert

            Assert.Multiple(() =>
                    {
                        // Status
                        Assert.AreEqual(200, (int)response.StatusCode, "Failed to retrieve all alerts for an user. Please check!");
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
                        Assert.IsNotEmpty(response.Content, "Response body is Empty. Please check!");
                        Assert.True(response.Data.All(ele => Regex.IsMatch(ele.RouteNumber, @"^[\d\w]{2,3}$")), "There is an invalid RouteNumber in the response. Please check!");
                        Assert.True(response.Data.All(ele => ele.ActiveDaysOfWeek.GetType() == typeof(int)), "There is an invalid ActiveDaysOfWeek value in the response. Please check!");
                        Assert.True(response.Data.All(ele => ele.IsEnabled.GetType() == typeof(bool)), "There is an invalid IsEnabled flag value in the response. Please check!");
                    });
        }

        [Test, Order(1)]
        public void St_45588_UserShouldBeAbleTo_SubscribeToANewAlert()
        {
            // Arrange
            var requestUrl = new PostawayProxy().RspAlerts(ProfileId);
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            {"referer", RefererHeaderValue},
                                            {"origin",  OriginHeaderValue}
                                        };
            object requestBody = "[{\"routeNumber\":\"" + routeNumber + "\",\"activeDaysOfWeek\":127,\"isEnabled\":true}]";

            // Delete the alert - if already exists!
            new ApiActionsLibrary().PerformDeleteRequest(requestUrl, requestHeaders, "[{\"routeNumber\":\"" + routeNumber + "\"}]", logToReport: false);

            // Act
            // Create a new alert
            var response = new ApiActionsLibrary().PerformPostRequest(requestUrl, requestHeaders, requestBody);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(201, (int)response.StatusCode, "Failed to create a new alert. Please check!");
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

            // Get All alerts & verify that above created alert exists
            var getResponse = new ApiActionsLibrary().PerformGetRequest<List<RegisteredAlertsModel>>(requestUrl, requestHeaders, logToReport: false);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(200, (int)getResponse.StatusCode, "Failed to retrieve the above created alert by making a get request. Please check!");
                Assert.True(getResponse.Headers.Any(ele => ele.Name == "access-control-expose-headers" && ele.Value.ToString() == "*"), "Response headers does not contain 'access-control-expose-headers'. Please check!");
                Assert.True(getResponse.Headers.Any(ele => ele.Name == "server-timing" && Regex.IsMatch(ele.Value.ToString(), @"rsp;dur=\d{0,4}$")), "Response headers does not contain 'server-timing' or time exceeds 10 seconds. Please check!");
                Assert.IsNotEmpty(getResponse.Content, "Response body is NOT Empty. Please check!");
                Assert.True(getResponse.Data.Any(ele => ele.RouteNumber == routeNumber), "Not able to retrieve the newly created alert.");
            });
        }

        [Test, Order(2)]
        public void St_45588_UserShouldBeAbleTo_UpdateSubscribedAlertInformation()
        {
            // Arrange
            var requestUrl = new PostawayProxy().RspAlerts(ProfileId);
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            {"referer", RefererHeaderValue},
                                            {"origin",  OriginHeaderValue}
                                        };
            object requestBody = "[{\"routeNumber\":\"" + routeNumber + "\",\"activeDaysOfWeek\":100,\"isEnabled\":false}]";

            var response = new ApiActionsLibrary().PerformPutRequest(requestUrl, requestHeaders, requestBody);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(200, (int)response.StatusCode, "Failed to update the existing alert. Please check!");
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

        [Test, Order(3)]
        public void St_45588_ShouldNotBeABleTo_UpdateSubsribedAlertInformation_WithWrongData()
        {
            // Arrange
            var requestUrl = new PostawayProxy().RspAlerts(ProfileId);
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            {"referer", RefererHeaderValue},
                                            {"origin",  OriginHeaderValue}
                                        };
            // 365 is not a valid value
            object requestBody = "[{\"routeNumber\":\"" + routeNumber + "\",\"activeDaysOfWeek\":365,\"isEnabled\":false}]";

            var response = new ApiActionsLibrary().PerformPutRequest(requestUrl, requestHeaders, requestBody);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(400, (int)response.StatusCode, "Able to update an alert with wrong data. Please check!");
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

        [Test, Order(4)]
        public void St_45588_UserShouldBeAbleTo_DeleteASubscribedAlert()
        {
            // Arrange
            var requestUrl = new PostawayProxy().RspAlerts(ProfileId);
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            {"referer", RefererHeaderValue},
                                            {"origin",  OriginHeaderValue}
                                        };
            object requestBody = "[{\"routeNumber\":\"" + routeNumber + "\"}]";

            // Act
            // Delete an alert
            var response = new ApiActionsLibrary().PerformDeleteRequest(requestUrl, requestHeaders, requestBody);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(200, (int)response.StatusCode, "Failed to delete an alert. Please check!");
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

            // Delete an already deleted alert            
            response = new ApiActionsLibrary().PerformDeleteRequest(requestUrl, requestHeaders, requestBody, logToReport: false);

            // Assert for 404 - Not Found
            Assert.Multiple(() =>
            {
                Assert.AreEqual(404, (int)response.StatusCode, "Status code is NOT 404. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "server-timing" && Regex.IsMatch(ele.Value.ToString(), @"rsp;dur=\d{0,4}$")), "Response headers does not contain 'server-timing' or time exceeds 10 seconds. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "access-control-expose-headers" && ele.Value.ToString() == "*"), "Response headers does not contain 'access-control-expose-headers'. Please check!");
                Assert.IsEmpty(response.Content, "Response body is NOT Empty. Please check!");
            });
        }

        [Test]
        public void St_45588_ShouldNotBeAbleTo_SubscribeAnAlertForAnUnknownRoute()
        {
            // Arrange
            var requestUrl = new PostawayProxy().RspAlerts(ProfileId);
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            {"referer", RefererHeaderValue},
                                            {"origin",  OriginHeaderValue}
                                        };
            // There is no route with value 'AB4'
            object requestBody = "[{\"routeNumber\":\"AB4\"}]";

            // Act
            var response = new ApiActionsLibrary().PerformPostRequest(requestUrl, requestHeaders, requestBody);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(404, (int)response.StatusCode, "Able to create a non-existant alert. Please check!");
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
        public void St_45588_ShouldNotBeABleTo_UpdateUnSubscribedAlert()
        {
            // Arrange
            var requestUrl = new PostawayProxy().RspAlerts(ProfileId);
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            {"referer", RefererHeaderValue},
                                            {"origin",  OriginHeaderValue}
                                        };
            // Assuming there is no alert created for route number 745
            object requestBody = "[{\"routeNumber\":\"745\",\"activeDaysOfWeek\":127,\"isEnabled\":true}]";

            // Deleting the alert, if exists
            new ApiActionsLibrary().PerformDeleteRequest(requestUrl, requestHeaders, requestBody, logToReport: false);

            // Act
            var response = new ApiActionsLibrary().PerformPutRequest(requestUrl, requestHeaders, requestBody);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(404, (int)response.StatusCode, $"Expecting a 404 Not found but was { response.StatusCode }", "Please check!");
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
        public void St_45588_ShouldNotBeAbleTo_DeleteAnUnSubscribedAlert()
        {
            // Arrange
            var requestUrl = new PostawayProxy().RspAlerts(ProfileId);
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            {"referer", RefererHeaderValue},
                                            {"origin",  OriginHeaderValue}
                                        };
            object requestBody = "[{\"routeNumber\":\"861\"}]";

            // Act
            // Delete an alert
            var response = new ApiActionsLibrary().PerformDeleteRequest(requestUrl, requestHeaders, requestBody);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(404, (int)response.StatusCode, $"Expecting a 404 Not Found but was { response.StatusCode }. Please check!");
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
        public void St_40919_401UnauthorizedForMissingRefererHeader()
        {
            string profileId = "c9d0ss11c1-595sc-4a70-bb1c-e7e6c2aa06122";

            var requestList = new List<string>()
                                    {
                                        new PostawayProxy().UserProfile(profileId),
                                        new PostawayProxy().RspAlerts(profileId),
                                        new PostawayProxy().DeliveryOptions(profileId),
                                        new PostawayProxy().SmsSubscription(profileId),
                                        new PostawayProxy().SmsValidation(profileId),
                                        new PostawayProxy().SnoozeDeliverySchedule(profileId),
                                        new PostawayProxy().UnSubscribeFromEmailAndSms()
                                    };

            var responseList = new List<int>();

            foreach (var requestUrl in requestList)
            {
                var response = new ApiActionsLibrary().PerformGetRequest(requestUrl, null, logToReport: false);
                responseList.Add((int)response.StatusCode);
                response = new ApiActionsLibrary().PerformPostRequest(requestUrl, null, null, logToReport: false);
                responseList.Add((int)response.StatusCode);
                response = new ApiActionsLibrary().PerformPutRequest(requestUrl, null, null, logToReport: false);
                responseList.Add((int)response.StatusCode);
                response = new ApiActionsLibrary().PerformDeleteRequest(requestUrl, null, null, logToReport: false);
                responseList.Add((int)response.StatusCode);
            }

            // Assert
            Assert.True(responseList.All(ele => ele.Equals(401) || ele.Equals(404)), "Status code is NOT 401. Please check!");
            ExtentManager.GetExtentTest().Log(Status.Pass, "All the postaway apis return a 401-Unauthorized when referer header is not passed as a request header.");
        }

        [Test, Order(5)]
        public void St_60244_ShouldBeAbleTo_SubscribeAlertForHandyDART()
        {
            // Arrange
            var requestUrl = new PostawayProxy().RspAlerts(ProfileId);
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            {"referer", RefererHeaderValue},
                                            {"origin",  OriginHeaderValue}
                                        };
            object requestBody = "[{\"routeNumber\":\"HD\",\"activeDaysOfWeek\":127}]";

            // Act
            // Delete an alert
            var response = new ApiActionsLibrary().PerformPostRequest(requestUrl, requestHeaders, requestBody);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(201, (int)response.StatusCode, $"Expecting a 404 Not Found but was { response.StatusCode }. Please check!");
                // Headers
                Assert.True(response.Headers.Any(ele => ele.Name == "strict-transport-security" && Regex.IsMatch(ele.Value.ToString(), @"max-age=[\d]+$")), "Header: 'strict-transport-security' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "access-control-expose-headers" && ele.Value.ToString() == "*"), "Header: 'access-control-expose-headers' is missing or value doesn't macth. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Access-Control-Allow-Origin" && ele.Value.ToString() == "*"), "Header: 'Access-Control-Allow-Origin' is missing or value doesn't macth. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "server-timing" && Regex.IsMatch(ele.Value.ToString(), @"rsp;dur=[\d]{1,4}$")), "Header: 'server-timing' is missing or time exceeds 10 seconds. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Cache-Control" && ele.Value.ToString() == "private, no-cache"), "Header: 'Cache-Control' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "timing-allow-origin" && ele.Value.ToString().Length > 10), "Header: 'timing-allow-origin' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Date" && Regex.IsMatch(ele.Value.ToString(), @"\w{3},\s[0-3][0-9]\s\w{3,9}\s\d{4}\s[0-2][0-9]:[0-5][0-9]:[0-5][0-9]\sGMT")), "Header: 'Date' is missing or value doesn't match. Please check!");
                // Body
                Assert.IsEmpty(response.Content, "Response body is NOT Empty. Please check!");
            });
        }

        [Test, Order(6)]
        public void St_60244_ShouldBeAbleTo_DeleteAlertForHandyDART()
        {
            // Arrange
            var requestUrl = new PostawayProxy().RspAlerts(ProfileId);
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            {"referer", RefererHeaderValue},
                                            {"origin",  OriginHeaderValue}
                                        };
            object requestBody = "[{\"routeNumber\":\"HD\"}]";

            // Act
            // Delete an alert
            var response = new ApiActionsLibrary().PerformDeleteRequest(requestUrl, requestHeaders, requestBody);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(200, (int)response.StatusCode, "Failed to delete an alert. Please check!");
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

            // Delete an already deleted alert            
            response = new ApiActionsLibrary().PerformDeleteRequest(requestUrl, requestHeaders, requestBody, logToReport: false);

            // Assert for 404 - Not Found
            Assert.Multiple(() =>
            {
                Assert.AreEqual(404, (int)response.StatusCode, "Status code is NOT 404. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "server-timing" && Regex.IsMatch(ele.Value.ToString(), @"rsp;dur=\d{0,4}$")), "Response headers does not contain 'server-timing' or time exceeds 10 seconds. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "access-control-expose-headers" && ele.Value.ToString() == "*"), "Response headers does not contain 'access-control-expose-headers'. Please check!");
                Assert.IsEmpty(response.Content, "Response body is NOT Empty. Please check!");
            });
        }
    }
}