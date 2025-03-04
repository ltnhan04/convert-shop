using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace convert_shop.Models
{
    public partial class CustomerRanking
    {
        public CustomerRanking()
        {
            this.Customers = new HashSet<Customer>();

            this.customer_ranking_id = GenerateCustomerRankingId();
            this.createdAt = DateTime.Now;
            this.updatedAt = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(10, ErrorMessage = "Customer Ranking ID cannot be longer than 10 characters.")]
        public string customer_ranking_id { get; set; }

        [Required(ErrorMessage = "Customer ranking name is required.")]
        [StringLength(50, ErrorMessage = "Customer ranking name cannot exceed 50 characters.")]
        public string customer_ranking_name { get; set; }

        [Required(ErrorMessage = "Required points are required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Required points must be a positive number.")]
        public int required_points { get; set; }

        [Required(ErrorMessage = "Voucher discount percentage is required.")]
        [Range(0, 100, ErrorMessage = "Voucher discount percentage must be between 0 and 100.")]
        public int voucher_discount_percentage { get; set; }

        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }

        private string GenerateCustomerRankingId()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
        }
    }
}
