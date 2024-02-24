using AutoMapper;
using NewDogTinder.EFDataAccessLibrary.Models;
using NewDogTinder.Repository.IRepositories;
using NewDogTinder.Services.IService;
using NewDogTinder.ViewModels;

namespace NewDogTinder.Services.Service
{
    public class DogService : IDogService
	{
		private IDogRepository DogRepository { get; }
		private readonly IMapper Mapper;

		public DogService(IDogRepository dogRepository, IMapper mapper)
		{
			DogRepository = dogRepository;
			Mapper = mapper;
		}

		public async Task<IList<DogViewModel>> GetDogs()
		{
			var dogs = await DogRepository.GetAllAsync(includeProperties: "Owner");
			return Mapper.Map<List<DogViewModel>>(dogs);
		}

		public async Task InsertDog(DogViewModel dogViewmodel)
		{
			var dog = Mapper.Map<Dog>(dogViewmodel);
			DogRepository.Insert(dog, dogViewmodel.OwnerId);
			await DogRepository.SaveAsync();
		}
	}
}