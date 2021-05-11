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
    public class LoadController : Controller
    {
        private const string ApiControllerName = "load";
        private readonly IApiClientHelper _client;

        public LoadController(IApiClientHelper client)
        {
            _client = client;
        }

        [HttpGet]
        //[Authorize(Roles = RoleNames.HeadDepartment)]
        [AllowAnonymous]
        public async Task<IActionResult> ShowLoads()
        {
            var result = await _client.GetAsync(ApiControllerName);
            var content = await _client.ReadAsJsonAsync<List<LoadViewModel>>(result);

            return View(content);
        }

        [HttpGet]
        public IActionResult CreateLoad()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> CreateLoad(LoadViewModel load)
        {
            if (!ModelState.IsValid)
            {
                return CreateLoad(load);
            }

            return CreateLoadInternal(load);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateLoad(int id)
        {
            var result = await _client.GetAsync($"{ApiControllerName}/{id}");
            var data = await _client.ReadAsJsonAsync<LoadViewModel>(result);

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateLoad(LoadViewModel load)
        {
            var result = await _client.PutAsync(ApiControllerName, load);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(ShowLoads));
            }
            else
            {
                var exMessage = await result.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, exMessage);

                return await UpdateLoad(load);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteLoad(int id)
        {
            await _client.DeleteAsync($"{ApiControllerName}/{id}");

            return RedirectToAction(nameof(ShowLoads));
        }

        private async Task<IActionResult> CreateLoadInternal(LoadViewModel load)
        {
            var result = await _client.PostAsync(ApiControllerName, load);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(ShowLoads));
            }
            else
            {
                var exMessage = await result.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, exMessage);

                return CreateLoad();
            }
        }
    }
}