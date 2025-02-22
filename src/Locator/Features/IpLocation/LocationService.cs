using Locator.Features.IpLocation.Contracts;
using Locator.Features.IpLocation.Models;
using Microsoft.EntityFrameworkCore;

namespace Locator.Features.IpLocation;

public sealed class LocationService(
    IpLocationDbContext dbContext,
    ILogger<LocationService> logger,
    IEnumerable<ILocationProvider> providers)
{
    public async Task<LocationResponse> GetLocationAsync(LocationRequest request)
    {
		try
		{
            var location = await dbContext.Locations.FirstOrDefaultAsync(x => x.IpAddress == request.IpAddress);
            if (location is not null)
            {
                return new(location.Country, location.State, location.City, location.Latitude, location.Longitude);
            }

            foreach (var provider in providers)
            {
                var result = await provider.GetLocationAsync(request)!;
                if(result is { IsSuccess : false})
                {
                    continue;
                }

                var locationResult = result.Result!;
                Location dbModel = new()
                {
                    City = locationResult.City,
                    State = locationResult.State,
                    IpAddress = request.IpAddress,
                    Country = locationResult.Country,
                    Latitude = locationResult.Latitude,
                    Longitude = locationResult.Longitude
                };

                await dbContext.AddAsync(dbModel);
                await dbContext.SaveChangesAsync();

                return new(dbModel.Country, dbModel.State, dbModel.City, dbModel.Latitude, dbModel.Longitude);
            }

            throw new InvalidIpLocationException(request.IpAddress);
        }
		catch (Exception ex)
		{
            logger.LogError(ex, $"Error in {nameof(LocationService)}.{nameof(GetLocationAsync)}");
            throw ex;
		}
    }
}