using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace convert_shop.Models
{
    public partial class ProductColor
    {
        public ProductColor()
        {
            this.Products = new HashSet<Product>();
            this.color_id = GenerateColorId();
            this.createdAt = DateTime.Now;
            this.updatedAt = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(10, ErrorMessage = "Color ID cannot be longer than 10 characters.")]
        public string color_id { get; set; }

        [Required(ErrorMessage = "Color name is required.")]
        [StringLength(50, ErrorMessage = "Color name cannot exceed 50 characters.")]
        public string color_name { get; set; }

        [Required(ErrorMessage = "Color code is required.")]
        [StringLength(7, ErrorMessage = "Color code must be in HEX format (e.g., #FFFFFF).")]
        public string color_code { get; set; }

        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        private string GenerateColorId()
        {
            return "C" + Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
        }
    }
}
