using System;
using System.ComponentModel.DataAnnotations;

namespace Prometheus.Api.Models
{
    public class Revenue
    {
        [Key]
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public int InvoiceId { get; set; }

        public decimal AmountReceived { get; set; }

        public DateTime DateReceived { get; set; }

        public virtual Project Project { get; set; }

        public virtual Invoice Invoice { get; set; }
    }
}