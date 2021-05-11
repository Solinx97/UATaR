using System.ComponentModel.DataAnnotations;

namespace UATaR.ViewModels
{
    public class LoadViewModel
    {
        public int Id { get; set; }

        public int TeacherId { get; set; }

        public int SubjectId { get; set; }

        public int LoadTypeId { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Required first name")]
        public string Description { get; set; }
    }
}