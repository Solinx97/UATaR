using System.ComponentModel.DataAnnotations;

namespace UATaR.ViewModels
{
    public class SubjectViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Поле 'Название' обязательно для заполнения")]
        public string Name { get; set; }
    }
}