using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Repository.Vadilation
{
	public class FileExtensionAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value is not null)
			{
				var file = value as IFormFile;
				var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
				var extension = Path.GetExtension(file!.FileName).ToLower();
				if (!allowedExtensions.Contains(extension))
				{
					return new ValidationResult("Invalid file type. Only .jpg, .jpeg, .png are allowed.");
				}
			}
			return ValidationResult.Success;
		}
	}
}
