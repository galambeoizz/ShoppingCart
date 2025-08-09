using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Repository;

namespace ShoppingCart.Controllers
{
	public class BrandController : Controller
	{
		private readonly DataContext _dataContext;
		public BrandController(DataContext context)
		{
			_dataContext = context;
		}
		public async Task<IActionResult> Index(string Slug = "")
		{
			var brand = _dataContext.Brands.Where(x => x.Slug == Slug).FirstOrDefault();
			if (brand == null)
			{
				return RedirectToAction("Index");
			}
			var productByBrand = _dataContext.Products.Include("Category").Include("Brand").Where(x => x.BrandId == brand.Id);

			return View(await productByBrand.OrderByDescending(x => x.Id).ToListAsync());
		}
	}
}
