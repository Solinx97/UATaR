using System.ComponentModel.DataAnnotations;

namespace UATaR.ViewModels
{
    public class SubjectViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Required first name")]
        public string Name { get; set; }

        [Display(Name = "Facility")]
        [Required(ErrorMessage = "Required first name")]
        public string Facility { get; set; }
    }
}