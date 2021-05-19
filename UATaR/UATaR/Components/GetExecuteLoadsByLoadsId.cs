using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UATaR.Interfaces;
using UATaR.ViewModels;

namespace UATaR.Components
{
    public class GetExecuteLoadsByLoadsId : ViewComponent
    {
        private const string ControllerName = "executeLoad";
        private readonly IApiClientHelper _client;

        public GetExecuteLoadsByLoadsId(IApiClientHelper client)
        {
            _client = client;
        }

        public async Task<IViewComponentResult> InvokeAsync(IGrouping<int, LoadViewModel> loads)
        {
            double sum = 0;
            foreach (var item in loads)
            {
                var result = await _client.GetAsync($"{ControllerName}/loadId/{item.Id}");
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var data = await _client.ReadAsJsonAsync<ExecuteLoadViewModel>(result);
                    sum += data.Hours;
                }
                else break;
            }

            return View(sum);
        }
    }
}
