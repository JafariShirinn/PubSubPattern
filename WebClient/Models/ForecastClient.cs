using System;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebClient.Exceptions;

namespace WebClient.Models
{
    public class ForecastClient
    {
        private readonly HttpClient _httpClient;

        public ForecastClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseModel> Broadcast(WeatherForecastRequestModel model)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/weatherForecast/", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return new ResponseModel
                {
                    Content = response.Content.ToString(),
                    StatusCode = 200
                };
            }

            return await HandleFailureResponse(response);
        }

        private static async Task<ResponseModel> HandleFailureResponse(HttpResponseMessage response)
        {
            var (deserialized, errorModel) = await TryDeserialize<ErrorModel>(response);

            if (deserialized)
            {
                throw new WebClientException(response.StatusCode, errorModel?.ErrorMessage)
                {
                    ErrorCode = errorModel?.ErrorCode ?? 0,
                    ValidationErrors = errorModel?.ValidationErrors
                };
            }

            throw new WebClientException(response.StatusCode, response.ReasonPhrase);
        }

        private static async Task<(bool Deserialized, T Instance)> TryDeserialize<T>(HttpResponseMessage response)
        {
            if (response == null || response.Content == null)
                return (false, default);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (responseContent == string.Empty)
                return (false, default);

            var responseType = typeof(T);
            if (responseType.IsClass && responseType.GetConstructor(Type.EmptyTypes) != null)
                return (true, JsonConvert.DeserializeObject<T>(responseContent));

            var typeConverter = TypeDescriptor.GetConverter(responseType);

            return typeConverter.CanConvertFrom(typeof(string))
                ? (true, (T)typeConverter.ConvertFrom(responseContent))
                : (false, default);
        }
    }
}
