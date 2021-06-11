using System.ComponentModel.DataAnnotations;

namespace UATaR.ViewModels
{
    public class LoadViewModel
    {
        public int Id { get; set; }

        public int TeacherId { get; set; }

        public int SubjectId { get; set; }

        public int GroupId { get; set; }

        public int LoadTypeId { get; set; }

        [Display(Name = "Часов")]
        [Required(ErrorMessage = "Поле 'Часов' обязательно для заполнения")]
        public double Hours { get; set; }
    }
}