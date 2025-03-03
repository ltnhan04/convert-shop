using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace convert_shop.Models
{
    public partial class Product
    {
        public Product()
        {
            this.OrderItems = new HashSet<OrderItem>();
            this.Reviews = new HashSet<Review>();
            this.Coupons = new HashSet<Coupon>();

            this.product_id = GenerateProductId();
            this.createdAt = DateTime.Now;
            this.updatedAt = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(10, ErrorMessage = "Product ID cannot be longer than 10 characters.")]
        public string product_id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public string product_name { get; set; }

        public string product_description { get; set; }

        [Required(ErrorMessage = "Product price is required.")]
        public int product_price { get; set; }

        [Required]
        public string category_id { get; set; }
        [Required]
        public string image_id { get; set; }
        [Required]
        public string color_id { get; set; }
        [Required]
        public string status_id { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative.")]
        public int quantity { get; set; }

        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        [Required]
        public string slug { get; set; }
        public virtual Category Category { get; set; }
        public virtual ProductColor ProductColor { get; set; }
        public virtual ProductImage ProductImage { get; set; }
        public virtual ProductStatu ProductStatu { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Coupon> Coupons { get; set; }

        private string GenerateProductId()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
        }
    }
}
