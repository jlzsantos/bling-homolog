using Newtonsoft.Json;

namespace AppHomolog.Entities
{
    internal class Data
    {
        [JsonProperty("data")]
        public Product Product { get; set; }
    }

    internal class Product
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("nome")]
        public string Name { get; set; }

        [JsonProperty("preco")]
        public decimal Value { get; set; }

        [JsonProperty("codigo")]
        public string Code { get; set; }
    }

    internal class ProductStatus
    {
        public ProductStatus()
        {
            
        }

        public ProductStatus(string status)
        {
            Status = status;
        }

        [JsonProperty("situacao")]
        public string Status { get; set; }
    }
}
