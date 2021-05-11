using AutoMapper;
using BusinessLayer.Entities;
using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using UATaRApi.ViewModels;

namespace UATaRApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TeacherController : Controller
    {
        private readonly IService<Teacher, int> _teacherService;
        private readonly IMapper _mapper;
        private readonly ILogger<TeacherController> _logger;

        public TeacherController(IService<Teacher, int> teacherService,
            IMapper mapper,
            ILogger<TeacherController> logger)
        {
            _teacherService = teacherService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> ShowTeacher()
        {
            try
            {
                var data = await _teacherService.GetAllAsync();
                var map = _mapper.Map<List<TeacherViewModel>>(data);

                return Ok(map);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                var data = new List<TeacherViewModel>();

                return Ok(data);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacher(int id)
        {
            try
            {
                var data = await _teacherService.GetByIdAsync(id);
                var map = _mapper.Map<TeacherViewModel>(data);

                return Ok(map);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeacher(TeacherViewModel teacher)
        {
            try
            {
                var map = _mapper.Map<Teacher>(teacher);
                await _teacherService.CreateAsync(map);

                return Ok();
            }
            catch (DateException ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTeacher(TeacherViewModel teacher)
        {
            try
            {
                var map = _mapper.Map<Teacher>(teacher);
                await _teacherService.UpdateAsync(map);

                return Ok();
            }
            catch (DateException ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var map = await _teacherService.GetByIdAsync(id);
            var data = _mapper.Map<Teacher>(map);
            await _teacherService.DeleteAsync(data);

            return Ok();
        }
    }
}
