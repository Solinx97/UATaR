using System.ComponentModel.DataAnnotations;

namespace UATaR.ViewModels
{
    public class ExecuteLoadViewModel
    {
        public int Id { get; set; }

        public int LoadId { get; set; }

        [Display(Name = "Выполнено часов")]
        [Required(ErrorMessage = "Required first name")]
        public double Hours { get; set; }
    }
}