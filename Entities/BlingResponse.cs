namespace AppHomolog.Entities
{
    internal class BlingResponse<T> : BlingResponse where T : class
    {
        public BlingResponse()
        {
            
        }

        public BlingResponse(int requestNumber, string token, string refreshToken)
        {
            RequestNumber = requestNumber;
            Token = token;
            RefreshToken = refreshToken;
        }

        public T Data { get; set; }

        public ErrorData ErrorData { get; set; }
    }

    internal class BlingResponse
    {
        public int RequestNumber { get; set; }

        public bool Success { get; set; }

        public string StringContent { get; set; }

        public string BlingHeaderHash { get; set; }

        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }
}
