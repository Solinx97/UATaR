using System.ComponentModel.DataAnnotations;

namespace UATaR.ViewModels
{
    public class ExecuteLoadViewModel
    {
        public int Id { get; set; }

        public int LoadId { get; set; }

        [Display(Name = "Выполнено часов")]
        [Required(ErrorMessage = "Поле 'Выполнено часов' обязательно для заполнения")]
        public double Hours { get; set; }
    }
}