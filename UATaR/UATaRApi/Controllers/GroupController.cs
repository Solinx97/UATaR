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
    public class GroupController : Controller
    {
        private readonly IService<Group, int> _groupService;
        private readonly IMapper _mapper;
        private readonly ILogger<GroupController> _logger;

        public GroupController(IService<Group, int> groupService,
            IMapper mapper,
            ILogger<GroupController> logger)
        {
            _groupService = groupService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetGroups()
        {
            try
            {
                var data = await _groupService.GetAllAsync();
                var map = _mapper.Map<List<GroupViewModel>>(data);

                return Ok(map);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                var data = new List<GroupViewModel>();

                return Ok(data);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroup(int id)
        {
            try
            {
                var data = await _groupService.GetByIdAsync(id);
                var map = _mapper.Map<GroupViewModel>(data);

                return Ok(map);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);

                return Ok();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup(GroupViewModel group)
        {
            try
            {
                var map = _mapper.Map<Group>(group);
                await _groupService.CreateAsync(map);

                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGroup(GroupViewModel group)
        {
            try
            {
                var map = _mapper.Map<Group>(group);
                await _groupService.UpdateAsync(map);

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
        public async Task<IActionResult> DeleteGroup(int id)
        {
            try
            {
                var data = await _groupService.GetByIdAsync(id);
                var map = _mapper.Map<Group>(data);
                await _groupService.DeleteAsync(map);

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
