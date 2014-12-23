using System;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Net.Http.Headers;

namespace ArkTrolley.WebService.HttpClientHandler
{
	public class HttpClientController
	{

		private static System.Net.Http.HttpClientHandler GetClientHandler()
		{
			var httpClientHandler = new System.Net.Http.HttpClientHandler ();
			httpClientHandler.Credentials = new System.Net.NetworkCredential ("mobclients", "M03C113n7$");
			return httpClientHandler;
		}
		private static async Task<string> SendHttpRequestForStringResponce(Uri uri, HttpMethod method, Stream requestContent = null, string contentType = null, System.Net.Http.HttpClientHandler clientHandler = null, Dictionary<string, string> headers = null, int timeoutInSeconds = 0)
		{
			var req = clientHandler == null ? new HttpClient() : new HttpClient(clientHandler);
			var message = new HttpRequestMessage(method, uri);
			string response = string.Empty;

			message.Headers.Add("Accept",contentType); // set the content type of the request
			message.Headers.Add("user-agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");

			if (requestContent != null && (method == HttpMethod.Post || method == HttpMethod.Put || method == HttpMethod.Delete))
			{
				message.Content = new StreamContent(requestContent); //set the body for the request

				if (!string.IsNullOrEmpty(contentType))
				{
					message.Content.Headers.Add("Content-Type", contentType); // if the request has a body set the MIME type

				}
			}

			// append additional headers to the request
			if (headers != null)
			{
				foreach (var header in headers)
				{
					if (message.Headers.Contains(header.Key))
					{
						message.Headers.Remove(header.Key);
					}

					message.Headers.Add(header.Key, header.Value);
				}
			}
			// Send the request and read the response as an array of bytes
			if (timeoutInSeconds > 0)
			{
				req.Timeout = new TimeSpan(0, 0, timeoutInSeconds);
			}
			try
			{
				using (var res = await req.SendAsync(message))
				{	response = await res.Content.ReadAsStringAsync();
				}
			}
			catch (Exception ex)
			{


			}
			return response;
		}

		public static async Task<string> PostResponseAsync(string restApi,StringContent httpContent = null)
		{
			if (String.IsNullOrEmpty(restApi))
				throw new InvalidOperationException("Invalid Rest API, rest api must not be empty or null");
			var httpClientHandler = GetClientHandler();
			System.Net.Http.HttpClient httpClient;
			httpClient = new HttpClient (httpClientHandler);
			if (httpClient == null) return "";
			if (httpContent != null)
				httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			HttpResponseMessage response = await httpClient.PostAsync(restApi, httpContent);
			response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			response.EnsureSuccessStatusCode();

			if (!response.IsSuccessStatusCode) throw new InvalidOperationException("Invalid Rest API, response failed");
			var responseContent = await response.Content.ReadAsStringAsync();
			response.Dispose();
			httpClient.Dispose();
			return responseContent;
		}

		public static async Task<string> DeleteResponseAsync(string restApi, string username = "")
		{
			if (String.IsNullOrEmpty(restApi))
				throw new InvalidOperationException("Invalid Rest API, rest api must not be empty or null");

			var httpClientHandler = GetClientHandler();
			System.Net.Http.HttpClient httpClient;
			httpClient = new HttpClient (httpClientHandler);
			if (httpClient == null) return "";
			HttpResponseMessage response = await httpClient.DeleteAsync(restApi);

			if (!response.IsSuccessStatusCode) throw new InvalidOperationException("Invalid Rest API, response failed");
			var responseContent = await response.Content.ReadAsStringAsync();
			response.Dispose();
			httpClient.Dispose();
			return responseContent;
		}

	

		public static async Task<string> PostRequest(string url,FormUrlEncodedContent content)
		{
			var httpClientHandler = new System.Net.Http.HttpClientHandler ();
			httpClientHandler.Credentials = new System.Net.NetworkCredential ("mobclients", "M03C113n7$");

			using (var client = new HttpClient(httpClientHandler))
			{
				client.BaseAddress = new Uri(AppSettings.baseurl);
				var result = client.PostAsync(url, content).Result;
				string resultContent = result.Content.ReadAsStringAsync().Result;

				return resultContent;
			}
		}

		public static async Task<DataBytesDataObject> GetDocument(string url)
		{
			var dataBytesObject = new DataBytesDataObject { DataBytes = null, Response = new ResponseDataObject () };
			try {
				var httpClientHandler = new System.Net.Http.HttpClientHandler ();
				httpClientHandler.Credentials = new System.Net.NetworkCredential ("mobclients", "M03C113n7$");
				dataBytesObject.DataString = await SendHttpRequestForStringResponce (new Uri (url), HttpMethod.Get, null, "application/json; charset=utf-8", httpClientHandler, null);
				dataBytesObject.Response = ((dataBytesObject.DataBytes != null && dataBytesObject.DataBytes.Count () != 0) || !string.IsNullOrEmpty (dataBytesObject.DataString))
					? new ResponseDataObject () { IsSucceeded = true }
					: new ResponseDataObject () { IsSucceeded = false };
			} catch (Exception ex) {
				dataBytesObject.Response = new ResponseDataObject () { IsSucceeded = false };
			}

			return dataBytesObject;
		}

		public static async Task<DataBytesDataObject> PostDocument(string url, StringContent content)
		{
			var dataBytesObject = new DataBytesDataObject { DataBytes = null, Response = new ResponseDataObject () };
			try {
				dataBytesObject.DataString = await PostResponseAsync (url, content);
				dataBytesObject.Response = ((dataBytesObject.DataBytes != null && dataBytesObject.DataBytes.Count () != 0) || !string.IsNullOrEmpty (dataBytesObject.DataString))
					? new ResponseDataObject () { IsSucceeded = true }
					: new ResponseDataObject () { IsSucceeded = false };
			} catch (Exception ex) {
				dataBytesObject.Response = new ResponseDataObject () { IsSucceeded = false };
			}

			return dataBytesObject;
		}

//		public static async Task<DataBytesDataObject> SetDocument(string url)
//		{
//			var dataBytesObject = new DataBytesDataObject { DataBytes = null, Response = new ResponseDataObject () };
//			try {
//				var httpClientHandler = new System.Net.Http.HttpClientHandler ();
//				httpClientHandler.Credentials = new System.Net.NetworkCredential ("mobclients", "M03C113n7$");
//				dataBytesObject.DataString = await PostHttpRequestForStringResponce (new Uri (url), HttpMethod.Post, null, "application/json; charset=utf-8", httpClientHandler, null);
//				dataBytesObject.Response = ((dataBytesObject.DataBytes != null && dataBytesObject.DataBytes.Count () != 0) || !string.IsNullOrEmpty (dataBytesObject.DataString))
//					? new ResponseDataObject () { IsSucceeded = true }
//					: new ResponseDataObject () { IsSucceeded = false };
//			} catch (Exception ex) {
//				dataBytesObject.Response = new ResponseDataObject () { IsSucceeded = false };
//			}
//
//			return dataBytesObject;
//		}
	}
}

