using AppHomolog.Client;
using AppHomolog.Entities;
using Newtonsoft.Json;

namespace AppHomolog
{
    internal class BlingHomologHelpers
    {
        private string _urlBase;
        private string _apiUrlBase;
        
        private string _clientId;
        private string _clientSecret;
        private string _token;
        private string _refreshToken;
        
        private CustomHttpClient _client;

        public BlingHomologHelpers()
        {
            _client = new CustomHttpClient();
        }

        public BlingHomologHelpers(
            string urlBase,
            string apiUrlBase, 
            string clientId,
            string clientSecret,
            string token, 
            string refreshToken)
        {
            _urlBase = urlBase;
            _apiUrlBase = apiUrlBase;
            _clientId = clientId;
            _clientSecret = clientSecret;
            _token = token;
            _refreshToken = refreshToken;
            _client = new CustomHttpClient(token);
        }

        internal BlingResponse<T> StartHomolog<T>(string getUrl) where T : class
        {
            return GetProduct<T>(getUrl);
        }

        internal BlingResponse<TokenResponse> RefreshToken()
        {
            var response = new BlingResponse<TokenResponse>();

            string contentType = "application/x-www-form-urlencoded";
            string url = $"{_urlBase}/oauth/token";
            
            var body = new Dictionary<string, string>();
            body.Add("grant_type", $"refresh_token");
            body.Add("refresh_token", $"{_refreshToken}");

            _client.SetBasicAuth(_clientId, _clientSecret);

            var tokenRS = _client.PostAsync<TokenResponse, ErrorData>(url, body, contentType);

            if (tokenRS.Success)
            {
                response.Data = tokenRS.Data;
                response.StringContent = tokenRS.StringContent;
                response.Success = true;

                _client = new CustomHttpClient(tokenRS.Data.AccessToken);
                _token = tokenRS.Data.AccessToken;
                _refreshToken = tokenRS.Data.RefreshToken;

                return response;
            }

            response.ErrorData = tokenRS.ErrorData;
            return response;
        }

        internal BlingResponse<T> GetProduct<T>(string url) where T : class
        {
            var response = new BlingResponse<T>(1, _token, _refreshToken);
            var getRS = _client.GetAsync<Data, ErrorData>(url);

            if (getRS.Success)
            {
                response.Data = getRS.Data as T;
                response.BlingHeaderHash = getRS.Headers?.Get("x-bling-homologacao") ?? string.Empty;

                return PostProduct<T>($"{_apiUrlBase}/produtos", getRS.Data, response.BlingHeaderHash);
            }

            if (getRS.ErrorData != null)
            {
                if (getRS.ErrorData.Error.Type == "VALIDATION_ERROR")
                {
                    if (getRS.ErrorData != null)
                    {
                        response.ErrorData = getRS.ErrorData;
                        response.StringContent = getRS.StringContent;

                        return response;
                    }
                }
                else if (getRS.ErrorData.Error.Type == "invalid_token")
                {
                    var refreshTokenRS = RefreshToken();

                    if (refreshTokenRS.Success)
                    {
                        getRS = _client.GetAsync<Data, ErrorData>(url);

                        if (getRS.Success)
                        {
                            response.Success = true;
                            response.Data = getRS.Data as T;
                            response.BlingHeaderHash = getRS.Headers?.Get("x-bling-homologacao") ?? string.Empty;

                            return PostProduct<T>($"{_apiUrlBase}/produtos", getRS.Data, response.BlingHeaderHash);
                        }

                        if (getRS.ErrorData != null)
                        {
                            response.ErrorData = getRS.ErrorData;
                            response.StringContent = getRS.StringContent;

                            return response;
                        }
                    }
                    else if (refreshTokenRS.ErrorData != null)
                    {
                        response.ErrorData = refreshTokenRS.ErrorData;
                        response.StringContent = refreshTokenRS.StringContent;

                        return response;
                    }
                }
            }

            return response;
        }

