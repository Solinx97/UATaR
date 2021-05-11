using System.ComponentModel.DataAnnotations;

namespace UATaR.ViewModels
{
    public class SpecialityViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Required first name")]
        public string Name { get; set; }

        [Display(Name = "Facility")]
        [Required(ErrorMessage = "Required first name")]
        public string Facility { get; set; }

        [Display(Name = "Chair")]
        [Required(ErrorMessage = "Required first name")]
        public string Chair { get; set; }
    }
}