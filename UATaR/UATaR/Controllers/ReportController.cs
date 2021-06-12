using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Novacode;
using System;
using System.Collections.Generic;
using System.Text;
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

        [HttpPost]
        public IActionResult CreateDocument(ReportViewModel report, Dictionary<string, string> hoursesForLoadType)
        {
            string pathDocument = AppDomain.CurrentDomain.BaseDirectory + "act.docx";

            var document = DocX.Create(pathDocument);
            document.MarginBottom = 27;
            document.MarginLeft = 42;
            document.MarginRight = 27;
            document.MarginTop = 19;

            FirstPart(document, report);
            var sumHours = SecondPart(document, hoursesForLoadType);
            ThirdPart(document);
            FourthPart(document, sumHours);
            FiftyPart(document);

            return PartialView();
        }

        private List<double> SecondPart(DocX document, Dictionary<string, string> hoursesForLoadType)
        {
            var hours = new List<double>();
            var hoursSum = new List<double>();
            var table = document.InsertTable(1, hoursesForLoadType.Count + 1);
            table.Alignment = Alignment.center;
            var row = table.Rows[0];
            int count = 0;
            foreach (var item in hoursesForLoadType)
            {
                row.Height = 120;
                var cell = row.Cells[count];
                cell.Paragraphs[0].Append(item.Key);
                cell.VerticalAlignment = VerticalAlignment.Center;
                cell.TextDirection = TextDirection.btLr;

                var newRow = table.InsertRow();
                InsertHoursByLoadType(item.Value.Split(';'), hours);
                count++;
            }
            row.Cells[row.ColumnCount - 1].Paragraphs[0].Append("Всего");
            count = 0;

            int step = 3;
            double currentSum = 0;
            for (int j = 1; j < table.Rows.Count; j++)
            {
                for (int i = 0; i < table.Rows[j].Cells.Count - 1; i++)
                {
                    table.Rows[j].Height = 30;
                    table.Rows[j].Cells[i].Paragraphs[0].Append(hours[count].ToString());
                    table.Rows[j].Cells[i].VerticalAlignment = VerticalAlignment.Center;
                    currentSum += hours[count];
                    count += step;
                }
                table.Rows[j].Cells[table.Rows[j].ColumnCount - 1].Paragraphs[0].Append(currentSum.ToString());
                hoursSum.Add(currentSum);
                currentSum = 0;
                count = j;
            }

            return hoursSum;
        }

        private void InsertHoursByLoadType(string[] hours, List<double> allHours)
        {
            for (int i = 0; i < hours.Length - 1; i++)
            {
                allHours.Add(double.Parse(hours[i]));
            }
        }

        private void FirstPart(DocX document, ReportViewModel report)
        {
            document.InsertParagraph("");
            var paragraph = document.InsertParagraph("АКТ ");
            paragraph.Font("Times New Roman");
            paragraph.FontSize(12);
            paragraph.Bold();
            paragraph.Alignment = Alignment.center;

            paragraph.Append("сдачи-приемки выполненных работ от « ____» __________ 20____г.");
            paragraph.Font("Times New Roman");
            paragraph.FontSize(12);

            paragraph = document.InsertParagraph("к договору подряда  от « ___» ___________20____г. № ______");
            paragraph.Font("Times New Roman");
            paragraph.Alignment = Alignment.center;
            paragraph.FontSize(12);

            paragraph = document.InsertParagraph("на выполнение педагогической работы  со слушателями на условиях почасовой оплаты труда");
            paragraph.Font("Times New Roman");
            paragraph.Alignment = Alignment.center;
            paragraph.FontSize(12);

            paragraph = document.InsertParagraph("г. Гомель");
            paragraph.Font("Times New Roman");
            paragraph.Alignment = Alignment.left;
            paragraph.FontSize(12);

            var stringBuilder = new StringBuilder();
            stringBuilder.Append("проректор по учебной работе Сычев Александр Васильевич, действующий на ");
            stringBuilder.Append("основании доверенности от 03.04.2017 г. № 20, с другой стороны, составили  настоящий  акт о том,");
            stringBuilder.Append($"что в период с «{report.StartPeriod.Day}» {report.StartPeriod.Month} {report.StartPeriod.Year}г.");
            stringBuilder.Append($" по «{report.FinishPeriod.Day}» {report.FinishPeriod.Month} {report.FinishPeriod.Year}г. ");

            paragraph = document.InsertParagraph("        Мы, нижеподписавшиеся:");
            paragraph.Font("Times New Roman");
            paragraph.Alignment = Alignment.both;
            paragraph.FontSize(12);

            paragraph.Append("Исполнитель ");
            paragraph.Font("Times New Roman");
            paragraph.FontSize(12);
            paragraph.Bold();

            paragraph.Append(report.ExecutorFullName);
            paragraph.Font("Times New Roman");
            paragraph.FontSize(12);
            paragraph.Italic();

            paragraph.Append($" ученая степень {report.Education} с одной стороны, и");
            paragraph.Font("Times New Roman");
            paragraph.FontSize(12);

            paragraph.Append("Заказчик");
            paragraph.Font("Times New Roman");
            paragraph.FontSize(12);
            paragraph.Bold();

            paragraph.Append(stringBuilder.ToString());
            paragraph.Font("Times New Roman");
            paragraph.FontSize(12);

            paragraph.Append("Исполнителем");
            paragraph.Font("Times New Roman");
            paragraph.FontSize(12);
            paragraph.Bold();

            paragraph.Append("выполнена, a ");
            paragraph.Font("Times New Roman");
            paragraph.FontSize(12);

            paragraph.Append("Заказчиком");
            paragraph.Font("Times New Roman");
            paragraph.FontSize(12);
            paragraph.Bold();

            paragraph.Append($" принята в ИПК и П педагогическая работа на условиях почасовой оплаты труда в объеме {report.Hours} часов, из них:");
            paragraph.Font("Times New Roman");
            paragraph.FontSize(12);
        }

        private void ThirdPart(DocX document)
        {
            document.InsertParagraph("");
            var paragraph = document.InsertParagraph("Всего  ____________________________________________________________________  учебных часов");
            paragraph.Font("Times New Roman");
            paragraph.Alignment = Alignment.both;
            paragraph.FontSize(12);

            paragraph = document.InsertParagraph("                                                                (прописью)");
            paragraph.Font("Times New Roman");
            paragraph.Alignment = Alignment.both;
            paragraph.FontSize(9);

            paragraph = document.InsertParagraph("Претензий к качеству и объему выполненной работы у Заказчика нет.   ");
            paragraph.Font("Times New Roman");
            paragraph.Alignment = Alignment.both;
            paragraph.FontSize(12);

            paragraph = document.InsertParagraph("Расчет стоимости выполненной педагогической работы:");
            paragraph.Font("Times New Roman");
            paragraph.Alignment = Alignment.both;
            paragraph.FontSize(12);
            document.InsertParagraph("");
        }

        private void FourthPart(DocX document, List<double> hours)
        {
            var table = document.InsertTable(hours.Count + 1, 3);
            table.Alignment = Alignment.center;
            int count = 1;

            var row = table.Rows[0];
            var cell = row.Cells[0];
            cell.Paragraphs[0].Append("Кол-во часов");
            cell.Paragraphs[0].Alignment = Alignment.center;

            cell = row.Cells[1];
            cell.Paragraphs[0].Append("Стоимость за один час");
            cell.Paragraphs[0].Alignment = Alignment.center;
            var paragraph = cell.InsertParagraph("(руб.)");
            paragraph.Alignment = Alignment.center;

            cell = row.Cells[2];
            cell.Paragraphs[0].Append("Сумма к оплате");
            paragraph = cell.InsertParagraph("(руб.)");
            paragraph.Alignment = Alignment.center;

            foreach (var item in hours)
            {
                row = table.Rows[count];
                row.Height = 30;

                cell = row.Cells[0];
                cell.Paragraphs[0].Append(item.ToString());
                cell.VerticalAlignment = VerticalAlignment.Center;
                count++;
            }
        }

        private void FiftyPart(DocX document)
        {
            var paragraph = document.InsertParagraph("Бухгалтерии оплатить за выполненную педагогическую работу по настоящему акту ");
            paragraph.Font("Times New Roman");
            paragraph.Alignment = Alignment.both;
            paragraph.FontSize(12);

            paragraph.Append("_______________________________________________________________________ рублей.");
            paragraph.Font("Times New Roman");
            paragraph.FontSize(12);

            paragraph = document.InsertParagraph("                                                                                      (сумма прописью)");
            paragraph.Font("Times New Roman");
            paragraph.Alignment = Alignment.both;
            paragraph.FontSize(9);

            paragraph = document.InsertParagraph("Источник финансирования: ");
            paragraph.Font("Times New Roman");
            paragraph.Alignment = Alignment.both;
            paragraph.FontSize(12);
            paragraph.Append("смета доходов и расходов «Переподготовка и ПК»");
            paragraph.Font("Times New Roman");
            paragraph.UnderlineStyle(UnderlineStyle.singleLine);
            paragraph.Alignment = Alignment.both;
            paragraph.FontSize(12);

            document.InsertParagraph("");

            paragraph = document.InsertParagraph("Исполнитель                                   _________________                     ___________________");
            paragraph.Font("Times New Roman");
            paragraph.Alignment = Alignment.both;
            paragraph.FontSize(12);

            paragraph = document.InsertParagraph("                                                                                            (подпись)                                            (И.О. Фамилия Исполнителя)");
            paragraph.Font("Times New Roman");
            paragraph.Alignment = Alignment.both;
            paragraph.FontSize(9);

            paragraph = document.InsertParagraph("Заказчик                                          _________________                      ___________________");
            paragraph.Font("Times New Roman");
            paragraph.Alignment = Alignment.both;
            paragraph.FontSize(12);

            paragraph = document.InsertParagraph("                                                                                     (подпись)                                              (И.О. Фамилия  Заказчика)");
            paragraph.Font("Times New Roman");
            paragraph.Alignment = Alignment.both;
            paragraph.FontSize(9);

            paragraph = document.InsertParagraph("__________________                                                  ____________        ____________________");
            paragraph.Font("Times New Roman");
            paragraph.Alignment = Alignment.both;
            paragraph.FontSize(12);

            paragraph = document.InsertParagraph("(должность руководителя	ИПК и П)                                                 (подпись)                               (И.О. Фамилия)   ");
            paragraph.Font("Times New Roman");
            paragraph.Alignment = Alignment.both;
            paragraph.FontSize(9);

            paragraph = document.InsertParagraph("Заведующий кафедрой ___________________	   ____________     ____________________     ");
            paragraph.Font("Times New Roman");
            paragraph.Alignment = Alignment.both;
            paragraph.FontSize(12);

            paragraph = document.InsertParagraph("                                           (наименование кафедры)	           (подпись)                       (И.О. Фамилия)   ");
            paragraph.Font("Times New Roman");
            paragraph.Alignment = Alignment.both;
            paragraph.FontSize(9);

            paragraph = document.InsertParagraph("Проверил _______________________                      ____________     ___________________   ");
            paragraph.Font("Times New Roman");
            paragraph.Alignment = Alignment.both;
            paragraph.FontSize(12);

            paragraph = document.InsertParagraph("                 (должность сотрудника ИПК и П)                                        (подпись)                       (И.О. Фамилия)   ");
            paragraph.Font("Times New Roman");
            paragraph.Alignment = Alignment.both;
            paragraph.FontSize(9);

            paragraph = document.InsertParagraph("Рассчитал_________________________                     _____________    ________________");
            paragraph.Font("Times New Roman");
            paragraph.Alignment = Alignment.both;
            paragraph.FontSize(12);

            paragraph = document.InsertParagraph("                         (должность сотрудника ПЭО)                                               (подпись)                       (И.О. Фамилия)   ");
            paragraph.Font("Times New Roman");
            paragraph.Alignment = Alignment.both;
            paragraph.FontSize(9);

            paragraph = document.InsertParagraph("Акт принят бухгалтерией «______» __________20___г.");
            paragraph.Font("Times New Roman");
            paragraph.Alignment = Alignment.both;
            paragraph.FontSize(12);

            paragraph = document.InsertParagraph("_________________________         _____________    ________________");
            paragraph.Font("Times New Roman");
            paragraph.Alignment = Alignment.both;
            paragraph.FontSize(12);

            paragraph = document.InsertParagraph("(должность сотрудника бухгалтерии)                       (подпись)                    (И.О. Фамилия)   ");
            paragraph.Font("Times New Roman");
            paragraph.Alignment = Alignment.both;
            paragraph.FontSize(9);

        }
    }
}