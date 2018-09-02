using System;
using System.Collections.Generic;
using Entities;

namespace Core.ApplicationService
{
	public interface IPetService
	{
		List<Pet> GetPets();

		//create new pet with properties
		Pet CreatePet(
			string Name,
			string Type,
			DateTime BirthDate,
			DateTime SoldDate,
			string Color,
			string PreviousOwner,
			double Price
		);
		Pet AddPet(Pet newPet);

		//Delete pet
		Pet DeletePet(int id);

		//Find pet by id
		Pet FindPetById(int id);

		//Update pet
		Pet UpdatePet(Pet selectedPet);

		//Getting pets by writing type
		List<Pet> GetPetsByType(string type);

		//Sort pets by price - lowest on the top
		List<Pet> SortPetsByPrice(List<Pet> petList);

		//Get 5 cheapest pets in the Little Pet Shop
		List<Pet> GetFiveCheapestPets(List<Pet> petList);
	}

}
