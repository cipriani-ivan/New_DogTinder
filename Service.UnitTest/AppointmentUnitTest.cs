using AutoMapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NewDogTinder.EFDataAccessLibrary.Models;
using NewDogTinder.Profile;
using NewDogTinder.Repository.IRepositories;
using NewDogTinder.Services.Service;

namespace Service.UnitTest;

[TestClass]
public class AppointmentUnitTest
{
    private readonly AppointmentService AppointmentService;
    private static IMapper Mapper;

    public AppointmentUnitTest()
    {
        // Arrange
        var config = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new NewDogTinderProfile());
        });

        Mapper = new Mapper(config);

        var appointmentsData = new List<Appointment>() {
            new Appointment{
                AppointmentId = 1,
                Time = new DateTime(),
                Place = new Place
                {
                    PlaceId = 1,
                    Address = "Nonnegate"
                },
                Dog = new Dog()
                {
                    DogId = 1,
                    Name = "Diabolik",
                    Breed = "German Shorthair Pointer",
                    Owner = new Owner()
                    {
                        Name = "Ivan",
                        OwnerId = 1
                    }
                }
            },
            new Appointment{
                AppointmentId = 2,
                Time = new DateTime(),
                Place = new Place
                {
                    PlaceId = 1,
                    Address = "Nonnegate"
                },
                Dog = new Dog()
                {
                    DogId = 1,
                    Name = "Eva",
                    Breed = "Weimaraner",
                    Owner = new Owner()
                    {
                        Name = "Adelaide",
                        OwnerId = 2
                    }
                }
            }
        };

        var appointmentRepositoryMock = new Mock<IAppointmentRepository>();
        appointmentRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(appointmentsData);
        AppointmentService = new AppointmentService(appointmentRepositoryMock.Object, Mapper);
    }

    [TestMethod]
    public async Task AppointmentsGetAllTestMapping()
    {
        // Act
        var appointmentsViewModel = await AppointmentService.GetAppointments();

        // Assert
        appointmentsViewModel.Should().HaveCount(2);
        var appointmentOne = appointmentsViewModel[0];
        appointmentOne.Should().NotBeNull();
        appointmentOne.AppointmentId.Should().BePositive();
        appointmentOne.Time.Should().Be(new DateTime());
        appointmentOne.Place.PlaceId.Should().BePositive();
        appointmentOne.Place.Address.Should().Be("Nonnegate");
        appointmentOne.Dog.DogId.Should().BePositive();
        appointmentOne.Dog.Name.Should().Be("Diabolik");
        appointmentOne.Dog.Breed.Should().Be("German Shorthair Pointer");
        appointmentOne.Dog.Owner.OwnerId.Should().BePositive();
        appointmentOne.Dog.Owner.Name.Should().Be("Ivan");

        var appointmentTwo = appointmentsViewModel[1];
        appointmentTwo.Should().NotBeNull();
        appointmentTwo.AppointmentId.Should().BePositive();
        appointmentTwo.Time.Should().Be(new DateTime());
        appointmentTwo.Place.PlaceId.Should().BePositive();
        appointmentTwo.Place.Address.Should().Be("Nonnegate");
        appointmentTwo.Dog.DogId.Should().BePositive();
        appointmentTwo.Dog.Name.Should().Be("Eva");
        appointmentTwo.Dog.Breed.Should().Be("Weimaraner");
        appointmentTwo.Dog.Owner.OwnerId.Should().BePositive();
        appointmentTwo.Dog.Owner.Name.Should().Be("Adelaide");
    }
}

