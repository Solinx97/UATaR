using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using UATaR.Interfaces;
using UATaR.ViewModels;

namespace UATaR.Components
{
    public class GetExecuteLoadsByLoadId : ViewComponent
    {
        private const string ControllerName = "executeLoad";
        private readonly IApiClientHelper _client;

        public GetExecuteLoadsByLoadId(IApiClientHelper client)
        {
            _client = client;
        }

        public async Task<IViewComponentResult> InvokeAsync(int loadId, string viewName)
        {
            var result = await _client.GetAsync($"{ControllerName}/loadId/{loadId}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var data = await _client.ReadAsJsonAsync<ExecuteLoadViewModel>(result);
                return View(viewName, data);
            }
            else
            {
                return View(viewName, new ExecuteLoadViewModel());
            }
        }
    }
}
