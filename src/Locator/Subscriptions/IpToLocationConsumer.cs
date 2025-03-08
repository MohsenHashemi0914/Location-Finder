using Locator.Features.IpLocation;
using Locator.IntegrationMessages;
using MassTransit;

namespace Locator.Subscriptions;

public sealed class IpToLocationConsumer(LocationService locationService) : IConsumer<IpToLocationMessage>
{
    public async Task Consume(ConsumeContext<IpToLocationMessage> context)
    {
        var message = context.Message;
        var location = await locationService.GetLocationAsync(new(message.IpAddress));

        IpToLocationResponseMessage response = new(location.Country, location.State, location.City, message.RequestId);
        await context.Publish(response, context.CancellationToken);
    }
}