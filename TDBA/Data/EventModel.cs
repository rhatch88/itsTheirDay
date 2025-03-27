using System.ComponentModel.DataAnnotations;

namespace TDBA.Data
{
    public class EventModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        [RegularExpression("^[A-Za-z0-9\\s]+$", ErrorMessage = "Only letters, numbers, and spaces allowed.")]

        public string Name { get; set; } = string.Empty;

        public string AgeGroup { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a price range.")]
        public string PriceRange { get; set; } = string.Empty;

        public bool IsIndoor { get; set; }
    }
}