using System.ComponentModel.DataAnnotations;

namespace UATaR.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Имя пользователя")]
        [Required(ErrorMessage = "Required user name")]
        public string UserName { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Required password")]
        public string Password { get; set; }
    }
}