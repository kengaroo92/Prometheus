using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prometheus.Api.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public virtual Customer Customer { get; set; }
    }
}