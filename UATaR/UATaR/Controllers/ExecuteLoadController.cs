using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UATaR.Core;
using UATaR.Interfaces;
using UATaR.ViewModels;

namespace UATaR.Controllers
{
    [Authorize(Roles = RoleNames.MethodologistDepartment)]
    public class ExecuteLoadController : Controller
    {
        private const string ApiControllerName = "executeLoad";
        private const string ApiLoadControllerName = "load";
        private readonly IApiClientHelper _client;

        public ExecuteLoadController(IApiClientHelper client)
        {
            _client = client;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ShowExecuteLoads()
        {
            var result = await _client.GetAsync(ApiControllerName);
            var content = await _client.ReadAsJsonAsync<List<ExecuteLoadViewModel>>(result);

            return View(content);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExecuteLoad(int loadId, double hours)
        {
            var executeLoad = new ExecuteLoadViewModel
            {
                LoadId = loadId,
                Hours = hours
            };

            var result = await _client.PostAsync(ApiControllerName, executeLoad);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(ShowExecuteLoads));
            }
            else
            {
                var exMessage = await result.Content.ReadAsStringAsync();
                ModelState.AddModelError("Hours", exMessage);

                return RedirectToAction(nameof(ShowExecuteLoads));
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateExecuteLoad(ExecuteLoadViewModel executeLoad)
        {
            var result = await _client.PutAsync(ApiControllerName, executeLoad);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return PartialView();
            }
            else
            {
                var errorMessage = await result.Content.ReadAsStringAsync();
                ModelState.AddModelError(nameof(executeLoad.Hours), errorMessage);

                return PartialView(executeLoad);
            }
        }
    }
}
