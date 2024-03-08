using NewDogTinder.Controller.IntegrationTest.Helpers;
using System.Net;

namespace NewDogTinder.Controller.IntegrationTest;

[Collection("Sequential")]
public class Appointment : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;

    public Appointment(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
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
