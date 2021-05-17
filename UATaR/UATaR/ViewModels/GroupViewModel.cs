using System.ComponentModel.DataAnnotations;

namespace UATaR.ViewModels
{
    public class GroupViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Группа")]
        [Required(ErrorMessage = "Required first name")]
        public string Name { get; set; }
    }
}