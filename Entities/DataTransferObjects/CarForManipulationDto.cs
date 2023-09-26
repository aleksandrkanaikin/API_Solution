using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public abstract class CarForManipulationDto
    {

        [Required(ErrorMessage = "Car brend is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Brend is 20 characters.")]
        public string Brend { get; set; }

        [Required(ErrorMessage = "Car model is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for rhe Model is 30 characte")]
        public string Model { get; set; }
    }
}
