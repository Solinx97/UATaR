using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using UATaR.Interfaces;
using UATaR.ViewModels;

namespace UATaR.Components
{
    public class GetLoadById : ViewComponent
    {
        private const string ControllerName = "load";
        private readonly IApiClientHelper _client;

        public GetLoadById(IApiClientHelper client)
        {
            _client = client;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var result = await _client.GetAsync($"{ControllerName}/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var data = await _client.ReadAsJsonAsync<LoadViewModel>(result);
                return View(data);
            }
            else
            {
                return View();
            }
        }
    }
}