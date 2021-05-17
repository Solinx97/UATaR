using System.ComponentModel.DataAnnotations;

namespace UATaR.ViewModels
{
    public class TeacherViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Полное имя")]
        [Required(ErrorMessage = "Required first name")]
        public string FullName { get; set; }

        [Display(Name = "Должность")]
        [Required(ErrorMessage = "Required first name")]
        public string Position { get; set; }

        [Display(Name = "Образование")]
        [Required(ErrorMessage = "Required first name")]
        public string Education { get; set; }
    }
}