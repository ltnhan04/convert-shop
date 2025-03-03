using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace convert_shop.Models
{
    public partial class Category
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
            this.category_id = GenerateCategoryId();
            this.createdAt = DateTime.Now;
            this.updatedAt = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(10, ErrorMessage = "Category ID cannot be longer than 10 characters.")]
        public string category_id { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(50, ErrorMessage = "Category name cannot exceed 50 characters.")]
        public string category_name { get; set; }

        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        private string GenerateCategoryId()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
        }
    }
}
