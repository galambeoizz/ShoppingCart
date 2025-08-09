using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;
using ShoppingCart.Repository;

namespace ShoppingCart.Controllers
{
	public class CategoryController : Controller
	{
		private readonly DataContext _dataContext;
		public CategoryController(DataContext context)
		{
			_dataContext = context;
		}
		public async Task<IActionResult> Index(string Slug = "")
		{
			var category = _dataContext.Categories.Where(x=>x.Slug == Slug).FirstOrDefault();
			if (category == null)
			{
				return RedirectToAction("Index");
			}
			var productByCategory = _dataContext.Products.Include("Category").Include("Brand").Where(x => x.CategoryId == category.Id);

			return View(await productByCategory.OrderByDescending(x => x.Id).ToListAsync());
		}
	}
}
