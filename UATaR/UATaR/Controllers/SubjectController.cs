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
    public class SubjectController : Controller
    {
        private const string ApiControllerName = "subject";
        private readonly IApiClientHelper _client;

        public SubjectController(IApiClientHelper client)
        {
            _client = client;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ShowSubjects()
        {
            var result = await _client.GetAsync(ApiControllerName);
            var content = await _client.ReadAsJsonAsync<List<SubjectViewModel>>(result);

            return View(content);
        }

        [HttpGet]
        public IActionResult CreateSubject()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSubject(SubjectViewModel subject)
        {
            if (!ModelState.IsValid)
            {
                return View(subject);
            }

            var result = await _client.PostAsync(ApiControllerName, subject);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(ShowSubjects));
            }
            else
            {
                var exMessage = await result.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, exMessage);

                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateSubject(int id)
        {
            var result = await _client.GetAsync($"{ApiControllerName}/{id}");
            var data = await _client.ReadAsJsonAsync<SubjectViewModel>(result);

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSubject(SubjectViewModel subject)
        {
            if (!ModelState.IsValid)
            {
                return View(subject);
            }

            var result = await _client.PutAsync(ApiControllerName, subject);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(ShowSubjects));
            }
            else
            {
                var exMessage = await result.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, exMessage);

                return await UpdateSubject(subject);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            await _client.DeleteAsync($"{ApiControllerName}/{id}");

            return RedirectToAction(nameof(ShowSubjects));
        }
    }
}