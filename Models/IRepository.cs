namespace ExampleApp.Models
{
	public interface IRepository
	{
		IQueryable<Product> Products { get; }
	}
}

