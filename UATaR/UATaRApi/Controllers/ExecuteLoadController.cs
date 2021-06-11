using AutoMapper;
using BusinessLayer.Entities;
using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UATaRApi.ViewModels;

namespace UATaRApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExecuteLoadController : Controller
    {
        private readonly IService<ExecuteLoad, int> _executeLoadService;
        private readonly IMapper _mapper;
        private readonly ILogger<ExecuteLoadController> _logger;

        public ExecuteLoadController(IService<ExecuteLoad, int> executeLoadService,
            IMapper mapper,
            ILogger<ExecuteLoadController> logger)
        {
            _executeLoadService = executeLoadService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetExecuteLoads()
        {
            try
            {
                var data = await _executeLoadService.GetAllAsync();
                var map = _mapper.Map<List<ExecuteLoadViewModel>>(data);

                return Ok(map);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                var data = new List<ExecuteLoadViewModel>();

                return Ok(data);
            }
        }

        [HttpGet("loadId/{loadId}")]
        public async Task<IActionResult> GetExecuteLoadByLoadId(int loadId)
        {
            try
            {
                var data = await _executeLoadService.GetAllAsync();
                var dataByLoadId = data.Find(val => val.LoadId == loadId);
                var map = _mapper.Map<ExecuteLoadViewModel>(dataByLoadId);

                return Ok(map);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);

                return Ok();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExecuteLoad(int id)
        {
            try
            {
                var data = await _executeLoadService.GetByIdAsync(id);
                var map = _mapper.Map<ExecuteLoadViewModel>(data);

                return Ok(map);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);

                return Ok();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateExecuteLoad(ExecuteLoadViewModel executeLoad)
        {
            try
            {
                var map = _mapper.Map<ExecuteLoad>(executeLoad);
                await _executeLoadService.CreateAsync(map);

                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateExecuteLoad(ExecuteLoadViewModel executeLoad)
        {
            try
            {
                var map = _mapper.Map<ExecuteLoad>(executeLoad);
                await _executeLoadService.UpdateAsync(map);

                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);

                return Ok(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExecuteLoad(int id)
        {
            try
            {
                var data = await _executeLoadService.GetByIdAsync(id);
                var map = _mapper.Map<ExecuteLoad>(data);
                await _executeLoadService.DeleteAsync(map);

                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);

                return Ok(ex.Message);
            }
        }
    }
}
