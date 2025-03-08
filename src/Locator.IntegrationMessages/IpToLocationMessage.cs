namespace Locator.IntegrationMessages;

public sealed record IpToLocationMessage(Guid RequestId, string IpAddress);