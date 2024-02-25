using NewDogTinder.EFDataAccessLibrary.Models;
using NewDogTinder.ViewModels;

namespace NewDogTinder.Profile
{
    public class NewDogTinderProfile: AutoMapper.Profile
	{
		public NewDogTinderProfile()
		{
			CreateMap<Appointment, AppointmentViewModel>()
				.ForMember(dest => dest.Place,
					opt => opt.MapFrom(scr => new PlaceViewModel()
					{
						PlaceId = scr.Place.PlaceId, Address = scr.Place.Address
					}))
				.ForMember(dest => dest.Dog,
					opt => opt.MapFrom(scr => new DogViewModel()
						{
							DogId = scr.Dog.DogId, Name = scr.Dog.Name, Breed = scr.Dog.Breed,
							Owner = new OwnerViewModel() {OwnerId = scr.Dog.Owner.OwnerId, Name = scr.Dog.Owner.Name}
						}));
			CreateMap<AppointmentViewModel, Appointment>();
			CreateMap<Owner, OwnerViewModel>();
			CreateMap<OwnerViewModel, Owner>();
			CreateMap<Place, PlaceViewModel>();
			CreateMap<PlaceViewModel, Place>();
			CreateMap<Dog, DogViewModel>();
			CreateMap<DogViewModel, Dog>();
		}			
	}	
}
