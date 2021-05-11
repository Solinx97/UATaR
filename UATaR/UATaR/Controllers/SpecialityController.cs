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
    public class SpecialityController : Controller
    {
        private const string ApiControllerName = "speciality";
        private readonly IApiClientHelper _client;

        public SpecialityController(IApiClientHelper client)
        {
            _client = client;
        }

        [HttpGet]
        //[Authorize(Roles = RoleNames.HeadDepartment)]
        [AllowAnonymous]
        public async Task<IActionResult> ShowSpecialities()
        {
            var result = await _client.GetAsync(ApiControllerName);
            var content = await _client.ReadAsJsonAsync<List<SpecialityViewModel>>(result);

            return View(content);
        }

        [HttpGet]
        public IActionResult CreateSpeciality()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> CreateSpeciality(SpecialityViewModel speciality)
        {
            if (!ModelState.IsValid)
            {
                return CreateSpeciality(speciality);
            }

            return CreateSpecialityInternal(speciality);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateSpeciality(int id)
        {
            var result = await _client.GetAsync($"{ApiControllerName}/{id}");
            var data = await _client.ReadAsJsonAsync<SpecialityViewModel>(result);

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSpeciality(SpecialityViewModel speciality)
        {
            var result = await _client.PutAsync(ApiControllerName, speciality);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(ShowSpecialities));
            }
            else
            {
                var exMessage = await result.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, exMessage);

                return await UpdateSpeciality(speciality);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteLoadType(int id)
        {
            await _client.DeleteAsync($"{ApiControllerName}/{id}");

            return RedirectToAction(nameof(ShowSpecialities));
        }

        private async Task<IActionResult> CreateSpecialityInternal(SpecialityViewModel speciality)
        {
            var result = await _client.PostAsync(ApiControllerName, speciality);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(ShowSpecialities));
            }
            else
            {
                var exMessage = await result.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, exMessage);

                return CreateSpeciality();
            }
        }
    }
}