using AutomationFramework.Configuration.ReportConfig;
using AventStack.ExtentReports;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;


namespace AutomationFramework.Libraries
{
    public class ApiActionsLibrary
    {
        #region - HTTP Methods
        // ===> Method: Get
        public IRestResponse PerformGetRequest(string requestUrl, Dictionary<string, string> requestHeaders, bool logToReport = true)
        {
            IRestRequest restRequest = PrepareRestRequest(requestUrl, requestHeaders, Method.GET, null, logToReport);
            IRestResponse restResponse = SendRequest(restRequest);
            return restResponse;
        }
        public IRestResponse<T> PerformGetRequest<T>(string requestUrl, Dictionary<string, string> requestHeaders, bool logToReport = true) where T : new()
        {
            IRestRequest restRequest = PrepareRestRequest(requestUrl, requestHeaders, Method.GET, null, logToReport);
            IRestResponse<T> restResponse = SendRequest<T>(restRequest);
            return restResponse;
        }

        // ===> Method: Post
        public IRestResponse PerformPostRequest(string requestUrl, Dictionary<string, string> requestHeaders, object requestBody, bool logToReport = true)
        {
            IRestRequest restRequest = PrepareRestRequest(requestUrl, requestHeaders, Method.POST, requestBody, logToReport);
            IRestResponse restResponse = SendRequest(restRequest);
            return restResponse;
        }
        public IRestResponse<T> PerformPostRequest<T>(string requestUrl, Dictionary<string, string> requestHeaders, object requestBody, bool logToReport = true) where T : new()
        {
            IRestRequest restRequest = PrepareRestRequest(requestUrl, requestHeaders, Method.POST, requestBody, logToReport);
            IRestResponse<T> restResponse = SendRequest<T>(restRequest);
            return restResponse;
        }

        // ===> Method: Put
        public IRestResponse PerformPutRequest(string requestUrl, Dictionary<string, string> requestHeaders, object requestBody, bool logToReport = true)
        {
            IRestRequest restRequest = PrepareRestRequest(requestUrl, requestHeaders, Method.PUT, requestBody, logToReport);
            IRestResponse restResponse = SendRequest(restRequest);
            return restResponse;
        }
        public IRestResponse<T> PerformPutRequest<T>(string requestUrl, Dictionary<string, string> requestHeaders, object requestBody, bool logToReport = true) where T : new()
        {
            IRestRequest restRequest = PrepareRestRequest(requestUrl, requestHeaders, Method.PUT, requestBody, logToReport);
            IRestResponse<T> restResponse = SendRequest<T>(restRequest);
            return restResponse;
        }

        // ===> Method: Patch
        public IRestResponse PerformPatchRequest(string requestUrl, Dictionary<string, string> requestHeaders, object requestBody, bool logToReport = true)
        {
            IRestRequest restRequest = PrepareRestRequest(requestUrl, requestHeaders, Method.PATCH, requestBody, logToReport);
            IRestResponse restResponse = SendRequest(restRequest);
            return restResponse;
        }
        public IRestResponse<T> PerformPatchRequest<T>(string requestUrl, Dictionary<string, string> requestHeaders, object requestBody, bool logToReport = true) where T : new()
        {
            IRestRequest restRequest = PrepareRestRequest(requestUrl, requestHeaders, Method.PATCH, requestBody, logToReport);
            IRestResponse<T> restResponse = SendRequest<T>(restRequest);
            return restResponse;
        }

        // ===> Method: Delete
        public IRestResponse PerformDeleteRequest(string requestUrl, Dictionary<string, string> requestHeaders, object requestBody, bool logToReport = true)
        {
            IRestRequest restRequest = PrepareRestRequest(requestUrl, requestHeaders, Method.DELETE, requestBody, logToReport);
            IRestResponse restResponse = SendRequest(restRequest);
            return restResponse;
        }
        public IRestResponse<T> PerformDeleteRequest<T>(string requestUrl, Dictionary<string, string> requestHeaders, object requestBody, bool logToReport = true) where T : new()
        {
            IRestRequest restRequest = PrepareRestRequest(requestUrl, requestHeaders, Method.DELETE, requestBody, logToReport);
            IRestResponse<T> restResponse = SendRequest<T>(restRequest);
            return restResponse;
        }
        #endregion

        #region - Local Methods
        private IRestClient PrepareRestClient()
        {
            IRestClient restClient = new RestClient();
            return restClient;
        }
        private IRestRequest PrepareRestRequest(string requestUrl, Dictionary<string, string> requestHeaders, Method methodType, object body, bool logToReport)
        {
            IRestRequest restRequest = new RestRequest()
            {
                Resource = requestUrl,
                Method = methodType
            };

            if (requestHeaders != null)
            {
                foreach (var headerName in requestHeaders.Keys)
                {
                    restRequest.AddHeader(headerName, requestHeaders[headerName]); // {headerName, headerValue}
                }
            }

            if (body != null)
            {
                restRequest.AddJsonBody(body);
            }

            if (logToReport)
            {
                WriteRequestToReport(restRequest, requestHeaders, body);
            }

            return restRequest;
        }
        private void WriteRequestToReport(IRestRequest restRequest, Dictionary<string, string> requestHeaders, object body)
        {
            string headersToReport;
            object bodyToReport = null;

            try
            {
                headersToReport = string.Join("<br/>", requestHeaders.Select(ele => ". " + ele.Key + " = " + ele.Value));
            }
            catch (System.Exception)
            {
                headersToReport = "     --- No Headers Required ---";
            }

            try
            {
                if (!body.GetType().Equals(typeof(string)))
                {
                    // ==> works for class objects
                    bodyToReport = JsonConvert.SerializeObject(body, Formatting.Indented);
                }
                else
                {
                    // ==> for string objects
                    //bodyToReport = JsonConvert.DeserializeObject(body.ToString());                    
                    dynamic parsedJson = JsonConvert.DeserializeObject(body.ToString());
                    bodyToReport = JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
                }

            }
            catch (System.Exception)
            {
                bodyToReport = "     --- No Request Body ---";
            }

            ExtentManager.GetExtentTest().Log(Status.Info, "<b>Request Details</b>");
            ExtentManager.GetExtentTest().Log(Status.Info, $"<pre><b>Request Uri:</b> { restRequest.Resource }<br />" +
                                                           $"<b>Request Method:</b> { restRequest.Method }<br />" +
                                                           $"<b>Request Headers:</b><br />{ headersToReport }<br />" +
                                                           $"<b>Request Body:</b><br />{ bodyToReport }</pre>");
        }
        private IRestResponse SendRequest(IRestRequest restRequest)
        {
            IRestClient restClient = PrepareRestClient();
            IRestResponse restResponse = restClient.ExecuteAsync(restRequest).GetAwaiter().GetResult();

            return restResponse;
        }
        private IRestResponse<T> SendRequest<T>(IRestRequest restRequest) where T : new()
        {
            IRestClient restClient = PrepareRestClient();
            IRestResponse<T> restResponse = restClient.ExecuteAsync<T>(restRequest).GetAwaiter().GetResult();

            if (restResponse.ContentType.Equals("application/xml"))
            {
                var deserializer = new RestSharp.Deserializers.DotNetXmlDeserializer();
                restResponse.Data = deserializer.Deserialize<T>(restResponse);
            }

            return restResponse;
        }
        #endregion
    }
}
