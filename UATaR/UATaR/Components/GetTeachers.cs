using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UATaR.Interfaces;
using UATaR.ViewModels;

namespace UATaR.Components
{
    public class GetTeachers : ViewComponent
    {
        private const string ControllerName = "teacher";
        private readonly IApiClientHelper _client;

        public GetTeachers(IApiClientHelper client)
        {
            _client = client;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = new List<TeacherViewModel>();
            var result = await _client.GetAsync($"{ControllerName}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                data = await _client.ReadAsJsonAsync<List<TeacherViewModel>>(result);
            }

            var selectList = new SelectList(data, "Id", "FullName");
            return View(selectList);
        }
    }
}