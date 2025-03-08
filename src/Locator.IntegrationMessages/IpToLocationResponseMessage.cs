namespace Locator.IntegrationMessages;

public sealed record IpToLocationResponseMessage(string Country, string State, string City, Guid RequestId);