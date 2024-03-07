using Api.IntegrationTest.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NewDogTinder;
using System.Net;
using System.Net.Http.Json;

namespace IntegrationTests;

[Collection("Sequential")]
public class Appointment : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;

    public Appointment(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task PostTodoWithValidParameters()
    {// Arrange
        var client = _factory.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });


        var response = await client.GetAsync("/dog");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task AppointmentTodoWithValidParameters()
    {// Arrange
        var client = _factory.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });


        var response = await client.GetAsync("/appointment");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
