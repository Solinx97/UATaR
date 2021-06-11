using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using UATaR.Interfaces;
using UATaR.ViewModels;

namespace UATaR.Components
{
    public class GetGroupById : ViewComponent
    {
        private const string ControllerName = "group";
        private readonly IApiClientHelper _client;

        public GetGroupById(IApiClientHelper client)
        {
            _client = client;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var result = await _client.GetAsync($"{ControllerName}/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var data = await _client.ReadAsJsonAsync<GroupViewModel>(result);
                return View(data);
            }
            else
            {
                return View();
            }
        }
    }
}
