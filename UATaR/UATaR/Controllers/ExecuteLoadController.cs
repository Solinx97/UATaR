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
        private readonly IApiClientHelper _client;

        public ExecuteLoadController(IApiClientHelper client)
        {
            _client = client;
        }

        [HttpGet]
        //[Authorize(Roles = RoleNames.HeadDepartment)]
        [AllowAnonymous]
        public async Task<IActionResult> ShowExecuteLoads()
        {
            var result = await _client.GetAsync(ApiControllerName);
            var content = await _client.ReadAsJsonAsync<List<ExecuteLoadViewModel>>(result);

            return View(content);
        }

        [HttpGet]
        public IActionResult CreateExecuteLoad()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> CreateExecuteLoad(ExecuteLoadViewModel executeLoad)
        {
            if (!ModelState.IsValid)
            {
                return CreateExecuteLoad(executeLoad);
            }

            return CreateExecuteLoadInternal(executeLoad);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateExecuteLoad(int id)
        {
            var result = await _client.GetAsync($"{ApiControllerName}/{id}");
            var data = await _client.ReadAsJsonAsync<ExecuteLoadViewModel>(result);

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateExecuteLoad(ExecuteLoadViewModel executeLoad)
        {
            var result = await _client.PutAsync(ApiControllerName, executeLoad);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(ShowExecuteLoads));
            }
            else
            {
                var exMessage = await result.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, exMessage);

                return await UpdateExecuteLoad(executeLoad);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteExecuteLoad(int id)
        {
            await _client.DeleteAsync($"{ApiControllerName}/{id}");

            return RedirectToAction(nameof(ShowExecuteLoads));
        }

        private async Task<IActionResult> CreateExecuteLoadInternal(ExecuteLoadViewModel executeLoad)
        {
            var result = await _client.PostAsync(ApiControllerName, executeLoad);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(ShowExecuteLoads));
            }
            else
            {
                var exMessage = await result.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, exMessage);

                return CreateExecuteLoad();
            }
        }
    }
}
