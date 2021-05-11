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
    public class ExecuteLoadController : Controller
    {
        private readonly IService<ExecuteLoad, int> _executeLoadervice;
        private readonly IMapper _mapper;
        private readonly ILogger<ExecuteLoadController> _logger;

        public ExecuteLoadController(IService<ExecuteLoad, int> executeLoadService,
            IMapper mapper,
            ILogger<ExecuteLoadController> logger)
        {
            _executeLoadervice = executeLoadService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> ShowDrivers()
        {
            try
            {
                var executeLoads = await _executeLoadervice.GetAllAsync();
                var data = _mapper.Map<List<ExecuteLoadViewModel>>(executeLoads);

                return Ok(data);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                var data = new List<ExecuteLoadViewModel>();

                return Ok(data);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDriver(int id)
        {
            try
            {
                var executeLoad = await _executeLoadervice.GetByIdAsync(id);
                var data = _mapper.Map<ExecuteLoadViewModel>(executeLoad);

                return Ok(data);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDriver(ExecuteLoadViewModel driver)
        {
            try
            {
                var mapData = _mapper.Map<ExecuteLoad>(driver);
                await _executeLoadervice.CreateAsync(mapData);

                return Ok();
            }
            catch (DateException ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDriver(ExecuteLoadViewModel driver)
        {
            try
            {
                var mapData = _mapper.Map<ExecuteLoad>(driver);
                await _executeLoadervice.UpdateAsync(mapData);

                return Ok();
            }
            catch (DateException ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriver(int id)
        {
            var executeLoad = await _executeLoadervice.GetByIdAsync(id);
            var mapData = _mapper.Map<ExecuteLoad>(executeLoad);
            await _executeLoadervice.DeleteAsync(mapData);

            return Ok();
        }
    }
}
