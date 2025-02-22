namespace Locator.Features.IpLocation.Contracts;

public sealed record LocationResponse(string Country, string State, string City, double Latitude, double Longitude);