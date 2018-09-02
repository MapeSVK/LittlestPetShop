using System;
using System.Collections.Generic;
using Core.ApplicationService;
using Core.ApplicationService.Implementations;
using Entities;

namespace ConsoleApplication
{
	public class Printer : IPrinter
	{
		readonly IPetService _iPetService;

		public Printer(IPetService iPetService)
		{
			_iPetService = iPetService;
			StartPrinting(); //starts menu with options
		}


		//navigation menu
		public void StartPrinting()
		{
			string[] menuItems = {
				"List All Pets",
				"Search Pets By Type",
				"Add Pet",
				"Delete Pet",
				"Update Pet",
				"Sort Pets By Price",
				"5 Cheapest Pets",
				"Exit"
			};

			var selection = ShowMenu(menuItems);

			while (selection != 8)
			{
				switch (selection)
				{
					case 1:
						ShowAllPets(_iPetService.GetPets());
						break;
					case 2:
						ShowPetsWithType();
						break;
					case 3:
						Pet Pet = CreatePet(1);
						_iPetService.AddPet(Pet);
						break;
					case 4:
						DeletePet();
						break;
					case 5:
						UpdatePet();
						break;
					case 6:
						SortPetsByPrice();
						break;
					case 7:
						ShowFiveCheapestPets();
						break;
					case 8:
						Environment.Exit(-1);
						break;
				}
				selection = ShowMenu(menuItems);
			}

			Console.ReadLine();
		}


		/*** Shows menu with all command user can do ***/
		int ShowMenu(string[] menuItems)
		{
			Console.WriteLine("Select from the menu\n");

			for (var i = 0; i < menuItems.Length; i++)
			{
				Console.WriteLine($"{(i + 1)}: {menuItems[i]}");
			}

			int selection;

			while (!int.TryParse(Console.ReadLine(), out selection)
				   || selection < 1
				   || selection > 8)
			{
				Console.WriteLine("Please select a number between 1-8");
			}

			return selection;
		}

		/*** Shows all pets which are in the list ***/
		private void ShowAllPets(List<Pet> petList)
		{
			foreach (var pet in petList)
			{
				Console.WriteLine($"ID: {pet.Id}, Name: {pet.Name}, Type: {pet.Type}, Birthdate: {pet.BirthDate.ToString("dd/MM/yyyy")}, SoldDate: {pet.SoldDate.ToString("dd/MM/yyyy")}, Color: {pet.Color}, PreviousOwner: {pet.PreviousOwner}, Price: {pet.Price}");

			}
			Console.ReadLine();
		}


		Pet CreatePet(int id)
		{
			var name = WriteAndReadNextLine("Name: ");
			var type = WriteAndReadNextLine("Type: ");
			var birthday = WriteAndReadNextLineDateTime("Birthday: ");
			var soldDate = WriteAndReadNextLineDateTime("SoldDate: ");
			var color = WriteAndReadNextLine("Color: ");
			var previousOwner = WriteAndReadNextLine("PreviousOwner: ");
			double price = WriteAndReadNextLineDouble("Price: ");
			return new Pet()
			{
				Id = id,
				Name = name,
				Type = type,
				BirthDate = birthday,
				SoldDate = soldDate,
				Color = color,
				PreviousOwner = previousOwner,
				Price = price
			};

		}

		/*** Delete a pet from the list ***/
		void DeletePet()
		{
			int selectedPetId = WriteAndReadNextLineInt("Write ID of the pet you want to remove from the list!\n");
			if (_iPetService.FindPetById(selectedPetId) != null)
			{
				_iPetService.DeletePet(selectedPetId);
			}
			else
			{
				Console.WriteLine("Pet with this ID does not exist in the pet list!");
			}
		}

		/*** Update pet - does changes ***/
		void UpdatePet()
		{
			int selectedPetId = WriteAndReadNextLineInt("Write ID of the pet you want to update!");
			if (_iPetService.FindPetById(selectedPetId) != null)
			{
				Pet Pet = CreatePet(selectedPetId);
				_iPetService.UpdatePet(Pet);
			}
			else
			{
				Console.WriteLine("Pet with this ID does not exist");
			}
		}

		/*** Shows pets with chosen type ***/
		void ShowPetsWithType()
		{
			var type = WriteAndReadNextLine("Type: ");
			List<Pet> listOfPets = _iPetService.GetPetsByType(type);
			if (listOfPets == null)
			{
				Console.WriteLine("There are no pets with this type\n");
			}
			else
			{
				ShowAllPets(listOfPets);
			}
		}

		/*** Sort pets by price - lowest price on top ***/
		void SortPetsByPrice()
		{
			List<Pet> petList; 
			petList = _iPetService.SortPetsByPrice(_iPetService.GetPets());
			ShowAllPets(petList);
			Console.ReadLine();
		}

		/*** Shows 5 cheapest pets in the Little Pet Shop ***/
		void ShowFiveCheapestPets() 
		{
			List<Pet> listWithCheapestPets;
			listWithCheapestPets = _iPetService.SortPetsByPrice(_iPetService.GetPets());
			ShowAllPets(_iPetService.GetFiveCheapestPets(listWithCheapestPets));
			Console.ReadLine();
		}

		/*** Read and Write section - trying to parse input from the user ***/
		#region read and write
		string WriteAndReadNextLine(string text)
		{
			Console.Write(text);
			return Console.ReadLine();
		}

		double WriteAndReadNextLineDouble(string text)
		{
			Console.Write(text);
			double price;
			while (!double.TryParse(Console.ReadLine(), out price))
			{
				Console.WriteLine("Please write number!");
			}
			return price;
		}

		DateTime WriteAndReadNextLineDateTime(string text)
		{
			Console.Write(text);
			DateTime dateTime;
			while (!DateTime.TryParse(Console.ReadLine(), out dateTime))
			{
				Console.WriteLine("Please write date!");
			}
			return dateTime;
		}

		int WriteAndReadNextLineInt(string text)
		{
			Console.Write(text);
			int id;
			while (!int.TryParse(Console.ReadLine(), out id))
			{
				Console.WriteLine("Please write right ID!");
			}
			return id;
		}
		#endregion

	}
}
