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
    [Category("Getaway")]
    [Category("All")]
    public class SitenavTests : BaseTest
    {
        [Test]
        public void St_45800_SiteNavApi_MainNavigation()
        {
            // Arrange
            var requestUrl = new GetawayProxy().SiteNav("7CB6E8FC-7592-4701-A2EB-448805B8E75A", "59f9fef8-f2d7-4212-87f3-5cf0adbb6ce8");
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            { "origin",  OriginHeaderValue }
                                        };
            // Act
            var response = new ApiActionsLibrary().PerformGetRequest<List<SiteNavModel>>(requestUrl, requestHeaders);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(200, (int)response.StatusCode, "Status code is NOT equal to 200");
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
                for (int menuItem = 0; menuItem < response.Data.Count; menuItem++)
                {
                    Assert.True(new Uri(response.Data[menuItem].MenuItemLinkUrl).IsAbsoluteUri, $"MenuItemLinkUrl is NOT a valid URL or empty at index { menuItem }.");
                    Assert.IsNotEmpty(response.Data[menuItem].MenuItemName, $"MenuItemName is EMPTY at index { menuItem }.");
                    Assert.True(response.Data[menuItem].MenuItemTabTarget.GetType() == typeof(bool), $"MenuItemTabTarget is NOT a boolean value at index { menuItem }.");

                    if (response.Data[menuItem].Children.Count > 0)
                    {
                        response.Data[menuItem].Children.ForEach(subItem =>
                            {
                                Assert.True(new Uri(subItem.MenuItemLinkUrl).IsAbsoluteUri, $"MenuItemLinkUrl (Children array) is NOT a valid URL or empty for { subItem.MenuItemName }.");
                                Assert.IsNotEmpty(subItem.MenuItemName, $"MenuItemName (Children array) is EMPTY at index { menuItem }.");
                                Assert.True(subItem.MenuItemTabTarget.GetType() == typeof(bool), $"MenuItemTabTarget (Children array) is NOT a boolean value at index { subItem.MenuItemName }.");
                            });
                    }
                }
            });
        }

        [Test]
        public void St_45800_SiteNavApi_SuperNavigation()
        {
            // Arrange
            var requestUrl = new GetawayProxy().SiteNav("5DD4374A-9C87-46AF-AC35-630B7C4A2299", "59f9fef8-f2d7-4212-87f3-5cf0adbb6ce8");
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            { "origin",  OriginHeaderValue }
                                        };
            // Act
            var response = new ApiActionsLibrary().PerformGetRequest<List<SiteNavModel>>(requestUrl, requestHeaders);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(200, (int)response.StatusCode, "Status code is NOT equal to 200");
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
                for (int index = 0; index < response.Data.Count; index++)
                {
                    Assert.True(new Uri(response.Data[index].MenuItemLinkUrl).IsAbsoluteUri, $"MenuItemLinkUrl is NOT absolute or empty at index { index }.");
                    Assert.IsNotEmpty(response.Data[index].MenuItemName, $"MenuItemName is EMPTY at index { index }.");
                    Assert.True(response.Data[index].MenuItemName.GetType() == typeof(string), $"MenuItemName is NOT a string value at index { index }.");
                    Assert.True(response.Data[index].MenuItemTabTarget.GetType() == typeof(bool), $"MenuItemTabTarget is NOT a boolean value at index { index }.");
                }
            });
        }

        [Test]
        public void St_45800_SiteNavApi_AccountNavigation()
        {
            // Arrange
            var requestUrl = new GetawayProxy().SiteNav("157F6AD2-047C-4F98-BB45-86461DF0F494", "59f9fef8-f2d7-4212-87f3-5cf0adbb6ce8");
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            { "origin",  OriginHeaderValue }
                                        };
            // Act
            var response = new ApiActionsLibrary().PerformGetRequest<List<SiteNavModel>>(requestUrl, requestHeaders);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(200, (int)response.StatusCode, "Status code is NOT equal to 200");
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
                for (int index = 0; index < response.Data.Count; index++)
                {
                    Assert.True(new Uri(response.Data[index].MenuItemLinkUrl).IsAbsoluteUri, $"MenuItemLinkUrl is NOT absolute or empty at index { index }.");
                    Assert.IsNotEmpty(response.Data[index].MenuItemName, $"MenuItemName is EMPTY at index { index }.");
                    Assert.True(response.Data[index].MenuItemName.GetType() == typeof(string), $"MenuItemName is NOT a string value at index { index }.");
                    Assert.True(response.Data[index].MenuItemTabTarget.GetType() == typeof(bool), $"MenuItemTabTarget is NOT a boolean value at index { index }.");
                }
            });
        }

        [Test]
        public void St_45800_SiteNavApi_SocialMediaLinks()
        {
            // Arrange
            var requestUrl = new GetawayProxy().SiteNav("23110991-4ADD-4011-8C17-02C17CC31BB7", "59f9fef8-f2d7-4212-87f3-5cf0adbb6ce8");
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            { "origin",  OriginHeaderValue }
                                        };
            // Act
            var response = new ApiActionsLibrary().PerformGetRequest<List<SiteNavModel>>(requestUrl, requestHeaders);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(200, (int)response.StatusCode, "Status code is NOT equal to 200");
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
                for (int i = 0; i < response.Data.Count; i++)
                {
                    Assert.True(new Uri(response.Data[i].MenuItemLinkUrl).IsAbsoluteUri, $"MenuItemLinkUrl is NOT absolute or empty at index { i }.");
                    Assert.IsNotEmpty(response.Data[i].MenuItemName, $"MenuItemName is EMPTY at index { i }.");
                    Assert.True(response.Data[i].MenuItemName.GetType() == typeof(string), $"MenuItemName is NOT a string value at index { i }.");
                    Assert.True(response.Data[i].MenuItemTabTarget.GetType() == typeof(bool), $"MenuItemTabTarget is NOT a boolean value at index { i }.");
                    Assert.True(new Uri(response.Data[i].MenuItemTabImageUrl).IsAbsoluteUri, $"MenuItemTabImageUrl is NOT absolute at index { i }.");
                    Assert.IsNotEmpty(response.Data[i].MenuItemTabImageAlt, $"MenuItemTabImageAlt is EMPTY at index { i }.");
                }
            });
        }

        [Test]
        public void St_45800_SiteNavApi_GettingAround()
        {
            // Arrange
            var requestUrl = new GetawayProxy().SiteNav("45CC2AC1-1D25-4C13-97E8-3F78A4E49149", "59f9fef8-f2d7-4212-87f3-5cf0adbb6ce8");
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            { "origin",  OriginHeaderValue }
                                        };
            // Act
            var response = new ApiActionsLibrary().PerformGetRequest<List<SiteNavModel>>(requestUrl, requestHeaders);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(200, (int)response.StatusCode, "Status code is NOT equal to 200");
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
                for (int index = 0; index < response.Data.Count; index++)
                {
                    Assert.True(new Uri(response.Data[index].MenuItemLinkUrl).IsAbsoluteUri, $"MenuItemLinkUrl is NOT absolute or empty at index { index }.");
                    Assert.IsNotEmpty(response.Data[index].MenuItemName, $"MenuItemName is EMPTY at index { index }.");
                    Assert.True(response.Data[index].MenuItemName.GetType() == typeof(string), $"MenuItemName is NOT a string value at index { index }.");
                    Assert.True(response.Data[index].MenuItemTabTarget.GetType() == typeof(bool), $"MenuItemTabTarget is NOT a boolean value at index { index }.");
                }
            });
        }

        [Test]
        public void St_45800_SiteNavApi_Resources()
        {
            // Arrange
            var requestUrl = new GetawayProxy().SiteNav("787D53DC-2D19-45E2-9A56-B9E330C9D5B1", "59f9fef8-f2d7-4212-87f3-5cf0adbb6ce8");
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            { "origin",  OriginHeaderValue }
                                        };
            // Act
            var response = new ApiActionsLibrary().PerformGetRequest<List<SiteNavModel>>(requestUrl, requestHeaders);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(200, (int)response.StatusCode, "Status code is NOT equal to 200");
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
                for (int index = 0; index < response.Data.Count; index++)
                {
                    bool IsAbsolute()
                    {
                        if (String.IsNullOrEmpty(response.Data[index].MenuItemLinkUrl))
                            return true;
                        return new Uri(response.Data[index].MenuItemLinkUrl).IsAbsoluteUri;
                    };

                    Assert.True(IsAbsolute(), $"MenuItemLinkUrl is NOT absolute at index { index }.");
                    Assert.IsNotEmpty(response.Data[index].MenuItemName, $"MenuItemName is EMPTY at index { index }.");
                    Assert.True(response.Data[index].MenuItemName.GetType() == typeof(string), $"MenuItemName is NOT a string value at index { index }.");
                    Assert.True(response.Data[index].MenuItemTabTarget.GetType() == typeof(bool), $"MenuItemTabTarget is NOT a boolean value at index { index }.");
                }
            });
        }

        [Test]
        public void St_45800_SiteNavApi_ContactUs()
        {
            // Arrange
            var requestUrl = new GetawayProxy().SiteNav("93363F24-AE0E-4528-B2F0-0ED4797CDCBA", "59f9fef8-f2d7-4212-87f3-5cf0adbb6ce8");
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            { "origin",  OriginHeaderValue }
                                        };
            // Act
            var response = new ApiActionsLibrary().PerformGetRequest<List<SiteNavModel>>(requestUrl, requestHeaders);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(200, (int)response.StatusCode, "Status code is NOT equal to 200");
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
                Assert.True(response.Data.Any(ele => ele.MenuItemLinkUrl.Contains("google") && new Uri(ele.MenuItemLinkUrl).IsAbsoluteUri), "Google maps address link is NOT a valid URL.");
                Assert.True(response.Data.Any(ele => ele.MenuItemName.Equals("400-287 Nelson's Ct. New Westminster BC V3L0E7")), "Translink Addrress doesn't match. Please check!");
                for (int i = 0; i < response.Data.Count; i++)
                {
                    Assert.IsNotEmpty(response.Data[i].MenuItemName, $"MenuItemName is EMPTY at index { i }.");
                    Assert.True(response.Data[i].MenuItemName.GetType() == typeof(string), $"MenuItemName is NOT a string value at index { i }.");
                    Assert.True(response.Data[i].MenuItemTabTarget.GetType() == typeof(bool), $"MenuItemTabTarget is NOT a boolean value at index { i }.");
                }
            });
        }

        [TestCase("7CB6E8FC-7592-4701-A2EB-448805B8E75A", "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx", TestName = "St_45800_TestInvalidApiToken")]
        [TestCase("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx", "59f9fef8-f2d7-4212-87f3-5cf0adbb6ce8", TestName = "St_45800_TestInvalidMenuId")]
        [TestCase("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx", "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx", TestName = "St_45800_TestInvalidMenuIdAndApiToken")]
        public void Test_InvalidSiteNav_MenuIdAndApiToken(string menuId, string apiToken)
        {
            // Arrange
            var requestUrl = new GetawayProxy().SiteNav(menuId, apiToken);
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            { "origin",  OriginHeaderValue }
                                        };
            // Act
            var response = new ApiActionsLibrary().PerformGetRequest<SiteNavErrorModel>(requestUrl, requestHeaders);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(200, (int)response.StatusCode, "Status code is NOT equal to 200");
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
                Assert.IsNotEmpty(response.Data.Error, "There is no error message or doesn't match. Please check!");
            });
        }

        [Test]
        public void St_45800_SiteNavApi_ClearCache()
        {
            // Arrange
            var requestUrl = new GetawayProxy().SiteNavCacheReset(SiteNavCacheApiKey);
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            { "origin",  OriginHeaderValue }
                                        };

            // Act
            var response = new ApiActionsLibrary().PerformDeleteRequest(requestUrl, requestHeaders, null);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(200, (int)response.StatusCode, "Status code is NOT equal to 200");
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
                Assert.True(response.Content.Contains("Cache reseted."), "Response body message doesn't match. Please check!");
            });
        }
    }
}
