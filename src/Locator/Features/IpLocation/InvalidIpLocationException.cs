namespace Locator.Features.IpLocation;

public sealed class InvalidIpLocationException(string ipAddress) : Exception(string.Format(ERROR_MESSAGE_TEMPLATE, ipAddress))
{
    private const string ERROR_MESSAGE_TEMPLATE = "Can not find a valid location for ip address : {0}";
}