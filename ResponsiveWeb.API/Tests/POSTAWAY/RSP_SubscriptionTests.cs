using AventStack.ExtentReports;
using NUnit.Framework;
using AutomationFramework.Libraries;
using ResponsiveWeb.API.Configuration.TestConfig;
using ResponsiveWeb.API.Models.Response;
using ResponsiveWeb.API.Services;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using static ResponsiveWeb.API.Data.TestData;
using AutomationFramework.Configuration.ReportConfig;
using AutomationFramework.Configuration.DbConfig;

namespace ResponsiveWeb.API.Tests
{
    [TestFixture]
    [NonParallelizable]
    [Category("Postaway")]
    [Category("All")]
    public class RSP_SubscriptionTests : BaseTest
    {
        [Test]
        public void St_45741_GetDeliveryOptions()
        {
            // Arrange
            var requestUrl = new PostawayProxy().DeliveryOptions(ProfileId);
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            {"referer", RefererHeaderValue},
                                            {"origin",  OriginHeaderValue}
                                        };
            // Act
            var response = new ApiActionsLibrary().PerformGetRequest<DeliveryOptionsModel>(requestUrl, requestHeaders);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
                    {
                        // Status
                        Assert.AreEqual(200, (int)response.StatusCode, "Status code is NOT equal to 200. Please check!");
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
                        Assert.True(Regex.IsMatch(response.Data.Email, @"^[a-zA-Z0-9.!#$%&’'*+=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$"));
                    });
        }

        [Test]
        public void St_45741_SetDeliveryOptions()
        {
            /*
             * Step 1: Get delivery options (email & SMS field values)
             * Step 2: Add a phone number for SMS subscription
             * Step 3: Set delivery options to TRUE or FALSE (email & SMS), opposite to step1 values
             * Step 4: Validation that delivery options are changed
             * 
             */

            // ================== Step 1 ====================
            bool set_email_flag = true;
            bool set_sms_flag = true;

            var requestUrl = new PostawayProxy().DeliveryOptions(ProfileId);
            var requestHeaders = new Dictionary<string, string>()
                                       {
                                           {"referer", RefererHeaderValue},
                                           {"origin",  OriginHeaderValue}
                                       };
            var response = new ApiActionsLibrary().PerformGetRequest<DeliveryOptionsModel>(requestUrl, requestHeaders, logToReport: false);
            if (response.Data.EmailEnabled && response.Data.SmsEnabled)
            {
                set_email_flag = false;
                set_sms_flag = false;
            }

            // ================== Step 2 ====================

            var requestUrl_1 = new PostawayProxy().SmsSubscription(ProfileId);
            var requestBody_1 = "{\"phone\":\"6045555555\"}";
            var response_1 = new ApiActionsLibrary().PerformPutRequest(requestUrl_1, requestHeaders, requestBody_1, logToReport: false);
            Assert.AreEqual(200, (int)response_1.StatusCode);

            // ================== Step 3 ====================
            // Arrange
            var requestUrl_2 = new PostawayProxy().DeliveryOptions(ProfileId);
            var requestBody_2 = "{\"emailEnabled\":" + set_email_flag.ToString().ToLower() + ",\"smsEnabled\":" + set_sms_flag.ToString().ToLower() + "}";
            // Act
            var response_2 = new ApiActionsLibrary().PerformPutRequest(requestUrl_2, requestHeaders, requestBody_2);
            WriteResposeToReport(response_2);
            // Assert
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(200, (int)response.StatusCode, "Status code is NOT equal to 200. Please check!");
                // Headers
                Assert.True(response.Headers.Any(ele => ele.Name == "strict-transport-security" && Regex.IsMatch(ele.Value.ToString(), @"max-age=[\d]+$")), "Header: 'strict-transport-security' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "access-control-expose-headers" && ele.Value.ToString() == "*"), "Header: 'access-control-expose-headers' is missing or value doesn't macth. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Access-Control-Allow-Origin" && ele.Value.ToString() == "*"), "Header: 'Access-Control-Allow-Origin' is missing or value doesn't macth. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "server-timing" && Regex.IsMatch(ele.Value.ToString(), @"rsp;dur=[\d]{1,4}$")), "Header: 'server-timing' is missing or time exceeds 10 seconds. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "timing-allow-origin" && ele.Value.ToString().Length > 10), "Header: 'timing-allow-origin' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Cache-Control" && ele.Value.ToString() == "private, no-cache"), "Header: 'Cache-Control' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Date" && Regex.IsMatch(ele.Value.ToString(), @"\w{3},\s[0-3][0-9]\s\w{3,9}\s\d{4}\s[0-2][0-9]:[0-5][0-9]:[0-5][0-9]\sGMT")), "Header: 'Date' is missing or value doesn't match. Please check!");
                // Body
                Assert.IsNotEmpty(response.Content, "Response body is Empty. Please check!");
                Assert.True(Regex.IsMatch(response.Data.Email, @"^[a-zA-Z0-9.!#$%&’'*+=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$"));
            });

            // ================== Step 4: Validation ====================

            var requestUrl_3 = new PostawayProxy().DeliveryOptions(ProfileId);
            var response_3 = new ApiActionsLibrary().PerformGetRequest<DeliveryOptionsModel>(requestUrl_3, requestHeaders, logToReport: false);
            Assert.AreEqual(response_3.Data.EmailEnabled, set_email_flag, "Failed to change the EmailEnabled falg.");
            Assert.AreEqual(response_3.Data.SmsEnabled, set_sms_flag, "Failed to change the SmsEnabled falg.");
        }

        [Test]
        public void St_45741_UserCanSubscribeToSMSAlerts()
        {
            /*
             * Step 1: Add a mobile number
             * Step 2: SMS validation
             * Step 3: Validate that the phone number is subscribed to receive alerts via SMS
             * Step 4: Delete the phonenumber/unsubscribe - to make the test reusable
             * Step 5: Display the Delivery options in the report
             * 
             */

            // ================== Step 1 ====================
            ExtentManager.GetExtentTest().Log(Status.Info, "<b>Step 1:</b> Add a Phone Number");
            var requestUrl = new PostawayProxy().SmsSubscription(ProfileId);
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            {"referer", RefererHeaderValue},
                                            {"origin",  OriginHeaderValue}
                                        };
            var requestBody = "{\"phone\":\"6045555555\"}";
            var response = new ApiActionsLibrary().PerformPutRequest(requestUrl, requestHeaders, requestBody);
            WriteResposeToReport(response);
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(200, (int)response.StatusCode, "Status code is NOT equal to 200. Please check!");
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

            // ================== Step 2 ====================
            ExtentManager.GetExtentTest().Log(Status.Info, "<b>Step 2:</b> Validate phone number with an SMS token");
            requestUrl = new PostawayProxy().SmsValidation(ProfileId);
            requestBody = "{\"validationToken\":" + GetSMSTokenFor(ProfileId) + "}";
            response = new ApiActionsLibrary().PerformPostRequest(requestUrl, requestHeaders, requestBody);
            WriteResposeToReport(response);
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(200, (int)response.StatusCode, "Status code is NOT equal to 200. Please check!");
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

            // ================== Step 3 ====================
            ExtentManager.GetExtentTest().Log(Status.Info, "<b>Step 3:</b> Display Current Delivery Options after a Successful Subscription");
            var requestUrl_1 = new PostawayProxy().DeliveryOptions(ProfileId);
            var response_1 = new ApiActionsLibrary().PerformGetRequest<DeliveryOptionsModel>(requestUrl_1, requestHeaders);
            WriteResposeToReport(response_1);
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(200, (int)response_1.StatusCode, "Status code is NOT equal to 200. Please check!");
                // Headers
                Assert.True(response_1.Headers.Any(ele => ele.Name == "Content-Encoding" && Regex.IsMatch(ele.Value.ToString(), @"^\w{0,4}$")), "Header: 'Content-Encoding' is missing or value doesn't match. Please check!");
                Assert.True(response_1.Headers.Any(ele => ele.Name == "strict-transport-security" && Regex.IsMatch(ele.Value.ToString(), @"max-age=[\d]+$")), "Header: 'strict-transport-security' is missing or value doesn't match. Please check!");
                Assert.True(response_1.Headers.Any(ele => ele.Name == "access-control-expose-headers" && ele.Value.ToString() == "*"), "Header: 'access-control-expose-headers' is missing or value doesn't macth. Please check!");
                Assert.True(response_1.Headers.Any(ele => ele.Name == "Access-Control-Allow-Origin" && ele.Value.ToString() == "*"), "Header: 'Access-Control-Allow-Origin' is missing or value doesn't macth. Please check!");
                Assert.True(response_1.Headers.Any(ele => ele.Name == "server-timing" && Regex.IsMatch(ele.Value.ToString(), @"rsp;dur=[\d]{1,4}$")), "Header: 'server-timing' is missing or time exceeds 10 seconds. Please check!");
                Assert.True(response_1.Headers.Any(ele => ele.Name == "Content-Type" && ele.Value.ToString() == "application/json; charset=utf-8"), "Header: 'Content-Type' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "timing-allow-origin" && ele.Value.ToString().Length > 10), "Header: 'timing-allow-origin' is missing or value doesn't match. Please check!");
                Assert.True(response_1.Headers.Any(ele => ele.Name == "Cache-Control" && ele.Value.ToString() == "private, no-cache"), "Header: 'Cache-Control' is missing or value doesn't match. Please check!");
                Assert.True(response_1.Headers.Any(ele => ele.Name == "Date" && Regex.IsMatch(ele.Value.ToString(), @"\w{3},\s[0-3][0-9]\s\w{3,9}\s\d{4}\s[0-2][0-9]:[0-5][0-9]:[0-5][0-9]\sGMT")), "Header: 'Date' is missing or value doesn't match. Please check!");
                // Body
                Assert.IsNotEmpty(response_1.Content, "Response body is Empty. Please check!");
                Assert.AreEqual(response_1.Data.Phone.ToString(), "6045555555");
                Assert.AreEqual(response_1.Data.SmsEnabled, true);
                Assert.AreEqual(response_1.Data.SmsValidationPending, false);
            });

            // ================== Step 4 ====================
            ExtentManager.GetExtentTest().Log(Status.Info, "<b>Step 4:</b> Unsusbcribe to SMS Alerts - to make the test reusable");
            requestUrl = new PostawayProxy().SmsSubscription(ProfileId);
            response = new ApiActionsLibrary().PerformDeleteRequest(requestUrl, requestHeaders, null);
            WriteResposeToReport(response);
            Assert.Multiple(() =>
                    {
                        // Status
                        Assert.AreEqual(200, (int)response.StatusCode, "Status code is NOT equal to 200. Please check!");
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

            // ================== Step 5 ====================
            ExtentManager.GetExtentTest().Log(Status.Info, "<b>Step 5:</b> Display Current Delivery Options after Unsubscribed");
            requestUrl_1 = new PostawayProxy().DeliveryOptions(ProfileId);
            response_1 = new ApiActionsLibrary().PerformGetRequest<DeliveryOptionsModel>(requestUrl_1, requestHeaders);
            WriteResposeToReport(response_1);
            Assert.Multiple(() =>
                    {
                        // Status
                        Assert.AreEqual(200, (int)response_1.StatusCode, "Status code is NOT equal to 200. Please check!");
                        // Headers
                        Assert.True(response_1.Headers.Any(ele => ele.Name == "Content-Encoding" && Regex.IsMatch(ele.Value.ToString(), @"^\w{0,4}$")), "Header: 'Content-Encoding' is missing or value doesn't match. Please check!");
                        Assert.True(response_1.Headers.Any(ele => ele.Name == "strict-transport-security" && Regex.IsMatch(ele.Value.ToString(), @"max-age=[\d]+$")), "Header: 'strict-transport-security' is missing or value doesn't match. Please check!");
                        Assert.True(response_1.Headers.Any(ele => ele.Name == "access-control-expose-headers" && ele.Value.ToString() == "*"), "Header: 'access-control-expose-headers' is missing or value doesn't macth. Please check!");
                        Assert.True(response_1.Headers.Any(ele => ele.Name == "Access-Control-Allow-Origin" && ele.Value.ToString() == "*"), "Header: 'Access-Control-Allow-Origin' is missing or value doesn't macth. Please check!");
                        Assert.True(response_1.Headers.Any(ele => ele.Name == "server-timing" && Regex.IsMatch(ele.Value.ToString(), @"rsp;dur=[\d]{1,4}$")), "Header: 'server-timing' is missing or time exceeds 10 seconds. Please check!");
                        Assert.True(response_1.Headers.Any(ele => ele.Name == "Content-Type" && ele.Value.ToString() == "application/json; charset=utf-8"), "Header: 'Content-Type' is missing or value doesn't match. Please check!");
                        Assert.True(response.Headers.Any(ele => ele.Name == "timing-allow-origin" && ele.Value.ToString().Length > 10), "Header: 'timing-allow-origin' is missing or value doesn't match. Please check!");
                        Assert.True(response_1.Headers.Any(ele => ele.Name == "Cache-Control" && ele.Value.ToString() == "private, no-cache"), "Header: 'Cache-Control' is missing or value doesn't match. Please check!");
                        Assert.True(response_1.Headers.Any(ele => ele.Name == "Date" && Regex.IsMatch(ele.Value.ToString(), @"\w{3},\s[0-3][0-9]\s\w{3,9}\s\d{4}\s[0-2][0-9]:[0-5][0-9]:[0-5][0-9]\sGMT")), "Header: 'Date' is missing or value doesn't match. Please check!");
                        // Body
                        Assert.IsNotEmpty(response_1.Content, "Response body is Empty. Please check!");
                        Assert.True(response_1.Data.Phone == null);
                        Assert.AreEqual(response_1.Data.SmsEnabled, false);
                        Assert.AreEqual(response_1.Data.SmsValidationPending, false);
                    });
        }

        [Test]
        public void St_45741_Test400BadRequestForInvalidPhoneNumberFormat()
        {
            var phoneNumbers = new List<string>() { "06045555555", "+16045555555", "A~@.604555" };

            foreach (var number in phoneNumbers)
            {
                ExtentManager.GetExtentTest().Log(Status.Info, $"<b>PhoneNumber format:</b> { number }");
                // Arrange
                var requestUrl = new PostawayProxy().SmsSubscription(ProfileId);
                var requestHeaders = new Dictionary<string, string>()
                                        {
                                            {"referer", RefererHeaderValue},
                                            {"origin",  OriginHeaderValue}
                                        };
                var requestBody = "{\"phone\":\"" + number + "\"}";

                // Act
                var response = new ApiActionsLibrary().PerformPutRequest(requestUrl, requestHeaders, requestBody);

                WriteResposeToReport(response);

                // Assert
                Assert.Multiple(() =>
                {
                    // Status
                    Assert.AreEqual(400, (int)response.StatusCode, "Status code is NOT equal to 400. Please check!");
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
        }

        [Test]
        public void St_45741_UserCantSubscribeToSMSAlerts_WithoutValidatingPhoneNumberViaSMSToken()
        {
            /*
             * Step 1: Delete SMS subscription, if any
             * Step 2: Try to set Delivery Options smsEnabled = true without adding a phonenumber
             * Step 3: Validation: 400 Bad request
             * 
             */

            // ================== Step 1 ====================
            var requestUrl = new PostawayProxy().SmsSubscription(ProfileId);
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            {"referer", RefererHeaderValue},
                                            {"origin",  OriginHeaderValue}
                                        };
            var response = new ApiActionsLibrary().PerformDeleteRequest(requestUrl, requestHeaders, null, logToReport: false);
            Assert.AreEqual(200, (int)response.StatusCode, "Status code is NOT equal to 200. Please check!");

            // ================== Step 2 ====================
            requestUrl = new PostawayProxy().DeliveryOptions(ProfileId);
            var requestBody = "{\"smsEnabled\":true}";
            response = new ApiActionsLibrary().PerformPutRequest(requestUrl, requestHeaders, requestBody);

            WriteResposeToReport(response);

            // ================== Step 3 ====================            
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(400, (int)response.StatusCode, "Status code is NOT equal to 400. Please check!");
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
        public void St_45741_UserCanSubscribeToEmailAlerts()
        {
            // Arrange
            var requestUrl = new PostawayProxy().DeliveryOptions(ProfileId);
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            {"referer", RefererHeaderValue},
                                            {"origin",  OriginHeaderValue}
                                        };
            var requestBody = "{\"emailEnabled\":true}";

            // Act
            var response = new ApiActionsLibrary().PerformPutRequest(requestUrl, requestHeaders, requestBody);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(200, (int)response.StatusCode, "Status code is NOT equal to 200. Please check!");
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

            ExtentManager.GetExtentTest().Log(Status.Info, "<b>Display Current Delivery Options</b> ");
            requestUrl = new PostawayProxy().DeliveryOptions(ProfileId);
            var response_1 = new ApiActionsLibrary().PerformGetRequest<DeliveryOptionsModel>(requestUrl, requestHeaders);
            WriteResposeToReport(response_1);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(200, (int)response_1.StatusCode, "Status code is NOT equal to 200. Please check!");
                Assert.IsNotEmpty(response_1.Content, "Response body is Empty. Please check!");
                Assert.AreEqual(response_1.Data.EmailEnabled, true, "Failed to Subscribe to Email Alerts");
            });
        }

        [Test]
        public void St_45741_UserCanUnSubscribeToEmailAlerts()
        {
            // Arrange
            var requestUrl = new PostawayProxy().DeliveryOptions(ProfileId);
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            {"referer", RefererHeaderValue},
                                            {"origin",  OriginHeaderValue}
                                        };
            var requestBody = "{\"emailEnabled\":false}";

            // Act
            var response = new ApiActionsLibrary().PerformPutRequest(requestUrl, requestHeaders, requestBody);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(200, (int)response.StatusCode, "Status code is NOT equal to 200. Please check!");
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


            ExtentManager.GetExtentTest().Log(Status.Info, "<b>Display Current Delivery Options</b> ");
            requestUrl = new PostawayProxy().DeliveryOptions(ProfileId);
            var response_1 = new ApiActionsLibrary().PerformGetRequest<DeliveryOptionsModel>(requestUrl, requestHeaders);
            WriteResposeToReport(response_1);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(200, (int)response_1.StatusCode, "Status code is NOT equal to 200. Please check!");
                Assert.IsNotEmpty(response_1.Content, "Response body is Empty. Please check!");
                Assert.AreEqual(response_1.Data.EmailEnabled, false, "Failed to Subscribe to Email Alerts");
            });
        }

        [Test]
        public void St_45741_TestUnsubscribeFromEmailAndSMSNotificationsAtOnce()
        {
            /*
             * Step 1: Unsubscribe to Email and SMS notifications 
             * Step 2: Validation and Display Current Delivery Options in the report
             * 
             */

            ExtentManager.GetExtentTest().Log(Status.Info, "<b>Step 1:</b> Unsubscribe From Email and SMS Notifications at Once");
            // Arrange
            var requestUrl = new PostawayProxy().UnSubscribeFromEmailAndSms();
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            {"referer", RefererHeaderValue},
                                            {"origin",  OriginHeaderValue}
                                        };
            var requestBody = "{\"unsubscribeRequestId\": \"" + GetUnsubcriptionIdFor(ProfileId) + "\"}";
            // Act
            var response = new ApiActionsLibrary().PerformPutRequest(requestUrl, requestHeaders, requestBody);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
                    {
                        // Status
                        Assert.AreEqual(200, (int)response.StatusCode, "Status code is NOT equal to 200. Please check!");
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

            ExtentManager.GetExtentTest().Log(Status.Info, "<b>Step 2:</b> Display Current Delivery Options after Unsubscribed");
            requestUrl = new PostawayProxy().DeliveryOptions(ProfileId);
            var response_1 = new ApiActionsLibrary().PerformGetRequest<DeliveryOptionsModel>(requestUrl, requestHeaders);
            WriteResposeToReport(response_1);
            Assert.Multiple(() =>
                    {
                        // Status
                        Assert.AreEqual(200, (int)response_1.StatusCode, "Status code is NOT equal to 200. Please check!");
                        // Headers
                        Assert.True(response_1.Headers.Any(ele => ele.Name == "Content-Encoding" && Regex.IsMatch(ele.Value.ToString(), @"^\w{0,4}$")), "Header: 'Content-Encoding' is missing or value doesn't match. Please check!");
                        Assert.True(response_1.Headers.Any(ele => ele.Name == "strict-transport-security" && Regex.IsMatch(ele.Value.ToString(), @"max-age=[\d]+$")), "Header: 'strict-transport-security' is missing or value doesn't match. Please check!");
                        Assert.True(response_1.Headers.Any(ele => ele.Name == "access-control-expose-headers" && ele.Value.ToString() == "*"), "Header: 'access-control-expose-headers' is missing or value doesn't macth. Please check!");
                        Assert.True(response_1.Headers.Any(ele => ele.Name == "Access-Control-Allow-Origin" && ele.Value.ToString() == "*"), "Header: 'Access-Control-Allow-Origin' is missing or value doesn't macth. Please check!");
                        Assert.True(response_1.Headers.Any(ele => ele.Name == "server-timing" && Regex.IsMatch(ele.Value.ToString(), @"rsp;dur=[\d]{1,4}$")), "Header: 'server-timing' is missing or time exceeds 10 seconds. Please check!");
                        Assert.True(response_1.Headers.Any(ele => ele.Name == "Content-Type" && ele.Value.ToString() == "application/json; charset=utf-8"), "Header: 'Content-Type' is missing or value doesn't match. Please check!");
                        Assert.True(response.Headers.Any(ele => ele.Name == "timing-allow-origin" && ele.Value.ToString().Length > 10), "Header: 'timing-allow-origin' is missing or value doesn't match. Please check!");
                        Assert.True(response_1.Headers.Any(ele => ele.Name == "Cache-Control" && ele.Value.ToString() == "private, no-cache"), "Header: 'Cache-Control' is missing or value doesn't match. Please check!");
                        Assert.True(response_1.Headers.Any(ele => ele.Name == "Date" && Regex.IsMatch(ele.Value.ToString(), @"\w{3},\s[0-3][0-9]\s\w{3,9}\s\d{4}\s[0-2][0-9]:[0-5][0-9]:[0-5][0-9]\sGMT")), "Header: 'Date' is missing or value doesn't match. Please check!");
                        // Body
                        Assert.IsNotEmpty(response_1.Content, "Response body is Empty. Please check!");
                        Assert.True(response_1.Data.EmailEnabled == false);
                        Assert.True(response_1.Data.Phone == null);
                        Assert.AreEqual(response_1.Data.SmsEnabled, false);
                        Assert.AreEqual(response_1.Data.SmsValidationPending, false);
                    });
        }

        [Test]
        public void St_45741_TestUnsubscribeFromEmailAndSMSNotificationsAtOnce_WithWrongRequestId()
        {

            // Arrange
            var requestUrl = new PostawayProxy().UnSubscribeFromEmailAndSms();
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            {"referer", RefererHeaderValue},
                                            {"origin",  OriginHeaderValue}
                                        };
            var requestBody = "{\"unsubscribeRequestId\": \"666C6BEC-2EE1-41A1-AE58-072AD5EEDEAC\"}";

            // Act
            var response = new ApiActionsLibrary().PerformPutRequest(requestUrl, requestHeaders, requestBody);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
                    {
                        // Status
                        Assert.AreEqual(404, (int)response.StatusCode, "Status code is NOT equal to 404. Please check!");
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

        // Local methods
        private int GetSMSTokenFor(string profileId)
        {
            object token = null;
            var connection = new SQLserver().OpenConnection();
            var query = "select preferencesTable.*from dbo.UserProfile profiletable,UserPreference preferencesTable where profiletable.ProfileId = '" + profileId + "'" + " and profiletable.ProfileId = preferencesTable.ProfileId";

            using (SqlCommand sqlCommand = new SqlCommand(query, connection))
            {
                var dataReader = sqlCommand.ExecuteReader();
                if (dataReader.Read())
                {
                    token = dataReader["SMSValidationToken"];
                }
            }

            return int.Parse(token.ToString());
        }
        private string GetUnsubcriptionIdFor(string profileId)
        {
            object UnsubscriptionRequestID = null;
            var connection = new SQLserver().OpenConnection();
            var query = "select preferencesTable.*from dbo.UserProfile profiletable,UserPreference preferencesTable where profiletable.ProfileId = '" + profileId + "'" + " and profiletable.ProfileId = preferencesTable.ProfileId";

            using (SqlCommand sqlCommand = new SqlCommand(query, connection))
            {
                var dataReader = sqlCommand.ExecuteReader();
                if (dataReader.Read())
                {
                    UnsubscriptionRequestID = dataReader["UnsubscriptionRequestIdentifier"];
                }
            }

            return UnsubscriptionRequestID.ToString().ToUpper();
        }
    }
}