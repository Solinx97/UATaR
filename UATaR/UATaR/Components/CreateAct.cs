using Microsoft.AspNetCore.Mvc;
using UATaR.ViewModels;

namespace UATaR.Components
{
    public class CreateAct : ViewComponent
    {
        public IViewComponentResult Invoke(TeacherViewModel teacher, ReportViewModel report)
        {
            ViewBag.Executar = teacher.FullName;
            ViewBag.Education = teacher.Education;
            ViewBag.StartPeriod = report.StartPeriod;
            ViewBag.FinishPeriod = report.FinishPeriod;

            return View();
        }
    }
}
