using System.ComponentModel.DataAnnotations;

namespace UATaR.ViewModels
{
    public class SpecialityViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Специальность")]
        [Required(ErrorMessage = "Required first name")]
        public string Name { get; set; }

        [Display(Name = "Кафедра")]
        [Required(ErrorMessage = "Required first name")]
        public string Chair { get; set; }
    }
}