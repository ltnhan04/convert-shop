using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace convert_shop.Models
{
    public partial class ProductStatu
    {
        public ProductStatu()
        {
            this.Products = new HashSet<Product>();
            this.status_id = GenerateStatusId();
            this.createdAt = DateTime.Now;
            this.updatedAt = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(10, ErrorMessage = "Status ID cannot be longer than 10 characters.")]
        public string status_id { get; set; }

        [Required(ErrorMessage = "Status name is required.")]
        [StringLength(50, ErrorMessage = "Status name cannot exceed 50 characters.")]
        public string status_name { get; set; }

        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        private string GenerateStatusId()
        {
            return "ST" + Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
        }
    }
}
