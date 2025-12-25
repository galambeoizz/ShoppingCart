using ShoppingCart.Repository.Vadilation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Models
{
	public class ProductModel
	{
		[Key]
		public int Id { get; set; }
		[Required, MinLength(4, ErrorMessage = "Please enter Name")]
		public string Name { get; set; }
		[Required, MinLength(4, ErrorMessage = "Please enter Description")]
		public string Slug { get; set; }
		[Required, MinLength(4, ErrorMessage = "Please enter Description")]
		public string Description { get; set; }
		[Required]
		[Column(TypeName = "decimal(18,4)")]
		public decimal Price { get; set; }
		public int BrandId { get; set; }
		public int CategoryId { get; set; }
		public CategoryModel Category { get; set; }
		public BrandModel Brand { get; set; }
		public string Image {  get; set; }
		[NotMapped]
		[FileExtension]
		public IFormFile ImageUpload { get; set; }
	}
}
