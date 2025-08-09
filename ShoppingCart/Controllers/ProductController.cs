using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Repository;

namespace ShoppingCart.Controllers
{
	public class ProductController : Controller
	{
		private readonly DataContext _dataContext;
		public ProductController(DataContext context)
		{
			_dataContext = context;
		}
		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> Details(int Id)
		{
			if (Id == 0)
			{
				return RedirectToAction("Index");
			}
			var productById = _dataContext.Products.Include("Category").Include("Brand").Where(x => x.Id == Id).FirstOrDefaultAsync();
			return View(await productById);
		}
	}
}
