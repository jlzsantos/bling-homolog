using Newtonsoft.Json;

namespace AppHomolog.Entities
{
    internal class ErrorData
    {
        [JsonProperty("error")]
        public Error Error { get; set; }
    }

    internal class Error
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("fields")]
        public List<FieldData> Fields { get; set; }
    }

    internal class FieldData
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("msg")]
        public string Message { get; set; }

        [JsonProperty("element")]
        public string Element { get; set; }

        [JsonProperty("namespace")]
        public string Namespace { get; set; }
    }
}
