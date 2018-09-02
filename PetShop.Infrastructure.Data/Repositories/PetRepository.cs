using System;
using System.Collections.Generic;
using System.Linq;
using Core.DomainService;
using Entities;

namespace Data.Repositories
{
	public class PetRepository:IPetRepository
	{
		static int id = 6;
		private List<Pet> _pets = new List<Pet>();


		public IEnumerable<Pet> ReadAllPets()
		{
			return FakeDB.Pets;
		}


		public Pet Add(Pet newPet)
		{
			newPet.Id = ++id;
			var petsFromDB = FakeDB.Pets.ToList();
			petsFromDB.Add(newPet);
			FakeDB.Pets = petsFromDB;
			FakeDB.Pets.ToList().Add(newPet);
			return newPet;
		}


		public Pet GetPetById(int id)
		{
			foreach (Pet pet in FakeDB.Pets.ToList())
			{
				if (pet.Id == id)
				{
					return pet;
				}
			}
			return null;
		}


		public Pet RemovePet(int id)
		{
			Pet selectedPet = this.GetPetById(id);
			if (selectedPet != null )
			{
				var petList = FakeDB.Pets.ToList();
				petList.Remove(selectedPet);
				FakeDB.Pets = petList;
				return selectedPet;
			}
			return null;
		}


		public Pet UpdatePet(Pet updatedPet)
		{
			Pet selectedPet = this.GetPetById(updatedPet.Id);
			if (selectedPet != null)
			{
				selectedPet.Name = updatedPet.Name;
				selectedPet.Type = updatedPet.Type;
				selectedPet.BirthDate = updatedPet.BirthDate;
				selectedPet.SoldDate = updatedPet.SoldDate;
				selectedPet.Color = updatedPet.Color;
				selectedPet.PreviousOwner = updatedPet.PreviousOwner;
				selectedPet.Price = updatedPet.Price;
				return selectedPet;
			}
			return null; ;
		}
	}
}
