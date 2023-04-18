using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prometheus.Api.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; } // individual or company

        [MaxLength(255)]
        public string Email { get; set; }

        [MaxLength(255)]
        public string PhoneNumber { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }

        public string Notes { get; set; }

        // The ICollection<Project> represents a collection of Projects that belong to a Customer. This is a navigation property representing the relationsip between a Customer and their Projects.
        // The ICollection<T> interaces allows for adding, removing, and enumerating items in the collection, as well as allowing a count of items.
        public virtual ICollection<Project> Projects { get; set; }
    }
}