using Locator.Features.IpLocation.Contracts;

namespace Locator.Features.IpLocation;

public interface ILocationProvider
{
    Task<LocationProviderResult> GetLocationAsync(LocationRequest request);
}