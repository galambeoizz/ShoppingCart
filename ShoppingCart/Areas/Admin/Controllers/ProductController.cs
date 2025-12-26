using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;
using ShoppingCart.Repository;

namespace ShoppingCart.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly DataContext _dataContext;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public ProductController(DataContext context, IWebHostEnvironment webHostEnvironment)
		{
			_dataContext = context;
			_webHostEnvironment = webHostEnvironment;
		}
		public async Task<IActionResult> Index()
		{
			return View(await _dataContext.Products.OrderByDescending(x => x.Id).Include(x => x.Category).Include(x => x.Brand).ToListAsync());
		}

		[HttpGet]
		public IActionResult Create()
		{
			ViewBag.Categories = new SelectList(_dataContext.Categories, "Id", "Name");
			ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "Name");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ProductModel product)
		{
			ViewBag.Categories = new SelectList(_dataContext.Categories, "Id", "Name", product.CategoryId);
			ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "Name", product.BrandId);
			if (ModelState.IsValid)
			{
				product.Slug = product.Name.Replace(" ", "-");
				var slug = await _dataContext.Products.FirstOrDefaultAsync(x => x.Slug == product.Slug);
				if (slug != null)
				{
					ModelState.AddModelError("", "This product is already created!");
					return View(product);
				}
				else
				{
					if (product.ImageUpload != null)
					{
						string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
						string uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
						string filePath = Path.Combine(uploadsFolder, uniqueFileName);

						FileStream fileStream = new FileStream(filePath, FileMode.Create);
						await product.ImageUpload.CopyToAsync(fileStream);
						fileStream.Close();

						product.Image = uniqueFileName;
					}
				}
				_dataContext.Products.Add(product);
				await _dataContext.SaveChangesAsync();
				TempData["success"] = "Product created successfully.";
				return RedirectToAction("Index");
			}

			return View(product);
		}
	}
}
