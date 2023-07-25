using System;
using ExampleApp.Models;

namespace ExampleApp.Models
{
	public class DummyRepository : IRepository
	{
		private static Product[] DummyData = {
			new Product { Name = "Prod1",  Category = "Cat1", Price = 100 },
			new Product { Name = "Prod2",  Category = "Cat1", Price = 100 },
			new Product { Name = "Prod3", Category = "Cat2", Price = 100 },
			new Product { Name = "Prod4", Category = "Cat2", Price = 100 },
  };

		public DummyRepository()
		{
		}

		public IQueryable<Product> Products => DummyData.AsQueryable();
    }
}

