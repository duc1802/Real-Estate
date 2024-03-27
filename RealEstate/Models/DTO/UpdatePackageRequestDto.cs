using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models.DTO
{
    public class UpdatePackageRequestDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be a maxinum of 100 characters")]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int DurationInDays { get; set; }
    }
}
