using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;

namespace ShoppingCart.Repository
{
	public class SeedData
	{
		public static void SeedingData(DataContext _context)
		{
			_context.Database.Migrate();
			if (!_context.Products.Any())
			{
				CategoryModel macbook = new CategoryModel()
				{
					Name = "Macbook",
					Slug = "macbook",
					Description = "Macbook is large brand in the world",
					Status = 1,
				};
				CategoryModel pc = new CategoryModel()
				{
					Name = "Pc",
					Slug = "pc",
					Description = "Pc is large brand in the world",
					Status = 1,
				};
				BrandModel apple = new BrandModel()
				{
					Name = "Apple",
					Slug = "apple",
					Description = "Apple is large brand in the world",
					Status = 1,
				};
				BrandModel samsung = new BrandModel()
				{
					Name = "Samsung",
					Slug = "samsung",
					Description = "Samsung is large brand in the world",
					Status = 1,
				};
				_context.Products.AddRange(
					new ProductModel()
					{
						Name = "Macbook",
						Slug = "macbook",
						Description = "Macbook is the Best",
						Image = "MacBook.jpg",
						Category = macbook,
						Price = 1233,
						Brand = apple
					},
					new ProductModel()
					{
						Name = "Pc",
						Slug = "pc",
						Description = "Pc is the Best",
						Image = "PC.jpg",
						Category = pc,
						Price = 989,
						Brand = samsung
					}
				);
				_context.SaveChanges();
			}
		}
	}
}
