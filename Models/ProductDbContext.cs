using Microsoft.EntityFrameworkCore;

namespace ExampleApp.Models
{
    public class ProductDbContext :DbContext
	{
		public ProductDbContext() { }


        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
		{
		}

		public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var envs = Environment.GetEnvironmentVariables();

            var host = envs["DBHOST"] ?? "localhost";
            var port = envs["DBPORT"] ?? "3306";
            var password = envs["DBPASSWORD"] ?? "mysecret";

            var connectionString = $"server={host};port={port};database=products;user=root;password={password};";

            options.UseMySQL(connectionString);
        }
    }
}
