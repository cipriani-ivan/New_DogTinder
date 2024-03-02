using AutoMapper;
using NewDogTinder.EFDataAccessLibrary.Models;
using NewDogTinder.Repository.IRepositories;
using NewDogTinder.Services.IService;
using NewDogTinder.ViewModels;

namespace NewDogTinder.Services.Service
{
    public class OwnerService: IOwnerService
	{
		private IOwnerRepository OwnerRepository { get; }
		private readonly IMapper Mapper;

		public OwnerService(IOwnerRepository ownerRepository, IMapper mapper)
		{
			OwnerRepository = ownerRepository;
			Mapper = mapper;
		}

        public async Task<OwnerViewModel> GetOwner(int ownerId)
        {
            var owner = await OwnerRepository.Get(ownerId);
            return Mapper.Map<OwnerViewModel>(owner);
        }

        public async Task<IList<OwnerViewModel>> GetOwners()
		{
			var owners = await OwnerRepository.GetAllAsync();
			return Mapper.Map<List<OwnerViewModel>>(owners);
		}

		public async Task<Owner> InsertOwner(OwnerForInsertViewModel ownerViewModel)
		{
			var owner = Mapper.Map<Owner>(ownerViewModel);
			var ownerCreated = OwnerRepository.Insert(owner);
			await OwnerRepository.SaveAsync();
			return ownerCreated;
        }
	}
}
