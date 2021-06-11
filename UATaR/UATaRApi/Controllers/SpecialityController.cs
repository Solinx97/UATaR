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
    public class SpecialityController : Controller
    {
        private readonly IService<Speciality, int> _specialityService;
        private readonly IMapper _mapper;
        private readonly ILogger<SpecialityController> _logger;

        public SpecialityController(IService<Speciality, int> specialityService,
            IMapper mapper,
            ILogger<SpecialityController> logger)
        {
            _specialityService = specialityService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetSpecialiies()
        {
            try
            {
                var data = await _specialityService.GetAllAsync();
                var map = _mapper.Map<List<SpecialityViewModel>>(data);

                return Ok(map);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                var data = new List<SpecialityViewModel>();

                return Ok(data);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpeciality(int id)
        {
            try
            {
                var data = await _specialityService.GetByIdAsync(id);
                var map = _mapper.Map<SpecialityViewModel>(data);

                return Ok(map);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);

                return Ok();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpeciality(SpecialityViewModel speciality)
        {
            try
            {
                var map = _mapper.Map<Speciality>(speciality);
                await _specialityService.CreateAsync(map);

                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSpeciality(SpecialityViewModel speciality)
        {
            try
            {
                var map = _mapper.Map<Speciality>(speciality);
                await _specialityService.UpdateAsync(map);

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

                return Ok();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpeciality(int id)
        {
            try
            {
                var data = await _specialityService.GetByIdAsync(id);
                var map = _mapper.Map<Speciality>(data);
                await _specialityService.DeleteAsync(map);

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