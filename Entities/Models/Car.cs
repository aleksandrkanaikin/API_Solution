using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Car
    {
        [Column("CarId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Car brend is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Brend { get; set; }
        [Required(ErrorMessage = "Car model is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for rhe Address is 30 characte")]
        public string Model { get; set; }

        [ForeignKey(nameof(Driver))]
        public Guid DriverId { get; set; }
        public Driver Driver { get; set; }

    }
}
