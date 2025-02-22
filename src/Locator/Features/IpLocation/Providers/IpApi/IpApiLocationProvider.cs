using Locator.Features.IpLocation.Contracts;
using System.Text.Json.Serialization;

namespace Locator.Features.IpLocation.Providers.IpApi;

public sealed class IpApiLocationProvider(
    IHttpClientFactory httpClientFactory,
    ILogger<IpApiLocationProvider> logger) : ILocationProvider
{
    private const string BASE_URI = "https://ip-api.com/json/";

    public async Task<LocationProviderResult> GetLocationAsync(LocationRequest request)
    {
        try
        {
            using var httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new(BASE_URI);
            var response = await httpClient.GetFromJsonAsync<IpApiResponse>($"{request.IpAddress}");

            return new()
            {
                IsSuccess = true,
                Result = new(response.Country, response.State, response.City, response.Latitude, response.Longitude)
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error in {nameof(IpApiLocationProvider)}.{nameof(GetLocationAsync)}");
            return new() { IsSuccess = false };
        }
    }

    private sealed class IpApiResponse
    {
        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("regionName")]
        public string State { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("lat")]
        public double Latitude { get; set; }

        [JsonPropertyName("lon")]
        public double Longitude { get; set; }
    }
}