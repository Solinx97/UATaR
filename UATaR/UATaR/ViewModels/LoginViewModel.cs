using System.ComponentModel.DataAnnotations;

namespace UATaR.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Имя пользователя")]
        [Required(ErrorMessage = "'Имя пользователя' обязательно для заполнения")]
        public string UserName { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "'Пароль' обязательно для заполнения")]
        public string Password { get; set; }
    }
}