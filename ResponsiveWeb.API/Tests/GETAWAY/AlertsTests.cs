using NUnit.Framework;
using ResponsiveWeb.API.Configuration.TestConfig;
using ResponsiveWeb.API.Models.Response;
using ResponsiveWeb.API.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static ResponsiveWeb.API.Data.TestData;
using AutomationFramework.Libraries;

namespace ResponsiveWeb.API.Tests
{
    [TestFixture]
    [Category("Getaway")]
    [Category("All")]
    public class AlertsTests : BaseTest
    {
        [Test]
        public void St_45748_RetrieveAllCurrentAlerts()
        {
            // Arrange
            var requestUrl = new GetawayProxy().AllAlerts();
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            { "origin",  OriginHeaderValue }
                                        };
            // Act
            var response = new ApiActionsLibrary().PerformGetRequest<List<AllAlertsModel>>(requestUrl, requestHeaders);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
                {
                    // Status
                    Assert.AreEqual(200, (int)response.StatusCode, "Status code is not equal to 200. Please check!");
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
                    Assert.IsNotEmpty(response.Content, "Received an empty reponse. Please check!");
                    if (response.Data.Count > 0)
                    {
                        response.Data.ForEach(index =>
                                    {
                                        Assert.IsNotEmpty(index.Id.ToString(), $"Id text is empty. Please check!");
                                        Assert.IsNotEmpty(index.AlertText, $"Alert text is empty for Id { index.Id }");
                                        Assert.IsNotEmpty(index.Header, $"Header text is empty for Id { index.Id }");
                                        Assert.IsNotEmpty(index.StartTime.ToString(), $"StartTime is empty for Id { index.Id }");
                                    });
                    }
                });
        }

        [Test]
        public void St_45748_RetrieveBannerAlerts()
        {
            // Arrange
            var requestUrl = new GetawayProxy().BannerAlerts();
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            { "origin",  OriginHeaderValue }
                                        };

            // Act
            var response = new ApiActionsLibrary().PerformGetRequest<List<BannerAlertsModel>>(requestUrl, requestHeaders);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(200, (int)response.StatusCode, "Status code is not equal to 200. Please check!");
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
                Assert.IsNotEmpty(response.Content, "Received an empty reponse. Please check!");
                if (response.Data.Count > 0)
                {
                    response.Data.ForEach(index =>
                    {
                        Assert.IsNotEmpty(index.Id.ToString(), $"Id text is empty. Please check!");
                        Assert.IsNotEmpty(index.AlertText, $"Alert text is empty for Id { index.Id }");
                        Assert.IsNotEmpty(index.Header, $"Header text is empty for Id { index.Id }");
                        Assert.IsNotEmpty(index.StartTime.ToString(), $"StartTime is empty for Id { index.Id }");
                    });
                }
            });
        }

        [Test]
        public void St_45748_RetrieveWebBannerAlerts()
        {
            // Arrange
            var requestUrl = new GetawayProxy().WebBannerAlerts();
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            { "origin",  OriginHeaderValue }
                                        };
            // Act
            var response = new ApiActionsLibrary().PerformGetRequest<List<BannerAlertsModel>>(requestUrl, requestHeaders);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(200, (int)response.StatusCode, "Status code is not equal to 200. Please check!");
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
                Assert.IsNotEmpty(response.Content, "Received an empty reponse. Please check!");
                if (response.Data.Count > 0)
                {
                    response.Data.ForEach(index =>
                    {
                        Assert.IsNotEmpty(index.Id.ToString(), $"Id text is empty. Please check!");
                        Assert.IsNotEmpty(index.AlertText, $"Alert text is empty for Id { index.Id }");
                        Assert.IsNotEmpty(index.Header, $"Header text is empty for Id { index.Id }");
                        Assert.IsNotEmpty(index.StartTime.ToString(), $"StartTime is empty for Id { index.Id }");
                    });
                }
            });
        }

        [Test]
        public void St_45748_TestThatLastAlertRefreshDateTimeOfTAMSsystem_IsInUTCformat()
        {
            // Arrange
            var requestUrl = new GetawayProxy().LastRefresh();
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
                Assert.AreEqual(200, (int)response.StatusCode, "Status code is not equal to 200. Please check!");
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
                Assert.True(Regex.IsMatch(response.Content.Trim(new char[] { '\"' }), @"\d{4}-[01][0-9]-[0-3][0-9]T[0-2][0-9]:[0-5][0-9]:[0-5][0-9].[0-9]+Z"), "Failed to display last refresh date in the UTC date time format.");
            });
        }

        [TestCase("Advisory")]
        [TestCase("Bus")]
        [TestCase("SkyTrain")]
        [TestCase("WCE")]
        [TestCase("Seabus")]
        [TestCase("HandyDART")]
        [TestCase("InfoServices")]
        [TestCase("StationAccess")]
        public void St_45748_CheckTotalAlertsCountFor(string routeMedium)
        {
            // Arrange
            var requestUrl = new GetawayProxy().TotalAlertsFor(routeMedium);
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
                Assert.True(int.TryParse(response.Content, out _), "Total count displayed is not an integer(number).");
            });
        }

        [Test]
        public void St_45748_GETAWAY_NewAlertCantBeCreatedViaPOSTcall()
        {
            // Arrange
            var requestUrl = new GetawayProxy().AllAlerts();
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            { "origin",  OriginHeaderValue }
                                        };
            var requestBody = "{\"id\":\"0eda11f0-415c-0cf4-1c7d-cf0c00868390\",\"group\":7,\"closed\":false,\"critical\":false,\"advisory\":false,\"mode\":0,\"routeId\":\"\",\"routeLongName\":\"\",\"stationId\":\"\",\"stationName\":\"\",\"alertLifecycle\":\"NEW\",\"certainty\":\"UNKNOWN\",\"effect\":\"ADVISORY\",\"endStamp\":0,\"startTime\":\"2020-04-01T21:45:26Z\",\"endTime\":null,\"alertText\":\"Technical Service Alert\",\"header\":\"Due to the physical distancing required by health authorities in BC, bus operators will not be able to assist our wheelchair customers with being strapped in a front-facing position on our conventional buses.\",\"description\":\"Those with mobility devices on buses that have a rear facing accessible seat will be able to park in the designated area themselves and secure their device. This is the case for most buses. \r\n\r\nFor buses that do not have this area (e.g. highway coaches, Community Shuttle) customers with mobility devices will need to travel with someone who can assist them or find an alternate mode of travel.\",\"url\":\"\",\"lastModified\":\"2020-04-10T21:34:22Z\"}";

            // Act
            var response = new ApiActionsLibrary().PerformPostRequest(requestUrl, requestHeaders, requestBody);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(404, (int)response.StatusCode, "Status code is not equal to 404. Please check!");
                // Headers
                Assert.True(response.Headers.Any(ele => ele.Name == "Access-Control-Allow-Origin" && ele.Value.ToString() == "*"), "Header: 'Access-Control-Allow-Origin' is missing or value doesn't macth. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Date" && Regex.IsMatch(ele.Value.ToString(), @"\w{3},\s[0-3][0-9]\s\w{3,9}\s\d{4}\s[0-2][0-9]:[0-5][0-9]:[0-5][0-9]\sGMT")), "Header: 'Date' is missing or value doesn't match. Please check!");
                // Body
                Assert.IsEmpty(response.Content, "Received a reponse body, it should be EMPTY. Please check!");
            });
        }

        [Test]
        public void St_45748_GETAWAY_AnAlertCantBeUpdatedViaPUTcall()
        {
            // Arrange
            var requestUrl = new GetawayProxy().AllAlerts();
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            { "origin",  OriginHeaderValue }
                                        };
            var requestBody = "{\"id\":\"0eda11f0-415c-0cf4-1c7d-cf0c00868390\",\"group\":7,\"closed\":false,\"critical\":false,\"advisory\":false,\"mode\":0,\"routeId\":\"\",\"routeLongName\":\"\",\"stationId\":\"\",\"stationName\":\"\",\"alertLifecycle\":\"NEW\",\"certainty\":\"UNKNOWN\",\"effect\":\"ADVISORY\",\"endStamp\":0,\"startTime\":\"2020-04-01T21:45:26Z\",\"endTime\":null,\"alertText\":\"Technical Service Alert\",\"header\":\"Due to the physical distancing required by health authorities in BC, bus operators will not be able to assist our wheelchair customers with being strapped in a front-facing position on our conventional buses.\",\"description\":\"Those with mobility devices on buses that have a rear facing accessible seat will be able to park in the designated area themselves and secure their device. This is the case for most buses. \r\n\r\nFor buses that do not have this area (e.g. highway coaches, Community Shuttle) customers with mobility devices will need to travel with someone who can assist them or find an alternate mode of travel.\",\"url\":\"\",\"lastModified\":\"2020-04-10T21:34:22Z\"}";

            // Act
            var response = new ApiActionsLibrary().PerformPostRequest(requestUrl, requestHeaders, requestBody);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(404, (int)response.StatusCode, "Status code is not equal to 404. Please check!");
                // Headers
                Assert.True(response.Headers.Any(ele => ele.Name == "Access-Control-Allow-Origin" && ele.Value.ToString() == "*"), "Header: 'Access-Control-Allow-Origin' is missing or value doesn't macth. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Date" && Regex.IsMatch(ele.Value.ToString(), @"\w{3},\s[0-3][0-9]\s\w{3,9}\s\d{4}\s[0-2][0-9]:[0-5][0-9]:[0-5][0-9]\sGMT")), "Header: 'Date' is missing or value doesn't match. Please check!");
                // Body
                Assert.IsEmpty(response.Content, "Received a reponse body, it should be EMPTY. Please check!");
            });
        }

        [Test]
        public void St_45748_GETAWAY_AnAlertCantBeDeletedViaDELETEcall()
        {
            // Arrange
            var requestUrl = new GetawayProxy().AllAlerts();
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            { "origin",  OriginHeaderValue }
                                        };

            // Act
            var response = new ApiActionsLibrary().PerformPostRequest(requestUrl, requestHeaders, null);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(404, (int)response.StatusCode, "Status code is not equal to 404. Please check!");
                // Headers
                Assert.True(response.Headers.Any(ele => ele.Name == "Access-Control-Allow-Origin" && ele.Value.ToString() == "*"), "Header: 'Access-Control-Allow-Origin' is missing or value doesn't macth. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Date" && Regex.IsMatch(ele.Value.ToString(), @"\w{3},\s[0-3][0-9]\s\w{3,9}\s\d{4}\s[0-2][0-9]:[0-5][0-9]:[0-5][0-9]\sGMT")), "Header: 'Date' is missing or value doesn't match. Please check!");
                // Body
                Assert.IsEmpty(response.Content, "Received a reponse body, it should be EMPTY. Please check!");
            });
        }
    }
}
