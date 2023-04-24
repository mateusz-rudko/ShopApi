using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class ProductToUpdateDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(200)]
        public string Description { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^[1-9]\d*(\.\d{1,2})?$", ErrorMessage = "Price must have up to two decimal places and cannot be a negative number!")]
        public decimal Price { get; set; }

    }
}
