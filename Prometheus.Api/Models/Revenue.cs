using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prometheus.Api.Models
{
    public class Revenue
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }

        [ForeignKey("Invoice")]
        public int InvoiceId { get; set; }

        public decimal AmountReceived { get; set; }

        public DateTime DateReceived { get; set; }

        public virtual Project Project { get; set; }

        public virtual Invoice Invoice { get; set; }
    }
}