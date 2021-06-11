using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UATaR.Interfaces;
using UATaR.ViewModels;

namespace UATaR.Components
{
    public class GetSubjects : ViewComponent
    {
        private const string ControllerName = "subject";
        private readonly IApiClientHelper _client;

        public GetSubjects(IApiClientHelper client)
        {
            _client = client;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = new List<SubjectViewModel>();
            var result = await _client.GetAsync($"{ControllerName}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                data = await _client.ReadAsJsonAsync<List<SubjectViewModel>>(result);
            }

            var selectList = new SelectList(data, "Id", "Name");
            return View(selectList);
        }
    }
}