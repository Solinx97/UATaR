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
    public class TeacherController : Controller
    {
        private const string ApiControllerName = "teacher";
        private readonly IApiClientHelper _client;

        public TeacherController(IApiClientHelper client)
        {
            _client = client;
        }

        [HttpGet]
        //[Authorize(Roles = RoleNames.HeadDepartment)]
        [AllowAnonymous]
        public async Task<IActionResult> ShowTeachers()
        {
            var result = await _client.GetAsync(ApiControllerName);
            var content = await _client.ReadAsJsonAsync<List<TeacherViewModel>>(result);

            return View(content);
        }

        [HttpGet]
        public IActionResult CreateTeacher()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> CreateTeacher(TeacherViewModel teacher)
        {
            if (!ModelState.IsValid)
            {
                return CreateTeacher(teacher);
            }

            return CreateTeacherInternal(teacher);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTeacher(int id)
        {
            var result = await _client.GetAsync($"{ApiControllerName}/{id}");
            var data = await _client.ReadAsJsonAsync<TeacherViewModel>(result);

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTeacher(TeacherViewModel teacher)
        {
            var result = await _client.PutAsync(ApiControllerName, teacher);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(ShowTeachers));
            }
            else
            {
                var exMessage = await result.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, exMessage);

                return await UpdateTeacher(teacher);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            await _client.DeleteAsync($"{ApiControllerName}/{id}");

            return RedirectToAction(nameof(ShowTeachers));
        }

        private async Task<IActionResult> CreateTeacherInternal(TeacherViewModel teacher)
        {
            var result = await _client.PostAsync(ApiControllerName, teacher);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(ShowTeachers));
            }
            else
            {
                var exMessage = await result.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, exMessage);

                return CreateTeacher();
            }
        }
    }
}