        internal BlingResponse<T> PostProduct<T>(string url, Data data, string blingHeaderHash) where T : class
        {
            JsonSerializerSettings jsonOptions = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            var response = new BlingResponse<T>(2, _token, _refreshToken);
            string contentType = "application/json";

            _client.UpdateHeaderByKey("x-bling-homologacao", blingHeaderHash);

            var postRS = _client.PostAsync<Data, ErrorData>(url, data.Product, contentType, jsonOptions);

            if (postRS.Success)
            {
                response.Success = true;
                response.Data = postRS.Data as T;
                response.BlingHeaderHash = postRS.Headers?.Get("x-bling-homologacao") ?? string.Empty;

                return PutProduct<T>($"{_apiUrlBase}/produtos/{postRS.Data.Product.Id}", postRS.Data, response.BlingHeaderHash);
            }
            
            if (postRS.ErrorData != null)
            {
                if (postRS.ErrorData.Error.Type == "VALIDATION_ERROR")
                {
                    if (postRS.ErrorData != null)
                    {
                        response.ErrorData = postRS.ErrorData;
                        response.StringContent = postRS.StringContent;

                        return response;
                    }
                }
                else if (postRS.ErrorData.Error.Type == "invalid_token")
                {
                    var refreshTokenRS = RefreshToken();

                    if (refreshTokenRS.Success)
                    {
                        _client.UpdateHeaderByKey("x-bling-homologacao", blingHeaderHash);

                        postRS = _client.PostAsync<Data, ErrorData>(url, data.Product, contentType, jsonOptions);

                        if (postRS.Success)
                        {
                            response.Success = true;
                            response.Data = postRS.Data as T;
                            response.BlingHeaderHash = postRS.Headers?.Get("x-bling-homologacao") ?? string.Empty;

                            return PutProduct<T>($"{_apiUrlBase}/produtos/{postRS.Data.Product.Id}", postRS.Data, response.BlingHeaderHash);
                        }

                        if (postRS.ErrorData != null)
                        {
                            response.ErrorData = postRS.ErrorData;
                            response.StringContent = postRS.StringContent;

                            return response;
                        }
                    }
                    else if (refreshTokenRS.ErrorData != null)
                    {
                        response.ErrorData = refreshTokenRS.ErrorData;
                        response.StringContent = refreshTokenRS.StringContent;

                        return response;
                    }
                }
            }

            return response;
        }

        internal BlingResponse<T> PutProduct<T>(string url, Data data, string blingHeaderHash) where T : class
        {
            JsonSerializerSettings jsonOptions = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            var response = new BlingResponse<T>(3, _token, _refreshToken);
            string contentType = "application/json";

            _client.UpdateHeaderByKey("x-bling-homologacao", blingHeaderHash);

            var newProduct = new Product()
            { 
                Name = "Copo",
                Value = data.Product.Value,
                Code = data.Product.Code
            };
            var productStatus = new ProductStatus("I");

            var putRS = _client.PutAsync<Data, ErrorData>(url, newProduct, contentType, jsonOptions);

            if (putRS.Success)
            {
                response.Success = true;
                response.Data = putRS.Data as T;
                response.BlingHeaderHash = putRS.Headers?.Get("x-bling-homologacao") ?? string.Empty;

                return PatchProduct<T>($"{_apiUrlBase}/produtos/{data.Product.Id}/situacoes", productStatus, (data.Product.Id ?? 0), response.BlingHeaderHash);
            }

            if (putRS.ErrorData != null)
            {
                if (putRS.ErrorData.Error.Type == "VALIDATION_ERROR")
                {
                    if (putRS.ErrorData != null)
                    {
                        response.ErrorData = putRS.ErrorData;
                        response.StringContent = putRS.StringContent;

                        return response;
                    }
                }
                else if (putRS.ErrorData.Error.Type == "invalid_token")
                {
                    var refreshTokenRS = RefreshToken();

                    if (refreshTokenRS.Success)
                    {
                        _client.UpdateHeaderByKey("x-bling-homologacao", blingHeaderHash);

                        putRS = _client.PutAsync<Data, ErrorData>(url, newProduct, contentType, jsonOptions);

                        if (putRS.Success)
                        {
                            response.Success = true;
                            response.Data = putRS.Data != null ? putRS.Data as T : null;
                            response.BlingHeaderHash = putRS.Headers?.Get("x-bling-homologacao") ?? string.Empty;

                            return PatchProduct<T>($"{_apiUrlBase}/produtos/{data.Product.Id}/situacoes", productStatus, (data.Product.Id ?? 0), response.BlingHeaderHash);
                        }

                        if (putRS.ErrorData != null)
                        {
                            response.ErrorData = putRS.ErrorData;
                            response.StringContent = putRS.StringContent;

                            return response;
                        }
                    }
                    else if (refreshTokenRS.ErrorData != null)
                    {
                        response.ErrorData = refreshTokenRS.ErrorData;
                        response.StringContent = refreshTokenRS.StringContent;

                        return response;
                    }
                }
            }

            return response;
        }

