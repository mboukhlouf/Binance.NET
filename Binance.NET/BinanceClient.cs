using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace Binance
{
    public class BinanceClient
    {
        private HttpClientHandler httpClientHandler;
        private HttpClient httpClient;

        public String UserAgent { get; set; } = "B-Trader";

        public Uri BaseAddress { get; } = new Uri("https://api.binance.com");

        public String ApiKey { get; set; }
        public String SecretKey { get; set; }

        public BinanceClient()
        {
            httpClientHandler = new HttpClientHandler()
            {
                UseCookies = false,
                AllowAutoRedirect = false
            };
            httpClient = new HttpClient(httpClientHandler)
            {
                BaseAddress = BaseAddress
            };
        }

        public BinanceClient(String apiKey, String secretKey) : this()
        {
            ApiKey = apiKey;
            SecretKey = secretKey;
        }

        /// <summary>
        /// Creates a HMAC SHA256 signature of the totalParams using the SecretKey as the key
        /// </summary>
        /// <param name="totalParams">The string to sign, it's defined as the query string concatenated with the request body.</param>
        /// <returns></returns>
        public String Sign(String totalParams)
        {
            byte[] key = Encoding.UTF8.GetBytes(SecretKey);
            byte[] input = Encoding.UTF8.GetBytes(totalParams);
            using (HMACSHA256 hmac = new HMACSHA256(key))
            {
                byte[] hash = hmac.ComputeHash(input);
                String signature = BitConverter.ToString(hash).Replace("-", "").ToLower();
                return signature;
            }   
        }

        public async Task<long> PingAsync()
        {
            String uri = "api/v1/ping";
            Stopwatch stopWatch = Stopwatch.StartNew();
            using (await GetAsync(uri, null, HttpCompletionOption.ResponseContentRead))
            stopWatch.Stop();
            return stopWatch.ElapsedMilliseconds;
        }

        public async Task<HttpResponseMessage> SendAsync(HttpMethod method, Uri uri, Dictionary<String, String> headers, HttpCompletionOption httpCompletionOption)
        {
            var requestMessage = new HttpRequestMessage(method, uri);
            if (headers != null)
                foreach (var header in headers)
                    requestMessage.Headers.Add(header.Key, header.Value);

            // User-Agent
            if (!requestMessage.Headers.Contains("User-Agent"))
                requestMessage.Headers.Add("User-Agent", UserAgent);

            HttpResponseMessage response;
            try
            {
                response = await httpClient.SendAsync(requestMessage, httpCompletionOption);
                requestMessage.Dispose();
                return response;
            }
            catch (Exception e)
            {
                requestMessage.Dispose();
                throw e;
            }
        }

        public Task<HttpResponseMessage> GetAsync(Uri uri, Dictionary<String, String> headers, HttpCompletionOption httpCompletionOption)
        {
            return SendAsync(HttpMethod.Get, uri, headers, httpCompletionOption);
        }

        public Task<HttpResponseMessage> GetAsync(Uri uri, Dictionary<String, String> headers)
        => GetAsync(uri, headers, HttpCompletionOption.ResponseContentRead);

        public Task<HttpResponseMessage> GetAsync(Uri uri)
                => GetAsync(uri, null, HttpCompletionOption.ResponseContentRead);

        public Task<HttpResponseMessage> GetAsync(String uri, Dictionary<String, String> headers, HttpCompletionOption httpCompletionOption)
            => GetAsync(new Uri(uri, UriKind.RelativeOrAbsolute), headers, httpCompletionOption);

        public Task<HttpResponseMessage> GetAsync(String uri, Dictionary<String, String> headers)
        => GetAsync(uri, headers, HttpCompletionOption.ResponseContentRead);

        public Task<HttpResponseMessage> GetAsync(String uri)
                => GetAsync(uri, null, HttpCompletionOption.ResponseContentRead);

        public Task<HttpResponseMessage> PostAsync(Uri uri, String postData, Dictionary<String, String> headers, HttpCompletionOption httpCompletionOption)
        {
            return SendAsync(HttpMethod.Post, uri, headers, httpCompletionOption);
        }

        public Task<HttpResponseMessage> PostAsync(Uri uri, String postData, Dictionary<String, String> headers)
            => PostAsync(uri, postData, headers, HttpCompletionOption.ResponseContentRead);

        public Task<HttpResponseMessage> PostAsync(Uri uri, String postData)
            => PostAsync(uri, postData, null, HttpCompletionOption.ResponseContentRead);

        public Task<HttpResponseMessage> PostAsync(String uri, String postData, Dictionary<String, String> headers, HttpCompletionOption httpCompletionOption)
        => PostAsync(new Uri(uri, UriKind.RelativeOrAbsolute), postData, headers, httpCompletionOption);

        public Task<HttpResponseMessage> PostAsync(String uri, String postData, Dictionary<String, String> headers)
            => PostAsync(uri, postData, headers, HttpCompletionOption.ResponseContentRead);

        public Task<HttpResponseMessage> PostAsync(String uri, String postData)
            => PostAsync(uri, postData, null, HttpCompletionOption.ResponseContentRead);
    }
}
