using Locator.Features.IpLocation.Providers.IpApi;
using Locator.Features.IpLocation.Providers.IpGeoLocation;
using Microsoft.EntityFrameworkCore;

namespace Locator.Features.IpLocation;

public static class DependencyInjectionExtensions
{
    public static void AddIpLocationFeature(this IHostApplicationBuilder builder)
    {
        var mongoDbConfig = builder.Configuration.GetSection(MongoDbOptions.SectionName).Get<MongoDbOptions>();

        ArgumentNullException.ThrowIfNull(mongoDbConfig, nameof(MongoDbOptions));

        builder.Services.AddDbContext<IpLocationDbContext>(options =>
        {
            options.UseMongoDB(mongoDbConfig.Host, mongoDbConfig.DatabaseName);
        });

        builder.Services.AddHttpClient();
        builder.Services.AddScoped<LocationService>();
        builder.Services.AddScoped<ILocationProvider, IpGeoLocationProvider>();
        builder.Services.AddScoped<ILocationProvider, IpApiLocationProvider>();
    }
}