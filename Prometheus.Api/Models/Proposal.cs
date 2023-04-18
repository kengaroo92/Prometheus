using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Prometheus.Api.Models
{
    public class Proposal
    {
        // The Key attribute is used to mark a property as the primary key of an entity.
        [Key]
        public int Id { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }

        // The Required attribute is used to mark a property as required.
        [Required]
        // The MaxLength attribute is used to specify the maximum length of a string property.
        [MaxLength(50)]
        public string Status { get; set; }

        public DateTime Date { get; set; }

        // The JsonDocument represents a JSON object or array. It allows you to store complex data structures in a single database column. Comes from 'System.Text.Json'.
        public JsonDocument LineItems { get; set; }

        public decimal Subtotal { get; set; }

        public decimal Tax { get; set; }

        public decimal TotalAmount { get; set; }

        // The virtual keyword enables lazy loading provided by entity framework. It allows data to be loaded automatically from the database when it is accessed.
        public virtual Customer Customer { get; set; }

        public virtual Project Project { get; set; }
    }
}