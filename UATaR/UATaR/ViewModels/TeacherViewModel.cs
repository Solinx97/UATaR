using System;
using System.ComponentModel.DataAnnotations;

namespace UATaR.ViewModels
{
    public class TeacherViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Full name")]
        [Required(ErrorMessage = "Required first name")]
        public string FullName { get; set; }

        [Display(Name = "Position")]
        [Required(ErrorMessage = "Required first name")]
        public string Position { get; set; }

        [Display(Name = "Education")]
        [Required(ErrorMessage = "Required first name")]
        public string Education { get; set; }

        [Display(Name = "Birthday")]
        [Required(ErrorMessage = "Required first name")]
        [DataType(DataType.Date)]
        public DateTimeOffset Birthday { get; set; }
    }
}