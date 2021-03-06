using System;
using System.ComponentModel.DataAnnotations;

namespace UATaR.ViewModels
{
    public class ReportViewModel
    {
        public int Id { get; set; }

        public string ExecutorFullName { get; set; }

        public string Education { get; set; }

        public double Hours { get; set; }

        [Display(Name = "Начало периода")]
        [Required(ErrorMessage = "Required password")]
        public DateTimeOffset StartPeriod { get; set; }

        [Display(Name = "Окончание периода")]
        [Required(ErrorMessage = "Required password")]
        public DateTimeOffset FinishPeriod { get; set; }
    }
}