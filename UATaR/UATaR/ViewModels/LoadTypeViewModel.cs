using System.ComponentModel.DataAnnotations;

namespace UATaR.ViewModels
{
    public class LoadTypeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Required first name")]
        public string Name { get; set; }
    }
}