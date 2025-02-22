using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace Locator.Features.IpLocation.Models;

[Collection("Locations")]
public sealed class Location
{
    public ObjectId Id { get; set; }

    public required string IpAddress { get; set; }

    public required string Country { get; set; }

    public required string State { get; set; }

    public required string City { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }
}