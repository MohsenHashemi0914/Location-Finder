using Locator.Features.IpLocation.Contracts;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using GeoLocationProviderSettings = Locator.AppSettings.LocationFeatures.IpLocationFeature.IpLocationProviders.IpGeoLocationProvider;

namespace Locator.Features.IpLocation.Providers.IpGeoLocation;

public sealed class IpGeoLocationProvider(
    IOptions<AppSettings> options,
    IHttpClientFactory httpClientFactory,
    ILogger<IpGeoLocationProvider> logger) : ILocationProvider
{
    private readonly GeoLocationProviderSettings _settings = options.Value.Features.IpLocation.Providers.IpGeoLocation;

    public async Task<LocationProviderResult> GetLocationAsync(LocationRequest request)
    {
        try
        {
            using var httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new(_settings.BaseUri);
            var response = await httpClient.GetFromJsonAsync<IpGeoLocationResponse>($"apiKey={_settings.ApiKey}&ip={request.IpAddress}");

            return new()
            {
                IsSuccess = true,
                Result = new(response.Country, response.State, response.City, response.Latitude, response.Longitude)
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error in {nameof(IpGeoLocationProvider)}.{nameof(GetLocationAsync)}");
            return new() { IsSuccess = false };
        }
    }

    private sealed class IpGeoLocationResponse
    {
        [JsonPropertyName("country_name")]
        public string Country { get; set; }

        [JsonPropertyName("state_prov")]
        public string State { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
    }
}