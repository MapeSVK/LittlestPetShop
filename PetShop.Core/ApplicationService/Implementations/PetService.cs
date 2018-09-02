using System;
using System.Collections.Generic;
using System.Linq;
using Core.DomainService;
using Entities;

namespace Core.ApplicationService.Implementations
{
	public class PetService : IPetService
	{
		readonly IPetRepository _iPetRepository;

		public PetService(IPetRepository petRepository)
		{
			_iPetRepository = petRepository;
		}


		// Get all pets - list of pets
		public List<Pet> GetPets()
		{
			return _iPetRepository.ReadAllPets().ToList();
		}


		public Pet AddPet(Pet newPet)
		{
			_iPetRepository.Add(newPet);
			return newPet;
		}

		public Pet CreatePet(string Name, string Type, DateTime BirthDate, DateTime SoldDate, string Color, string PreviousOwner, double Price)
		{
			return new Pet()
			{
				Name = Name,
				Type = Type,
				BirthDate = BirthDate,
				SoldDate = SoldDate,
				Color = Color,
				PreviousOwner = PreviousOwner,
				Price = Price
			};
		}



		public Pet DeletePet(int id)
		{
			return _iPetRepository.RemovePet(id);

		}

		public Pet FindPetById(int id)
		{
			return _iPetRepository.GetPetById(id);
		}

		public Pet UpdatePet(Pet selectedPet)
		{
			return _iPetRepository.UpdatePet(selectedPet);
		}

		public List<Pet> GetPetsByType(string type)
		{
			List<Pet> petsWithType = new List<Pet>();
			foreach (Pet pet in _iPetRepository.ReadAllPets().ToList())
			{
				if (pet.Type.ToUpper() == type.ToUpper())
				{
					petsWithType.Add(pet);
				}
			}
			if (petsWithType.Count == 0)
			{
				return null;
			}
			return petsWithType;
		}

		public List<Pet> SortPetsByPrice(List<Pet> petList)
		{
			return petList.OrderBy(pet => pet.Price).ToList();
		}

		public List<Pet> GetFiveCheapestPets(List<Pet> petList)
		{
			int i = 0;
			List<Pet> cheapestPetsList = new List<Pet>();
			while (i < petList.Count() && i < 5)
			{
				cheapestPetsList.Add(petList[i++]);
			}
			return cheapestPetsList;
		}
	}
}
