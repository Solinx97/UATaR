using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UATaR.Interfaces;
using UATaR.ViewModels;

namespace UATaR.Components
{
    public class GetLoads : ViewComponent
    {
        private const string ControllerName = "load";
        private readonly IApiClientHelper _client;

        public GetLoads(IApiClientHelper client)
        {
            _client = client;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = new List<LoadViewModel>();
            var result = await _client.GetAsync($"{ControllerName}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                data = await _client.ReadAsJsonAsync<List<LoadViewModel>>(result);
            }

            var selectList = new SelectList(data, "Id", "Description");
            return View(selectList);
        }
    }
}