        internal BlingResponse<T> PatchProduct<T>(string url, ProductStatus status, long productId, string blingHeaderHash) where T : class
        {
            JsonSerializerSettings jsonOptions = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            var response = new BlingResponse<T>(4, _token, _refreshToken);
            string contentType = "application/json";

            _client.UpdateHeaderByKey("x-bling-homologacao", blingHeaderHash);

            var patchRS = _client.PatchAsync<Data, ErrorData>(url, status, contentType, jsonOptions);

            if (patchRS.Success)
            {
                response.Success = true;
                response.Data = patchRS.Data as T;
                response.BlingHeaderHash = patchRS.Headers?.Get("x-bling-homologacao") ?? string.Empty;

                return DeleteProduct<T>($"{_apiUrlBase}/produtos/{productId}", response.BlingHeaderHash);
            }

            if (patchRS.ErrorData != null)
            {
                if (patchRS.ErrorData.Error.Type == "VALIDATION_ERROR")
                {
                    if (patchRS.ErrorData != null)
                    {
                        response.ErrorData = patchRS.ErrorData;
                        response.StringContent = patchRS.StringContent;

                        return response;
                    }
                }
                else if (patchRS.ErrorData.Error.Type == "invalid_token")
                {
                    var refreshTokenRS = RefreshToken();

                    if (refreshTokenRS.Success)
                    {
                        _client.UpdateHeaderByKey("x-bling-homologacao", blingHeaderHash);

                        patchRS = _client.PatchAsync<Data, ErrorData>(url, status, contentType, jsonOptions);

                        if (patchRS.Success)
                        {
                            response.Success = true;
                            response.Data = patchRS.Data as T;
                            response.BlingHeaderHash = patchRS.Headers?.Get("x-bling-homologacao") ?? string.Empty;

                            return DeleteProduct<T>($"{_apiUrlBase}/produtos/{productId}", response.BlingHeaderHash);
                        }

                        if (patchRS.ErrorData != null)
                        {
                            response.ErrorData = patchRS.ErrorData;
                            response.StringContent = patchRS.StringContent;

                            return response;
                        }
                    }
                    else if (refreshTokenRS.ErrorData != null)
                    {
                        response.ErrorData = refreshTokenRS.ErrorData;
                        response.StringContent = refreshTokenRS.StringContent;

                        return response;
                    }
                }
            }

            return response;
        }

        internal BlingResponse<T> DeleteProduct<T>(string url, string blingHeaderHash) where T : class
        {
            JsonSerializerSettings jsonOptions = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            var response = new BlingResponse<T>(5, _token, _refreshToken);
            string contentType = "application/json";

            _client.UpdateHeaderByKey("x-bling-homologacao", blingHeaderHash);

            var patchRS = _client.DeleteAsync<Data, ErrorData>(url);

            if (patchRS.Success)
            {
                response.Success = true;
                response.Data = patchRS.Data != null ? patchRS.Data as T : null;

                return response;
            }

            if (patchRS.ErrorData != null)
            {
                if (patchRS.ErrorData.Error.Type == "VALIDATION_ERROR")
                {
                    if (patchRS.ErrorData != null)
                    {
                        response.ErrorData = patchRS.ErrorData;
                        response.StringContent = patchRS.StringContent;

                        return response;
                    }
                }
                else if (patchRS.ErrorData.Error.Type == "invalid_token")
                {
                    var refreshTokenRS = RefreshToken();

                    if (refreshTokenRS.Success)
                    {
                        _client.UpdateHeaderByKey("x-bling-homologacao", blingHeaderHash);

                        patchRS = _client.DeleteAsync<Data, ErrorData>(url);

                        if (patchRS.Success)
                        {
                            response.Success = true;
                            response.Data = patchRS.Data != null ? patchRS.Data as T : null;
                            response.BlingHeaderHash = patchRS.Headers?.Get("x-bling-homologacao") ?? string.Empty;

                            return response;
                        }

                        if (patchRS.ErrorData != null)
                        {
                            response.ErrorData = patchRS.ErrorData;
                            response.StringContent = patchRS.StringContent;

                            return response;
                        }
                    }
                    else if (refreshTokenRS.ErrorData != null)
                    {
                        response.ErrorData = refreshTokenRS.ErrorData;
                        response.StringContent = refreshTokenRS.StringContent;

                        return response;
                    }
                }
            }

            return response;
        }
    }
}
