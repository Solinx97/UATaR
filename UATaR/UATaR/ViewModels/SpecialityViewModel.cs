using System.ComponentModel.DataAnnotations;

namespace UATaR.ViewModels
{
    public class SpecialityViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Специальность")]
        [Required(ErrorMessage = "Поле 'Специальность' обязательно для заполнения")]
        public string Name { get; set; }

        [Display(Name = "Кафедра")]
        public string Chair { get; set; }
    }
}