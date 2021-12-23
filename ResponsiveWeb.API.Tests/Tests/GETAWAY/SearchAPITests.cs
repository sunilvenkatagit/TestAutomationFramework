using NUnit.Framework;
using AutomationFramework.Libraries;
using ResponsiveWeb.API.Configuration.TestConfig;
using ResponsiveWeb.API.Models.Request;
using ResponsiveWeb.API.Models.Response;
using ResponsiveWeb.API.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static ResponsiveWeb.API.Models.Request.SearchAPIRequestModel;
using static ResponsiveWeb.API.Data.TestData;
using RestSharp;
using AventStack.ExtentReports;
using AutomationFramework.Configuration.ReportConfig;

namespace ResponsiveWeb.API.Tests
{
    [TestFixture]
    [Category("Getaway")]
    [Category("All")]
    public class SearchAPITests : BaseTest
    {
        [Test]
        public void St_45747_ShouldBeAbleToSearchForAterm()
        {
            // Arrange
            var requestUrl = new GetawayProxy().SearchAPI();
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            { "origin",  OriginHeaderValue }
                                        };
            var requestBody = new SearchAPIRequestModel()
            {
                DisplayCount = 5,
                SearchQuery = "bylaw"
            };

            // Act
            var response = new ApiActionsLibrary().PerformPostRequest<SearchAPIResponseModel>(requestUrl, requestHeaders, requestBody);

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
                    Assert.True(response.Headers.Any(ele => ele.Name == "Cache-Control" && ele.Value.ToString() == "private, no-cache"), "Header: 'Cache-Control' is missing or value doesn't match. Please check!");
                    Assert.True(response.Headers.Any(ele => ele.Name == "Content-Type" && ele.Value.ToString() == "application/json; charset=utf-8"), "Header: 'Content-Type' is missing or value doesn't match. Please check!");
                    Assert.True(response.Headers.Any(ele => ele.Name == "timing-allow-origin" && ele.Value.ToString().Length > 10), "Header: 'timing-allow-origin' is missing or value doesn't match. Please check!");
                    Assert.True(response.Headers.Any(ele => ele.Name == "Date" && Regex.IsMatch(ele.Value.ToString(), @"\w{3},\s[0-3][0-9]\s\w{3,9}\s\d{4}\s[0-2][0-9]:[0-5][0-9]:[0-5][0-9]\sGMT")), "Header: 'Date' is missing or value doesn't match. Please check!");
                    // Body
                    Assert.IsNotEmpty(response.Content, "Received an EMPTY response.");
                    Assert.IsNull(response.Data.ErrorMessage, "Error message should be NULL, but its not. Please check!");
                    Assert.True(response.Data.Count > 0, "Count is NOT greater than 0. Please check!");
                    Assert.True(response.Data.CurrentPage == 1, "CurrentPage value is NOT 1. Please check!");
                    Assert.True(response.Data.Values.Count == 5, $"Search result count is NOT equqal to the specified value {(5)}. Please check!");
                    for (int index = 0; index < response.Data.Values.Count; index++)
                    {
                        Assert.IsNotEmpty(response.Data.Values[index].Title, $"Result title is EMPTY at index { index }");
                        Assert.IsNotNull(response.Data.Values[index].Abstract, $"Abstract is NULL at index { index }");
                        Assert.IsNotEmpty(response.Data.Values[index].Url, $"Url is EMPTY at index { index }");
                    }
                    Assert.True(Regex.IsMatch(response.Data.DurationMilliseconds.ToString(), @"^([^0]\d{0,3})(\.?)(\d*)$"), "Took longer than expected or Invalid time format. Please check!");
                });
        }

        [Test]
        public void St_45747_ValidateNoSearchResults()
        {
            // Arrange
            var requestUrl = new GetawayProxy().SearchAPI();
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            { "origin",  OriginHeaderValue }
                                        };
            var requestBody = new SearchAPIRequestModel()
            {
                SearchQuery = "GHg2#%$^",
            };

            // Act
            var response = new ApiActionsLibrary().PerformPostRequest<SearchAPIResponseModel>(requestUrl, requestHeaders, requestBody);

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
                Assert.True(response.Headers.Any(ele => ele.Name == "Cache-Control" && ele.Value.ToString() == "private, no-cache"), "Header: 'Cache-Control' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Content-Type" && ele.Value.ToString() == "application/json; charset=utf-8"), "Header: 'Content-Type' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "timing-allow-origin" && ele.Value.ToString().Length > 10), "Header: 'timing-allow-origin' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Date" && Regex.IsMatch(ele.Value.ToString(), @"\w{3},\s[0-3][0-9]\s\w{3,9}\s\d{4}\s[0-2][0-9]:[0-5][0-9]:[0-5][0-9]\sGMT")), "Header: 'Date' is missing or value doesn't match. Please check!");
                // Body
                Assert.IsNotEmpty(response.Content, "Received an EMPTY response.");
                Assert.IsNull(response.Data.ErrorMessage, "Error message should be NULL, but its not. Please check!");
                Assert.True(response.Data.Count == 0, "Count is NOT equal to 0. Please check!");
                Assert.True(response.Data.CurrentPage == 1, "CurrentPage value is NOT 1. Please check!");
                Assert.True(response.Data.Values == null, $"Values is NOT null. Please check!");
                Assert.True(Regex.IsMatch(response.Data.DurationMilliseconds.ToString(), @"^([^0]\d{0,3})(\.?)(\d*)$"), "Took longer than expected or Invalid time format. Please check!");
            });
        }

        [Test]
        public void St_45747_SearchResultsCanBeFilteredByYear()
        {
            // Arrange
            var requestUrl = new GetawayProxy().SearchAPI();
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            { "origin",  OriginHeaderValue }
                                        };
            var requestBody = new SearchAPIRequestModel()
            {
                SearchQuery = "",
                Facets = new List<Facet>()
                                    {
                                        {
                                            new Facet()
                                                    {
                                                        FieldName = "computeditemdateyearfield",
                                                        SortOrder = "asc",
                                                        SelectedValues = new List<object>() { "2021"},
                                                        FieldType = "String"
                                                    }
                                        } }
            };

            // Act
            var response = new ApiActionsLibrary().PerformPostRequest<SearchAPIResponseModel>(requestUrl, requestHeaders, requestBody);

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
                Assert.True(response.Headers.Any(ele => ele.Name == "Cache-Control" && ele.Value.ToString() == "private, no-cache"), "Header: 'Cache-Control' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Content-Type" && ele.Value.ToString() == "application/json; charset=utf-8"), "Header: 'Content-Type' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "timing-allow-origin" && ele.Value.ToString().Length > 10), "Header: 'timing-allow-origin' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Date" && Regex.IsMatch(ele.Value.ToString(), @"\w{3},\s[0-3][0-9]\s\w{3,9}\s\d{4}\s[0-2][0-9]:[0-5][0-9]:[0-5][0-9]\sGMT")), "Header: 'Date' is missing or value doesn't match. Please check!");
                // Body
                Assert.IsNotEmpty(response.Content, "Received an EMPTY response.");
                for (int index = 0; index < response.Data.Values.Count; index++)
                {
                    Assert.IsNotEmpty(response.Data.Values[index].Title, $"Result title is EMPTY at index { index }");
                    Assert.IsNotNull(response.Data.Values[index].Abstract, $"Abstract is NULL at index { index }");
                    Assert.True(response.Data.Values[index].Date.EndsWith("2021"), $"Result item at index { index } is not from 2021. Please check!");
                    Assert.IsNotEmpty(response.Data.Values[index].Url, $"Url is EMPTY at index { index }");
                }
                Assert.IsNull(response.Data.ErrorMessage, "Error message should be NULL, but its not. Please check!");
                Assert.True(response.Data.Values.Count == 10, $"Search result count is NOT equal to the 10 by default. Please check!");
                Assert.True(response.Data.CurrentPage == 1, "CurrentPage value is NOT 1. Please check!");
                Assert.True(Regex.IsMatch(response.Data.DurationMilliseconds.ToString(), @"^([^0]\d{0,3})(\.?)(\d*)$"), "Took longer than expected or Invalid time format. Please check!");
            });
        }

        [Test]
        public void St_45747_SearchResultsCanBeFilteredByApplyingMultipleFilters()
        {
            /* *
             * Filter values used are ...
             *      1. 2020
             *      2. Buzzer Blog Post
             *      3. Media Release
             * */

            // Arrange
            var requestUrl = new GetawayProxy().SearchAPI();
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            { "origin",  OriginHeaderValue }
                                        };
            var requestBody = new SearchAPIRequestModel()
            {
                DisplayCount = 100,
                SearchQuery = "",
                Facets = new List<Facet>()
                                    {
                                        {
                                            new Facet()
                                                    {
                                                        FieldName = "computeditemdateyearfield",
                                                        SortOrder = "asc",
                                                        SelectedValues = new List<object>(){"2020"},
                                                        FieldType = "String"
                                                    }
                                        },
                                        {
                                            new Facet()
                                                    {
                                                         FieldName = "computedcontenttypefield",
                                                         SortOrder = "asc",
                                                         SelectedValues = new List<object>(){"Buzzer Blog Post","Media Release"},
                                                         FieldType = "String"
                                                     }
                                        } }
            };

            // Act
            var response = new ApiActionsLibrary().PerformPostRequest<SearchAPIResponseModel>(requestUrl, requestHeaders, requestBody);

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
                Assert.True(response.Headers.Any(ele => ele.Name == "Cache-Control" && ele.Value.ToString() == "private, no-cache"), "Header: 'Cache-Control' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Content-Type" && ele.Value.ToString() == "application/json; charset=utf-8"), "Header: 'Content-Type' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "timing-allow-origin" && ele.Value.ToString().Length > 10), "Header: 'timing-allow-origin' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Date" && Regex.IsMatch(ele.Value.ToString(), @"\w{3},\s[0-3][0-9]\s\w{3,9}\s\d{4}\s[0-2][0-9]:[0-5][0-9]:[0-5][0-9]\sGMT")), "Header: 'Date' is missing or value doesn't match. Please check!");
                // Body
                Assert.IsNotEmpty(response.Content, "Received an EMPTY response.");
                Assert.IsNull(response.Data.ErrorMessage, "Error message should be NULL, but its not. Please check!");
                Assert.True(response.Data.CurrentPage == 1, "CurrentPage value is NOT 1. Please check!");
                for (int index = 0; index < response.Data.Values.Count; index++)
                {
                    Assert.IsNotEmpty(response.Data.Values[index].Title, $"Result title is EMPTY at index { index }");
                    Assert.IsNotNull(response.Data.Values[index].Abstract, $"Abstract is NULL at index { index }");
                    Assert.True(response.Data.Values[index].Date.EndsWith("2020"), $"Result item at index { index } is not from 2020. Please check!");
                    Assert.True((response.Data.Values[index].ItemType == "Media Release" | response.Data.Values[index].ItemType == "Buzzer Blog Post"), $"ItemType is not as expected at index { index }");
                    Assert.IsNotEmpty(response.Data.Values[index].Url, $"Url is EMPTY at index { index }");
                }
                Assert.True(Regex.IsMatch(response.Data.DurationMilliseconds.ToString(), @"^([^0]\d{0,3})(\.?)(\d*)$"), "Took longer than expected or Invalid time format. Please check!");
            });
        }

        [Test]
        public void St_45747_ValidateThatNoResultsAreFoundForInvalidFilters()
        {
            /* *
             * Invalid ContentType filter values are passed (Ex: Movie, Song) ...
             * */

            // Arrange
            var requestUrl = new GetawayProxy().SearchAPI();
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            { "origin",  OriginHeaderValue }
                                        };
            var requestBody = new SearchAPIRequestModel()
            {
                DisplayCount = 100,
                SearchQuery = "",
                Facets = new List<Facet>()
                                    {
                                        {
                                            new Facet()
                                                    {
                                                         FieldName = "computedcontenttypefield",
                                                         SortOrder = "asc",
                                                         SelectedValues = new List<object>(){"Movie","Song"},
                                                         FieldType = "String"
                                                     }
                                        } }
            };

            // Act
            var response = new ApiActionsLibrary().PerformPostRequest<SearchAPIResponseModel>(requestUrl, requestHeaders, requestBody);

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
                Assert.True(response.Headers.Any(ele => ele.Name == "Cache-Control" && ele.Value.ToString() == "private, no-cache"), "Header: 'Cache-Control' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Content-Type" && ele.Value.ToString() == "application/json; charset=utf-8"), "Header: 'Content-Type' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "timing-allow-origin" && ele.Value.ToString().Length > 10), "Header: 'timing-allow-origin' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Date" && Regex.IsMatch(ele.Value.ToString(), @"\w{3},\s[0-3][0-9]\s\w{3,9}\s\d{4}\s[0-2][0-9]:[0-5][0-9]:[0-5][0-9]\sGMT")), "Header: 'Date' is missing or value doesn't match. Please check!");
                // Body
                Assert.IsNotEmpty(response.Content, "Received an EMPTY response.");
                Assert.IsNull(response.Data.ErrorMessage, "Error message should be NULL, but its not. Please check!");
                Assert.True(response.Data.Count == 0, "Count is NOT equal to 0. Please check!");
                Assert.True(response.Data.CurrentPage == 1, "CurrentPage value is NOT 1. Please check!");
                Assert.True(response.Data.Values == null, $"Values is NOT null. Please check!");
                Assert.True(Regex.IsMatch(response.Data.DurationMilliseconds.ToString(), @"^([^0]\d{0,3})(\.?)(\d*)$"), "Took longer than expected or Invalid time format. Please check!");
            });
        }

        [Test]
        public void St_45747_500InternalErrorIsReceived_WhenAnInvalidRequestBodyIsSent()
        {
            // Arrange
            var requestUrl = new GetawayProxy().SearchAPI();
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            { "origin",  OriginHeaderValue }
                                        };
            var requestBody = "{\"randomKey\": \"randomValue\"}";

            // Act
            var response = new ApiActionsLibrary().PerformPostRequest<SearchAPIResponseModel>(requestUrl, requestHeaders, requestBody);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(500, (int)response.StatusCode, "Status code is NOT 500. Please check!");
                // Headers
                Assert.True(response.Headers.Any(ele => ele.Name == "strict-transport-security" && Regex.IsMatch(ele.Value.ToString(), @"max-age=[\d]+$")), "Header: 'strict-transport-security' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "access-control-expose-headers" && ele.Value.ToString() == "*"), "Header: 'access-control-expose-headers' is missing or value doesn't macth. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Access-Control-Allow-Origin" && ele.Value.ToString() == "*"), "Header: 'Access-Control-Allow-Origin' is missing or value doesn't macth. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Cache-Control" && ele.Value.ToString() == "private, no-cache"), "Header: 'Cache-Control' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "timing-allow-origin" && ele.Value.ToString().Length > 10), "Header: 'timing-allow-origin' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Date" && Regex.IsMatch(ele.Value.ToString(), @"\w{3},\s[0-3][0-9]\s\w{3,9}\s\d{4}\s[0-2][0-9]:[0-5][0-9]:[0-5][0-9]\sGMT")), "Header: 'Date' is missing or value doesn't match. Please check!");
                // Body
                Assert.IsEmpty(response.Content, "Received a response body. It should be EMPTY. Please check!");
            });
        }

        [Test]
        public void St_45747_500InternalErrorIsReceived_WhenNoRequestBodyIsSent()
        {
            // Arrange
            var requestUrl = new GetawayProxy().SearchAPI();
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            { "origin",  OriginHeaderValue }
                                        };

            // Act
            var response = new ApiActionsLibrary().PerformPostRequest<SearchAPIResponseModel>(requestUrl, requestHeaders, null);

            WriteResposeToReport(response);

            // Assert
            Assert.Multiple(() =>
            {
                // Status
                Assert.AreEqual(500, (int)response.StatusCode, "Status code is NOT 500. Please check!");
                // Headers
                Assert.True(response.Headers.Any(ele => ele.Name == "strict-transport-security" && Regex.IsMatch(ele.Value.ToString(), @"max-age=[\d]+$")), "Header: 'strict-transport-security' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "access-control-expose-headers" && ele.Value.ToString() == "*"), "Header: 'access-control-expose-headers' is missing or value doesn't macth. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Access-Control-Allow-Origin" && ele.Value.ToString() == "*"), "Header: 'Access-Control-Allow-Origin' is missing or value doesn't macth. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Cache-Control" && ele.Value.ToString() == "private, no-cache"), "Header: 'Cache-Control' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "timing-allow-origin" && ele.Value.ToString().Length > 10), "Header: 'timing-allow-origin' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Date" && Regex.IsMatch(ele.Value.ToString(), @"\w{3},\s[0-3][0-9]\s\w{3,9}\s\d{4}\s[0-2][0-9]:[0-5][0-9]:[0-5][0-9]\sGMT")), "Header: 'Date' is missing or value doesn't match. Please check!");
                // Body
                Assert.IsEmpty(response.Content, "Received a response body. It should be EMPTY. Please check!");
            });
        }

        [Test]
        public void St_45747_404NotFoundForOtherHttpMethods()
        {
            var httpMethods = new List<string> { "GET", "PUT", "DELETE" };

            foreach (var method in httpMethods)
            {
                // Arrange
                var requestUrl = new GetawayProxy().SearchAPI();
                var requestHeaders = new Dictionary<string, string>()
                                        {
                                            { "origin",  OriginHeaderValue }
                                        };
                var requestBody = new SearchAPIRequestModel()
                {
                    DisplayCount = 5,
                    SearchQuery = "bylaw"
                };

                // Act
                IRestResponse response = null;
                ExtentManager.GetExtentTest().Log(Status.Info, $"<b>--- Http Method: {method} ---</b>");
                switch (method)
                {
                    case "GET":
                        response = new ApiActionsLibrary().PerformGetRequest<SearchAPIResponseModel>(requestUrl, requestHeaders);
                        break;
                    case "PUT":
                        response = new ApiActionsLibrary().PerformPutRequest<SearchAPIResponseModel>(requestUrl, requestHeaders, requestBody);
                        break;
                    case "DELETE":
                        response = new ApiActionsLibrary().PerformDeleteRequest(requestUrl, requestHeaders, requestBody);
                        break;
                }

                WriteResposeToReport(response);

                // Assert
                Assert.Multiple(() =>
                {
                    // Status
                    Assert.AreEqual(404, (int)response.StatusCode, "Status code is NOT 404. Please check!");
                    // Headers
                    Assert.True(response.Headers.Any(ele => ele.Name == "Access-Control-Allow-Origin" && ele.Value.ToString() == "*"), "Header: 'Access-Control-Allow-Origin' is missing or value doesn't macth. Please check!");
                    Assert.True(response.Headers.Any(ele => ele.Name == "Date" && Regex.IsMatch(ele.Value.ToString(), @"\w{3},\s[0-3][0-9]\s\w{3,9}\s\d{4}\s[0-2][0-9]:[0-5][0-9]:[0-5][0-9]\sGMT")), "Header: 'Date' is missing or value doesn't match. Please check!");
                    // Body
                    Assert.IsEmpty(response.Content, "Received a response body. It should be EMPTY. Please check!");
                });
            }
        }

        [Test]
        public void St_42386_SearchResultsCanBeFilteredByPDF()
        {
            // Arrange
            var requestUrl = new GetawayProxy().SearchAPI();
            var requestHeaders = new Dictionary<string, string>()
                                        {
                                            { "origin",  OriginHeaderValue }
                                        };
            var requestBody = new SearchAPIRequestModel()
            {
                SearchQuery = "",
                DisplayCount = 10,
                Facets = new List<Facet>()
                                    {
                                        {
                                            new Facet()
                                                    {
                                                        FieldName = "computedcontenttypefield",
                                                        SortOrder = "asc",
                                                        SelectedValues = new List<object>() {"PDF"},
                                                        FieldType = "String"
                                                    }
                                        } }
            };

            // Act
            var response = new ApiActionsLibrary().PerformPostRequest<SearchAPIResponseModel>(requestUrl, requestHeaders, requestBody);

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
                Assert.True(response.Headers.Any(ele => ele.Name == "Cache-Control" && ele.Value.ToString() == "private, no-cache"), "Header: 'Cache-Control' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Content-Type" && ele.Value.ToString() == "application/json; charset=utf-8"), "Header: 'Content-Type' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "timing-allow-origin" && ele.Value.ToString().Length > 10), "Header: 'timing-allow-origin' is missing or value doesn't match. Please check!");
                Assert.True(response.Headers.Any(ele => ele.Name == "Date" && Regex.IsMatch(ele.Value.ToString(), @"\w{3},\s[0-3][0-9]\s\w{3,9}\s\d{4}\s[0-2][0-9]:[0-5][0-9]:[0-5][0-9]\sGMT")), "Header: 'Date' is missing or value doesn't match. Please check!");
                // Body
                Assert.IsNotEmpty(response.Content, "Received an EMPTY response.");
                 for (int index = 0; index < response.Data.Values.Count; index++)
                 {
                    Assert.IsNotEmpty(response.Data.Values[index].Title, $"Result title is EMPTY at index { index }");
                    Assert.IsNotNull(response.Data.Values[index].Abstract, $"Abstract is NULL at index { index }");
                    Assert.IsNotEmpty(response.Data.Values[index].DownloadUrl, $"Result downloadUrl is EMPTY at index { index }");
                    Assert.IsNotEmpty(response.Data.Values[index].ResourceSize, $"Result resourceSize is EMPTY at index { index }");

                 }
                Assert.IsNull(response.Data.ErrorMessage, "Error message should be NULL, but its not. Please check!");
                Assert.True(response.Data.CurrentPage == 1, "CurrentPage value is NOT 1. Please check!");
                Assert.True(Regex.IsMatch(response.Data.DurationMilliseconds.ToString(), @"^([^0]\d{0,3})(\.?)(\d*)$"), "Took longer than expected or Invalid time format. Please check!");
            });
        }

    }
}
