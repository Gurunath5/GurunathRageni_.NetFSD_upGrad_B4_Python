using System.ComponentModel.DataAnnotations;

namespace EMS.API.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; }

        [Required, MaxLength(200)]
        public string Email { get; set; }

        [Required, MaxLength(15)]
        public string Phone { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Designation { get; set; }

        [Required]
        public decimal Salary { get; set; }

        [Required]
        public DateTime JoinDate { get; set; }

        [Required]
        public string Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}