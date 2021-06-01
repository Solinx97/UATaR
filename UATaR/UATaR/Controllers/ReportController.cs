using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Novacode;
using System;
using System.Drawing;
using System.Threading.Tasks;
using UATaR.Core;
using UATaR.Interfaces;
using UATaR.ViewModels;

namespace UATaR.Controllers
{
    [Authorize(Roles = RoleNames.MethodologistDepartment)]
    public class ReportController : Controller
    {
        private const string ApiControllerName = "teacher";
        private readonly IApiClientHelper _client;

        public ReportController(IApiClientHelper client)
        {
            _client = client;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ShowReports()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetReportData(int teacherId, DateTimeOffset startPeriod, DateTimeOffset finishPeriod, double hours)
        {
            var result = await _client.GetAsync($"{ApiControllerName}/{teacherId}");
            var content = await _client.ReadAsJsonAsync<TeacherViewModel>(result);

            ViewBag.Executar = content.FullName;
            ViewBag.Education = content.Education;
            ViewBag.StartPeriod = startPeriod.ToString("dd.MM.yyyy");
            ViewBag.FinishPeriod = finishPeriod.ToString("dd.MM.yyyy");
            ViewBag.Hours = hours;

            return PartialView();
        }

        [HttpGet]
        public async Task<IActionResult> CreateDocument()
        {
            string pathDocument = AppDomain.CurrentDomain.BaseDirectory + "example.docx";

            DocX document = DocX.Create(pathDocument);
            document.InsertParagraph("Тест");

            document.InsertParagraph("Тест").
                     Font("Calibri").
                     FontSize(36).
                     Color(Color.Navy).
                     Bold().
                     Spacing(15).
                     Alignment = Alignment.center;

            Paragraph paragraph = document.InsertParagraph();
            paragraph.Alignment = Alignment.right;

            paragraph.AppendLine("Тест").
                     FontSize(20).
                     Italic().
                     UnderlineStyle(UnderlineStyle.dotted).
                     UnderlineColor(Color.DarkOrange).
                     Highlight(Highlight.yellow);
            paragraph.AppendLine();
            paragraph.AppendLine("Тест");

            document.Save();

            return PartialView();
        }
    }
}