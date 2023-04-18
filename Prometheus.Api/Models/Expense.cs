using System;
using System.ComponentModel.DataAnnotations;

namespace Prometheus.Api.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }

        public int ProjectId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Category { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public decimal AmountSpent { get; set; }

        public DateTime DateSpent { get; set; }

        public virtual Project Project { get; set; }
    }
}