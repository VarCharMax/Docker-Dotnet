﻿namespace ExampleApp.Models
{
    public class ProductRepository: IRepository
	{
		private readonly ProductDbContext context;

		public ProductRepository(ProductDbContext ctx)
		{
			context = ctx;
		}

        public IQueryable<Product> Products => context.Products;
    }
}

