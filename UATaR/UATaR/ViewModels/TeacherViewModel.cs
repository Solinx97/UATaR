using System.ComponentModel.DataAnnotations;

namespace UATaR.ViewModels
{
    public class TeacherViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Полное имя")]
        [Required(ErrorMessage = "Поле 'Полное имя' обязательно для заполнения")]
        public string FullName { get; set; }

        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Display(Name = "Образование")]
        public string Education { get; set; }
    }
}