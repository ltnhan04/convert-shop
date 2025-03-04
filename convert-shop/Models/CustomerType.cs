using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace convert_shop.Models
{
    public partial class CustomerType
    {
        public CustomerType()
        {
            this.Customers = new HashSet<Customer>();

            this.customer_type_id = GenerateCustomerTypeId();
            this.createdAt = DateTime.Now;
            this.updatedAt = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(10, ErrorMessage = "Customer Type ID cannot be longer than 10 characters.")]
        public string customer_type_id { get; set; }

        [Required(ErrorMessage = "Customer type is required.")]
        [StringLength(50, ErrorMessage = "Customer type cannot exceed 50 characters.")]
        public string customer_type { get; set; }

        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }

        private string GenerateCustomerTypeId()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
        }
    }
}
