using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

using Newtonsoft.Json.Linq;

using Binance.Helpers;
using Binance.Serialization;
using Binance.Exceptions;

namespace Binance
{
    public class BinanceClient
    {
        private HttpClientHandler httpClientHandler;
        private HttpClient httpClient;

        public String UserAgent { get; set; } = "Binance.NET";

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

        public async Task<long> CheckServerTimeAsync()
        {
            String uri = "api/v1/time";
            using (var response = await GetAsync(uri, null, HttpCompletionOption.ResponseContentRead))
            {
                int statusCode = (int)response.StatusCode;
                String body = await response.Content.ReadAsStringAsync();
                CheckForBinanceException(statusCode, body);
                JObject jResponse = JObject.Parse(body);
                return (long)jResponse["serverTime"];
            }
        }

        public async Task<ExchangeInfo> GetExchangeInfoAsync()
        {
            String uri = "api/v1/exchangeInfo";
            using (var response = await GetAsync(uri, null))
            {
                int statusCode = (int)response.StatusCode;
                String body = await response.Content.ReadAsStringAsync();
                CheckForBinanceException(statusCode, body);
                var exchangeInfo = JsonHelper.ParseFromJson<ExchangeInfo>(body);
                return exchangeInfo;
            }
        }

        public async Task<Dictionary<String, decimal>> GetSymbolPriceTickerAsync()
        {
            String uri = $"api/v3/ticker/price";
            using (var response = await GetAsync(uri, null))
            {
                int statusCode = (int)response.StatusCode;
                String body = await response.Content.ReadAsStringAsync();
                CheckForBinanceException(statusCode, body);
                JArray jBody = JArray.Parse(body);
                return jBody.ToDictionary((token) => (String)token["symbol"], (token) => (decimal)token["price"]);
            }
        }

        public async Task<KeyValuePair<String, decimal>> GetSymbolPriceTickerAsync(String symbol)
        {
            String uri = $"api/v3/ticker/price?symbol={symbol}";
            using (var response = await GetAsync(uri, null))
            {
                int statusCode = (int)response.StatusCode;
                String body = await response.Content.ReadAsStringAsync();
                CheckForBinanceException(statusCode, body);
                JObject jBody = JObject.Parse(body);
                return new KeyValuePair<string, decimal>((String)jBody["symbol"], (decimal)jBody["price"]);
            }
        }

        public async Task TestNewOrderAsync(Order order)
        {
            String uri = "api/v3/order/test";
            if (order.Timestamp == null)

                order.Timestamp = DateNowUnix();

            if (order.RecvWindow == null)
                order.RecvWindow = 60000;

            Dictionary<String, String> headers = new Dictionary<string, string>();
            headers.Add("X-MBX-APIKEY", ApiKey);

            String postData = FormDataSerializer.Serialize(order);
            String signature = Sign(postData);
            postData += $"&signature={signature}";
            using (var response = await PostAsync(uri, postData, headers))
            {
                int statusCode = (int)response.StatusCode;
                String body = await response.Content.ReadAsStringAsync();
                CheckForBinanceException(statusCode, body);
            }
        }

        public async Task<OrderResponse> NewOrderAsync(Order order)
        {
            String uri = "api/v3/order";
            if (order.Timestamp == null)

                order.Timestamp = DateNowUnix();

            if (order.RecvWindow == null)
                order.RecvWindow = 60000;

            Dictionary<String, String> headers = new Dictionary<string, string>();
            headers.Add("X-MBX-APIKEY", ApiKey);

            String postData = FormDataSerializer.Serialize(order);
            String signature = Sign(postData);
            postData += $"&signature={signature}";
            using (var response = await PostAsync(uri, postData, headers))
            {
                int statusCode = (int)response.StatusCode;
                String body = await response.Content.ReadAsStringAsync();
                CheckForBinanceException(statusCode, body);
                var orderResponse = JsonHelper.ParseFromJson<OrderResponse>(body);
                return orderResponse;
            }
        }

        public async Task<OcoOrderResponse> NewOcoOrderAsync(OcoOrder order)
        {
            String uri = "api/v3/order/oco";
            if (order.Timestamp == null)
                order.Timestamp = DateNowUnix();

            if (order.RecvWindow == null)
                order.RecvWindow = 60000;

            Dictionary<String, String> headers = new Dictionary<string, string>();
            headers.Add("X-MBX-APIKEY", ApiKey);

            String postData = FormDataSerializer.Serialize(order);
            String signature = Sign(postData);
            postData += $"&signature={signature}";
            using (var response = await PostAsync(uri, postData, headers))
            {
                int statusCode = (int)response.StatusCode;
                String body = await response.Content.ReadAsStringAsync();
                CheckForBinanceException(statusCode, body);
                var orderResponse = JsonHelper.ParseFromJson<OcoOrderResponse>(body);
                return orderResponse;
            }
        }

        public void CheckForBinanceException(int statusCode, String body)
        {
            if (statusCode >= 200 && statusCode <= 299)
            {
                return;
            }
            else if (statusCode == 429)
            {
                throw RateLimitedException.CreateFromPayload(body);
            }
            else if(statusCode == 418)
            {
                throw IpBannedException.CreateFromPayload(body);
            }
            else if(statusCode >= 400 && statusCode <= 499)
            {
                throw MalformedRequestException.CreateFromPayload(body);
            }
            else if(statusCode >= 500 && statusCode <= 599)
            {
                throw InternalErrorException.CreateFromPayload(body);
            }
            else
            {
                throw new BinanceException(0, body);
            }
        }

        public async Task<HttpResponseMessage> GetAsync(Uri uri, Dictionary<String, String> headers, HttpCompletionOption httpCompletionOption)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            if (headers != null)
                foreach (var header in headers)
                    requestMessage.Headers.Add(header.Key, header.Value);

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

        private static long DateNowUnix()
        {
            return (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
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

        public async Task<HttpResponseMessage> PostAsync(Uri uri, String postData, Dictionary<String, String> headers, HttpCompletionOption httpCompletionOption)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            requestMessage.Content = new StringContent(postData, Encoding.UTF8, "application/x-www-form-urlencoded");
            if (headers != null)
                foreach (var header in headers)
                    requestMessage.Headers.Add(header.Key, header.Value);

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
