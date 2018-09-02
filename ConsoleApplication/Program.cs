using System;
using Core.ApplicationService;
using Core.ApplicationService.Implementations;
using Core.DomainService;
using Data;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
			FakeDB.InitData();

			var serviceCollection = new ServiceCollection();
			serviceCollection.AddScoped<IPetRepository, PetRepository>();
			serviceCollection.AddScoped<IPetService, PetService>();
			serviceCollection.AddScoped<IPrinter, Printer>();

			var serviceProvider = serviceCollection.BuildServiceProvider();


			var printer = serviceProvider.GetRequiredService<IPrinter>();
			printer.StartPrinting();

			Console.ReadLine();
		}
    }
}
