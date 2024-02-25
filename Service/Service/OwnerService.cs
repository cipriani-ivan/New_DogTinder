﻿using AutoMapper;
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

		public async Task<IList<OwnerViewModel>> GetOwners()
		{
			var owners = await OwnerRepository.GetAllAsync();
			return Mapper.Map<List<OwnerViewModel>>(owners);
		}

		public async Task InsertOwner(OwnerViewModel ownerViewModel)
		{
			var owner = Mapper.Map<Owner>(ownerViewModel);
			OwnerRepository.Insert(owner);
			await OwnerRepository.SaveAsync();
		}
	}
}
