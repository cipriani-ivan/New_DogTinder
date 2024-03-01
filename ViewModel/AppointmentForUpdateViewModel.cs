using System.ComponentModel.DataAnnotations;

namespace NewDogTinder.ViewModels
{
    public class AppointmentForUpdateViewModel
    {
        public DateTime Time { get; set; }
        public int PlaceId { get; set; }
        public int DogId { get; set; }
	}
}
