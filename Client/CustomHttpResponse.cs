using System.Collections.Specialized;

namespace AppHomolog.Client
{
    public class CustomHttpResponse<T, TError> : CustomHttpResponse where T : class
    {
        public T Data { get; set; }

        public TError ErrorData { get; set; }
    }

    public class CustomHttpResponse
    {
        public bool Success { get; set; }

        public string ErrorMessage { get; set; }

        public string StringContent { get; set; }

        public NameValueCollection Headers { get; set; }
    }
}
