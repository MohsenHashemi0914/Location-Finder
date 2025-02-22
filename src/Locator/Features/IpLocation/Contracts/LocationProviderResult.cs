namespace Locator.Features.IpLocation.Contracts;

public sealed class LocationProviderResult
{
    public bool IsSuccess { get; set; }
    public LocationResponse? Result { get; set; }
}