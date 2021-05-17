using System.ComponentModel.DataAnnotations;

namespace UATaR.ViewModels
{
    public class UserRegistrationViewModel
    {
        [Display(Name = "Имя пользователя")]
        [Required(ErrorMessage = "Required user name")]
        public string UserName { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Required first name")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Required surname")]
        public string Surname { get; set; }

        [Display(Name = "Отчество")]
        [Required(ErrorMessage = "Required patronic")]
        public string Patronic { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Required password")]
        public string Password { get; set; }

        [Display(Name = "Подтверждение пароля")]
        [Required(ErrorMessage = "Required confirm password")]
        public string ConfirmPassword { get; set; }
    }
}