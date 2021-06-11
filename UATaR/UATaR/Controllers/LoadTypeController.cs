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
    public class LoadTypeController : Controller
    {
        private const string ApiControllerName = "loadType";
        private readonly IApiClientHelper _client;

        public LoadTypeController(IApiClientHelper client)
        {
            _client = client;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ShowLoadTypes()
        {
            var result = await _client.GetAsync(ApiControllerName);
            var content = await _client.ReadAsJsonAsync<List<LoadTypeViewModel>>(result);

            return View(content);
        }

        [HttpGet]
        public IActionResult CreateLoadType()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLoadType(LoadTypeViewModel loadType)
        {
            if (!ModelState.IsValid)
            {
                return View(loadType);
            }

            var result = await _client.PostAsync(ApiControllerName, loadType);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(ShowLoadTypes));
            }
            else
            {
                var exMessage = await result.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, exMessage);

                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateLoadType(int id)
        {
            var result = await _client.GetAsync($"{ApiControllerName}/{id}");
            var data = await _client.ReadAsJsonAsync<LoadTypeViewModel>(result);

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateLoadType(LoadTypeViewModel loadType)
        {
            if (!ModelState.IsValid)
            {
                return View(loadType);
            }

            var result = await _client.PutAsync(ApiControllerName, loadType);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(ShowLoadTypes));
            }
            else
            {
                var exMessage = await result.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, exMessage);

                return View(loadType);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteLoadType(int id)
        {
            await _client.DeleteAsync($"{ApiControllerName}/{id}");

            return RedirectToAction(nameof(ShowLoadTypes));
        }
    }
}