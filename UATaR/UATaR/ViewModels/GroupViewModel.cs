using System.ComponentModel.DataAnnotations;

namespace UATaR.ViewModels
{
    public class GroupViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Группа")]
        [Required(ErrorMessage = "Поле 'Группа' обязательно для заполнения")]
        public string Name { get; set; }
    }
}