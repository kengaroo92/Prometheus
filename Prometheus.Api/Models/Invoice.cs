using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Prometheus.Api.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int ProjectId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; }

        public DateTime Date { get; set; }

        public DateTime DueDate { get; set; }

        public JsonDocument LineItems { get; set; }

        public decimal Subtotal { get; set; }

        public decimal Tax { get; set; }

        public decimal TotalAmount { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Project Project { get; set; }
    }
}