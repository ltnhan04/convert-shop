using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace convert_shop.Models
{
    public partial class Customer
    {
        public Customer()
        {
            this.Messages = new HashSet<Message>();
            this.Messages1 = new HashSet<Message>();
            this.Orders = new HashSet<Order>();
            this.Reviews = new HashSet<Review>();

            this.customer_id = GenerateCustomerId();
            this.createdAt = DateTime.Now;
            this.updatedAt = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(10, ErrorMessage = "Customer ID cannot be longer than 10 characters.")]
        public string customer_id { get; set; }

        [Required(ErrorMessage = "Customer name is required.")]
        [StringLength(100, ErrorMessage = "Customer name cannot exceed 100 characters.")]
        public string customer_name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(100)]
        public string email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        [StringLength(15)]
        public string phone_number { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(255)]
        [NotMapped]
        public string password { get; set; }

        [StringLength(255, ErrorMessage = "Address cannot exceed 255 characters.")]
        public string address { get; set; }

        public string customer_type_id { get; set; }
        public string role_id { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Loyalty points must be a positive number.")]
        public int loyaltyPoints { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Used points must be a positive number.")]
        public int usedPoint { get; set; }

        public string customer_ranking_id { get; set; }

        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }

        // Navigation properties
        public virtual CustomerRanking CustomerRanking { get; set; }
        public virtual CustomerType CustomerType { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Message> Messages1 { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

        private string GenerateCustomerId()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
        }
    }
}
