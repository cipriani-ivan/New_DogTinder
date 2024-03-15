using FluentAssertions;
using System.Net;

namespace NewDogTinder.Controller.IntegrationTest;

[TestClass]
public class Appointment : Helpers.IntegrationTest
{

    [TestMethod, TestCategory("Integration")]
    public async Task AppointmentTodoWithValidParameters()
    {   // Arrange
        var client = _factory.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

        // Act
        var response = await client.GetAsync("/appointment");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
