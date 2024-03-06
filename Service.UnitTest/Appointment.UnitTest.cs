using AutoMapper;
using Moq;
using NewDogTinder.EFDataAccessLibrary.Models;
using NewDogTinder.Profile;
using NewDogTinder.Repository.IRepositories;
using NewDogTinder.Services.Service;
using Xunit;

namespace Service.UnitTest
{
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
                    Time = DateTime.Now,
                    Place = new Place
                    {
                        PlaceId = 1,
                        Address = "Ockenburg"
                    },
                    Dog =

                        new Dog()
                        {
                            DogId = 1,
                            Name = "Diablolik",
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
                    Time = DateTime.Now,
                    Place = new Place
                    {
                        PlaceId = 1,
                        Address = "Ockenburg"
                    },
                    Dog =

                        new Dog()
                        {
                            DogId = 1,
                            Name = "Diablolik",
                            Breed = "German Shorthair Pointer",
                            Owner = new Owner()
                            {
                                Name = "Ivan",
                                OwnerId = 1
                            }
                        }
                }
             };

            var appointmentRepositoryMock = new Mock<IAppointmentRepository>();
            appointmentRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(appointmentsData);
            AppointmentService = new AppointmentService(appointmentRepositoryMock.Object, Mapper);
        }

        [Fact]
        public async Task AppointmentsGetAllTestMapping()
        {
            // Act
            var appointmentViewModel = await AppointmentService.GetAppointments();

            // Assert
            Assert.Equal(2, appointmentViewModel.Count);
            Assert.Equal("Diablolik", appointmentViewModel[1].Dog.Name);
            Assert.Equal("Ockenburg", appointmentViewModel[1].Place.Address);
        }

    }
}

