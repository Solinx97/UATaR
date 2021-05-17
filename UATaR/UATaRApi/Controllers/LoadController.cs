using AutoMapper;
using BusinessLayer.Entities;
using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UATaRApi.ViewModels;

namespace UATaRApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoadController : Controller
    {
        private readonly IService<Load, int> _loadService;
        private readonly IMapper _mapper;
        private readonly ILogger<LoadController> _logger;

        public LoadController(IService<Load, int> loadService,
            IMapper mapper,
            ILogger<LoadController> logger)
        {
            _loadService = loadService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetLoads()
        {
            try
            {
                var data = await _loadService.GetAllAsync();
                var map = _mapper.Map<List<LoadViewModel>>(data);

                return Ok(map);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                var data = new List<LoadViewModel>();

                return Ok(data);
            }
        }

        [HttpGet("teacherId/{teacherId}")]
        public async Task<IActionResult> GetLoadByTeacherId(int teacherId)
        {
            try
            {
                var data = await _loadService.GetAllAsync();
                var dataByteacherId = data.Where(val => val.TeacherId == teacherId);
                var map = _mapper.Map<List<LoadViewModel>>(dataByteacherId);

                return Ok(map);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLoadById(int id)
        {
            try
            {
                var data = await _loadService.GetByIdAsync(id);
                var map = _mapper.Map<LoadViewModel>(data);

                return Ok(map);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateLoad(LoadViewModel load)
        {
            var map = _mapper.Map<Load>(load);
            await _loadService.CreateAsync(map);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLoad(LoadViewModel load)
        {
            var map = _mapper.Map<Load>(load);
            await _loadService.UpdateAsync(map);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoad(int id)
        {
            var data = await _loadService.GetByIdAsync(id);
            var map = _mapper.Map<Load>(data);
            await _loadService.DeleteAsync(map);

            return Ok();
        }
    }
}