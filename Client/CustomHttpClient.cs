using Newtonsoft.Json;
using Polly;
using System.Collections.Specialized;
using System.Net.Http.Headers;
using System.Text;

namespace AppHomolog.Client
{
    internal class CustomHttpClient
    {
        HttpClient _client;
        private AsyncPolicy<HttpResponseMessage> _policyHandling;

        public CustomHttpClient()
        {
            SetPolicy();

            _client = new HttpClient { Timeout = TimeSpan.FromSeconds(120) };
        }

        public CustomHttpClient(string bearerToken)
            : this()
        {
            SetAuthBearer(bearerToken);
        }

        public CustomHttpResponse<ObjResponse, ObjResponseError> GetAsync<ObjResponse, ObjResponseError>(string url) where ObjResponse : class
        {
            //return _policyHandling.ExecuteAsync(() => _client.GetAsync(url));

            var result = _client.GetAsync(url).Result;
            var response = new CustomHttpResponse<ObjResponse, ObjResponseError>();
            
            response.StringContent = result.Content.ReadAsStringAsync().Result;

            if (result.IsSuccessStatusCode)
            {
                response.Success = true;
                response.Headers = GetHeaders(result);
                response.Data = response.StringContent.Deserialize<ObjResponse>(result.Content.Headers.ContentType.MediaType, null);
            }
            else
            {
                response.ErrorMessage = result.ReasonPhrase;
                response.ErrorData = response.StringContent.Deserialize<ObjResponseError>(result.Content.Headers.ContentType.MediaType, null);
            }

            return response;
        }

        public CustomHttpResponse<ObjResponse, ObjResponseError> PostAsync<ObjResponse, ObjResponseError>(
            string url,
            object body,
            string mediaType,
            JsonSerializerSettings jsonSerializerSettings = null) where ObjResponse : class
        {
            return ExecuteAsync<ObjResponse, ObjResponseError>("POST", url, body, mediaType, jsonSerializerSettings);
        }

        public CustomHttpResponse<ObjResponse, ObjResponseError> PutAsync<ObjResponse, ObjResponseError>(
            string url,
            object body,
            string mediaType,
            JsonSerializerSettings jsonSerializerSettings = null) where ObjResponse : class
        {
            return ExecuteAsync<ObjResponse, ObjResponseError>("PUT", url, body, mediaType, jsonSerializerSettings);
        }

        public CustomHttpResponse<ObjResponse, ObjResponseError> PatchAsync<ObjResponse, ObjResponseError>(
            string url,
            object body,
            string mediaType,
            JsonSerializerSettings jsonSerializerSettings = null) where ObjResponse : class
        {
            return ExecuteAsync<ObjResponse, ObjResponseError>("PATCH", url, body, mediaType, jsonSerializerSettings);
        }

        public CustomHttpResponse<ObjResponse, ObjResponseError> DeleteAsync<ObjResponse, ObjResponseError>(string url) where ObjResponse : class
        {
            return ExecuteAsync<ObjResponse, ObjResponseError>("DELETE", url, null, null, null);
        }

        private CustomHttpResponse<ObjResponse, ObjResponseError> ExecuteAsync<ObjResponse, ObjResponseError>(
            string verb,
            string url,
            object body,
            string mediaType,
            JsonSerializerSettings jsonSerializerSettings = null) where ObjResponse : class
        {
            var response = new CustomHttpResponse<ObjResponse, ObjResponseError>();

            HttpContent content = null;
            HttpResponseMessage result = null;

            if (body != null)
            {
                if (mediaType == "application/json")
                {
                    content = Getcontent(body.Serialize(mediaType, jsonSerializerSettings), mediaType);
                }
                else
                {
                    content = Getcontent(body, mediaType);
                }
            }

            if (verb == "POST")
            {
                result = _client.PostAsync(url, content).Result;
            }
            else if (verb == "PATCH")
            {
                result = _client.PatchAsync(url, content).Result;
            }
            else if (verb == "DELETE")
            {
                result = _client.DeleteAsync(url).Result;
            }
            else
            {
                result = _client.PutAsync(url, content).Result;
            }

            response.StringContent = result.Content.ReadAsStringAsync().Result;

            if (result.IsSuccessStatusCode)
            {
                response.Success = true;
                response.Headers = GetHeaders(result);

                if (!string.IsNullOrWhiteSpace(response.StringContent) && typeof(ObjResponse) != typeof(string))
                {
                    response.Data = response.StringContent.Deserialize<ObjResponse>(result.Content.Headers.ContentType.MediaType);
                }
            }
            else
            {
                if (typeof(ObjResponse) != typeof(string))
                {
                    response.ErrorData = response.StringContent.Deserialize<ObjResponseError>(result.Content.Headers.ContentType.MediaType);
                }

                response.ErrorMessage = result.ReasonPhrase;
            }

            return response;
        }

        private void SetPolicy()
        {
            _policyHandling = Policy<HttpResponseMessage>
                .HandleResult(r => r.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable ||
                                   r.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                .WaitAndRetryAsync(3, (retryAttempt) => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        public void SetAuthBearer(string bearerToken)
        {
            if (string.IsNullOrEmpty(bearerToken))
            {
                throw new ArgumentException("Bearer Token não informado ou vazio.");
            }

            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{bearerToken}");
        }

        private HttpContent Getcontent(object serialized, string mediaType)
        {
            if (mediaType == "application/x-www-form-urlencoded")
            {
                return new FormUrlEncodedContent(serialized as Dictionary<string, string>);
            }
            else
            {
                return new StringContent(serialized?.ToString() ?? "", Encoding.UTF8, mediaType);
            }
        }

        public void AddHeader(string name, string value)
        {
            _client.DefaultRequestHeaders.Add(name, value);
        }

        public void UpdateHeaderByKey(string name, string value)
        {
            if (_client.DefaultRequestHeaders.Contains(name))
            {
                _client.DefaultRequestHeaders.Remove(name);
            }

            AddHeader(name, value);
        }

        private NameValueCollection GetHeaders(HttpResponseMessage message)
        {
            return Enumerable
                     .Empty<(String name, String value)>()
                     .Concat(
                         message.Headers
                             .SelectMany(kvp => kvp.Value
                                .Select(v => (name: kvp.Key, value: v))
                             )
                     )
                     .Concat(
                         message.Content.Headers
                             .SelectMany(kvp => kvp.Value
                                .Select(v => (name: kvp.Key, value: v))
                             )
                     )
                     .Aggregate(
                         seed: new NameValueCollection(),
                         func: (nvc, pair) => { nvc.Add(pair.name, pair.value); return nvc; },
                         resultSelector: nvc => nvc
                     );
        }

        public virtual void SetBasicAuth(string login, string password)
        {
            var authBase64 = this.GetAuthBase64(login, password);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authBase64);
        }

        public virtual string GetAuthBase64(string login, string password)
        {
            return Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{login}:{password}"));
        }
    }
}
