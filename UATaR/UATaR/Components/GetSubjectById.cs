using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using UATaR.Interfaces;
using UATaR.ViewModels;

namespace UATaR.Components
{
    public class GetSubjectById : ViewComponent
    {
        private const string ControllerName = "subject";
        private readonly IApiClientHelper _client;

        public GetSubjectById(IApiClientHelper client)
        {
            _client = client;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var result = await _client.GetAsync($"{ControllerName}/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var data = await _client.ReadAsJsonAsync<SubjectViewModel>(result);
                return View(data);
            }
            else
            {
                return View();
            }
        }
    }
}