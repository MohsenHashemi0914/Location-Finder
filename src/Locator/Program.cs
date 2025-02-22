using Locator;
using Locator.Features.IpLocation;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.AddIpLocationFeature();
builder.Services.Configure<AppSettings>(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapGet("/{ip}", 
    async ([FromRoute(Name = "ip")] string ipAddress, LocationService service) =>
{
    var result = await service.GetLocationAsync(new(ipAddress));
    return Results.Ok(result);
});

app.Run();