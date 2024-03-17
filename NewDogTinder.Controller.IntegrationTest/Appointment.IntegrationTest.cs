using NewDogTinder.EFDataAccessLibrary.Models;
using NewDogTinder.ViewModels;
using System.Net;
using System.Net.Http.Json;

namespace NewDogTinder.Controller.IntegrationTest;

[TestClass]
public class AppointmentIntegrationTest : Helpers.IntegrationTest
{

    [TestMethod, TestCategory("Integration")]
    public async Task GetAppointments_ReturnOK()
    {   // Arrange
        await InsertInDatabaseAsync(new Appointment()
        {
            Time = new DateTime(),
            Dog = new Dog() { 
                Breed = "GSP",
                Name = "Diabolik",
                Owner = new Owner()
                {
                    Name = "Ivan"
                }
            },
            Place = new Place()
            {
                Address = "Trondheim"
            }
        });
        await InsertInDatabaseAsync(new Appointment()
        {
            Time = new DateTime(),
            Dog = new Dog()
            {
                Breed = "Weimaraner",
                Name = "Eva",
                Owner = new Owner()
                {
                    Name = "Adelaide"
                }
            },
            Place = new Place()
            {
                Address = "Trondheim"
            }
        });

        // Act
        var response = await _client.GetAsync("/appointment");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var appointments = await ParseResponseContentAsync<List<Appointment>>(response);
        appointments.Should().HaveCount(2);

        var appointmentOne = appointments[0];
        appointmentOne.Should().NotBeNull();
        appointmentOne.AppointmentId.Should().BePositive();
        appointmentOne.Time.Should().Be(new DateTime());
        appointmentOne.Place.PlaceId.Should().BePositive();
        appointmentOne.Place.Address.Should().Be("Trondheim");
        appointmentOne.Dog.DogId.Should().BePositive();
        appointmentOne.Dog.Name.Should().Be("Diabolik");
        appointmentOne.Dog.Breed.Should().Be("GSP");
        appointmentOne.Dog.Owner.OwnerId.Should().BePositive();
        appointmentOne.Dog.Owner.Name.Should().Be("Ivan");

        var appointmentTwo = appointments[1];
        appointmentTwo.Should().NotBeNull();
        appointmentTwo.AppointmentId.Should().BePositive();
        appointmentTwo.Time.Should().Be(new DateTime());
        appointmentTwo.Place.PlaceId.Should().BePositive();
        appointmentTwo.Place.Address.Should().Be("Trondheim");
        appointmentTwo.Dog.DogId.Should().BePositive();
        appointmentTwo.Dog.Name.Should().Be("Eva");
        appointmentTwo.Dog.Breed.Should().Be("Weimaraner");
        appointmentTwo.Dog.Owner.OwnerId.Should().BePositive();
        appointmentTwo.Dog.Owner.Name.Should().Be("Adelaide");
    }

    [TestMethod, TestCategory("Integration")]
    public async Task GetAppointments_ReturnOK_EmptyList()
    {   
        // Act
        var response = await _client.GetAsync("/appointment");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var appointments = await ParseResponseContentAsync<List<Appointment>>(response);
        appointments.Should().HaveCount(0);
    }

    [TestMethod, TestCategory("Integration")]
    public async Task GetAppointment_ReturnOK()
    {   // Arrange
        var entityInserted = await InsertInDatabaseAsync(new Appointment()
        {
            Time = new DateTime(),
            Dog = new Dog()
            {
                Breed = "GSP",
                Name = "Diabolik",
                Owner = new Owner()
                {
                    Name = "Ivan"
                }
            },
            Place = new Place()
            {
                Address = "Trondheim"
            }
        });

        // Act
        var response = await _client.GetAsync($"/appointment/{entityInserted.AppointmentId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var appointment = await ParseResponseContentAsync<Appointment>(response);

        appointment.Should().NotBeNull();
        appointment.AppointmentId.Should().BePositive();
        appointment.Time.Should().Be(new DateTime());
        appointment.Place.PlaceId.Should().BePositive();
        appointment.Place.Address.Should().Be("Trondheim");
        appointment.Dog.DogId.Should().BePositive();
        appointment.Dog.Name.Should().Be("Diabolik");
        appointment.Dog.Breed.Should().Be("GSP");
        appointment.Dog.Owner.OwnerId.Should().BePositive();
        appointment.Dog.Owner.Name.Should().Be("Ivan");
    }

    [TestMethod, TestCategory("Integration")]
    public async Task GetAppointment_ReturnNotFound()
    {   // Arrange
        var entityInserted = await InsertInDatabaseAsync(new Appointment()
        {
            Time = new DateTime(),
            Dog = new Dog()
            {
                Breed = "GSP",
                Name = "Diabolik",
                Owner = new Owner()
                {
                    Name = "Ivan"
                }
            },
            Place = new Place()
            {
                Address = "Trondheim"
            }
        });

        // Act
        var response = await _client.GetAsync($"/appointment/10000");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [TestMethod, TestCategory("Integration")]
    public async Task PostAppointment_ReturnCreated()
    {   // Arrange
        var dogInserted = await InsertInDatabaseAsync(new Dog()
        {
            Breed = "GSP",
            Name = "Diabolik",
            Owner = new Owner()
            {
                Name = "Ivan"
            }
        });

        var placeInserted = await InsertInDatabaseAsync(new Place()
        {
            Address = "Trondheim"
        });

        var appointmentToSave = new AppointmentForInsertViewModel()
        {
            Time = new DateTime(),
            DogId = dogInserted.DogId,
            PlaceId = placeInserted.PlaceId
        };

        // Act
        var response = await _client.PostAsync($"/appointment", JsonContent.Create(appointmentToSave));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var appointment = await ParseResponseContentAsync<Appointment>(response);

        response.Headers.Location.ToString().Should().Contain($"/Appointment/{appointment.AppointmentId}", Exactly.Once());

        appointment.Should().NotBeNull();
        appointment.AppointmentId.Should().BePositive();
        appointment.Time.Should().Be(new DateTime());
        appointment.Place.PlaceId.Should().BePositive();
        appointment.Place.Address.Should().Be("Trondheim");
        appointment.Dog.DogId.Should().BePositive();
        appointment.Dog.Name.Should().Be("Diabolik");
        appointment.Dog.Breed.Should().Be("GSP");
        appointment.Dog.Owner.OwnerId.Should().BePositive();
        appointment.Dog.Owner.Name.Should().Be("Ivan");
    }

    [TestMethod, TestCategory("Integration")]
    public async Task PostAppointment_ReturnBadRequest()
    {   // Arrange
        var placeInserted = await InsertInDatabaseAsync(new Place()
        {
            Address = "Trondheim"
        });

        var appointmentToSave = new AppointmentForInsertViewModel()
        {
            Time = new DateTime(),
            DogId = -1,
            PlaceId = placeInserted.PlaceId
        };

        // Act
        var response = await _client.PostAsync($"/appointment", JsonContent.Create(appointmentToSave));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
