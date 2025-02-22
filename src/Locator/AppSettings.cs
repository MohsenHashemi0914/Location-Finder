namespace Locator;

public sealed class AppSettings
{
    public required string BaseUrl { get; set; }
    public required MongoDbOptions MongoDbConfigurations { get; set; }
    public required LocationFeatures Features { get; set; }

    public sealed class LocationFeatures
    {
        public required IpLocationFeature IpLocation { get; set; }

        public sealed class IpLocationFeature
        {
            public required IpLocationProviders Providers { get; set; }

            public sealed class IpLocationProviders
            {
                public required IpGeoLocationProvider IpGeoLocation { get; set; }

                public sealed class IpGeoLocationProvider
                {
                    public required string ApiKey { get; set; }
                    public required string BaseUri { get; set; }
                }
            }
        }
    }
}

public sealed class MongoDbOptions
{
    public const string SectionName = "MongoDbConfigurations";

    public required string Host { get; set; }
    public required string DatabaseName { get; set; }
}
