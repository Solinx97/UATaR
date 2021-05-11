using System;
using System.ComponentModel.DataAnnotations;

namespace UATaR.ViewModels
{
    public class ExecuteLoadViewModel
    {
        public int Id { get; set; }

        public int LoadId { get; set; }

        [Display(Name = "Date execute")]
        [Required(ErrorMessage = "Required first name")]
        public DateTimeOffset DateExecute { get; set; }

        [Display(Name = "Is full executed")]
        [Required(ErrorMessage = "Required first name")]
        public bool IsFullExecuted { get; set; }
    }
}