using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace convert_shop.Models
{
    public partial class ProductImage
    {
        public ProductImage()
        {
            this.Products = new HashSet<Product>();
            this.image_id = GenerateImageId();
            this.createdAt = DateTime.Now;
            this.updatedAt = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(10, ErrorMessage = "Image ID cannot be longer than 10 characters.")]
        public string image_id { get; set; }

        [StringLength(255, ErrorMessage = "Image URL cannot exceed 255 characters.")]
        public string image_url_1 { get; set; }

        [StringLength(255, ErrorMessage = "Image URL cannot exceed 255 characters.")]
        public string image_url_2 { get; set; }

        [StringLength(255, ErrorMessage = "Image URL cannot exceed 255 characters.")]
        public string image_url_3 { get; set; }

        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        private string GenerateImageId()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
        }

        [NotMapped]
        public HttpPostedFileBase ImageFile1 { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile2 { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile3 { get; set; }
    }
}

