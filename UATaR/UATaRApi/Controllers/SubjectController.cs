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
    public class SubjectController : Controller
    {
        private readonly IService<Subject, int> _subjectService;
        private readonly IMapper _mapper;
        private readonly ILogger<SubjectController> _logger;

        public SubjectController(IService<Subject, int> subjectService,
            IMapper mapper,
            ILogger<SubjectController> logger)
        {
            _subjectService = subjectService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetSubjects()
        {
            try
            {
                var data = await _subjectService.GetAllAsync();
                var map = _mapper.Map<List<SubjectViewModel>>(data);

                return Ok(map);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                var data = new List<SubjectViewModel>();

                return Ok(data);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubject(int id)
        {
            try
            {
                var data = await _subjectService.GetByIdAsync(id);
                var map = _mapper.Map<SubjectViewModel>(data);

                return Ok(map);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);

                return Ok();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubject(SubjectViewModel subject)
        {
            try
            {
                var map = _mapper.Map<Subject>(subject);
                await _subjectService.CreateAsync(map);

                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSubject(SubjectViewModel subject)
        {
            try
            {
                var map = _mapper.Map<Subject>(subject);
                await _subjectService.UpdateAsync(map);

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
        public async Task<IActionResult> DeleteSubject(int id)
        {
            try
            {
                var data = await _subjectService.GetByIdAsync(id);
                var map = _mapper.Map<Subject>(data);
                await _subjectService.DeleteAsync(map);

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
    }
}