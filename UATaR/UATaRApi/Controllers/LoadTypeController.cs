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
    public class LoadTypeController : Controller
    {
        private readonly IService<LoadType, int> _LoadTypeService;
        private readonly IMapper _mapper;
        private readonly ILogger<LoadTypeController> _logger;

        public LoadTypeController(IService<LoadType, int> loadTypeService,
            IMapper mapper,
            ILogger<LoadTypeController> logger)
        {
            _LoadTypeService = loadTypeService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetLoadTypes()
        {
            try
            {
                var data = await _LoadTypeService.GetAllAsync();
                var map = _mapper.Map<List<LoadTypeViewModel>>(data);

                return Ok(map);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                var data = new List<LoadTypeViewModel>();

                return Ok(data);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLoadType(int id)
        {
            try
            {
                var data = await _LoadTypeService.GetByIdAsync(id);
                var map = _mapper.Map<LoadTypeViewModel>(data);

                return Ok(map);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);

                return Ok();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateLoadType(LoadTypeViewModel loadType)
        {
            try
            {
                var map = _mapper.Map<LoadType>(loadType);
                await _LoadTypeService.CreateAsync(map);

                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLoadType(LoadTypeViewModel loadType)
        {
            try
            {
                var map = _mapper.Map<LoadType>(loadType);
                await _LoadTypeService.UpdateAsync(map);

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoaTyped(int id)
        {
            try
            {
                var data = await _LoadTypeService.GetByIdAsync(id);
                var map = _mapper.Map<LoadType>(data);
                await _LoadTypeService.DeleteAsync(map);

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