using AutoMapper;
using BusinessLayer.Entities;
using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    internal class TeacherService : IService<Teacher, int>
    {
        private readonly IGenericRepository<TeacherDto, int> _teacherRepository;
        private readonly IMapper _mapper;

        public TeacherService(IGenericRepository<TeacherDto, int> teacherRepository,
                IMapper mapper)
        {
            _teacherRepository = teacherRepository;
            _mapper = mapper;
        }

        public Task<int> CreateAsync(Teacher item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return CreateInternalAsync(item);
        }

        public Task DeleteAsync(Teacher item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return DeleteInternalAsync(item);
        }

        public async Task<List<Teacher>> GetAllAsync()
        {
            var allData = await _teacherRepository.GetAllAsync();
            if (!allData.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Teacher)} not found", nameof(allData));
            }

            var result = _mapper.Map<List<Teacher>>(allData);
            return result;
        }

        public async Task<Teacher> GetByIdAsync(int id)
        {
            var executeLoad = await _teacherRepository.GetByIdAsync(id);
            if (executeLoad == null)
            {
                throw new NotFoundException($"Entity {nameof(Teacher)} by Id not found", nameof(executeLoad));
            }

            var result = _mapper.Map<Teacher>(executeLoad);
            return result;
        }

        public Task UpdateAsync(Teacher item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return UpdateInternalAsync(item);
        }

        private async Task<int> CreateInternalAsync(Teacher item)
        {
            if (item.Birthday > DateTimeOffset.Now)
            {
                throw new DateException("Date of birthday can't be in the future tense", nameof(item.Birthday));
            }

            await _teacherRepository.CreateAsync(_mapper.Map<TeacherDto>(item));

            var allData = await _teacherRepository.GetAllAsync();
            var executeLoad = allData.Last();

            return executeLoad.Id;
        }

        private async Task DeleteInternalAsync(Teacher item)
        {
            var allData = await _teacherRepository.GetAllAsync();
            if (!allData.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Teacher)} not found", nameof(allData));
            }

            await _teacherRepository.DeleteAsync(_mapper.Map<TeacherDto>(item));
        }

        private async Task UpdateInternalAsync(Teacher item)
        {
            var allData = await _teacherRepository.GetAllAsync();
            if (!allData.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Teacher)} not found", nameof(allData));
            }

            if (item.Birthday > DateTimeOffset.Now)
            {
                throw new DateException("Date of birthday can't be in the future tense", nameof(item.Birthday));
            }

            await _teacherRepository.UpdateAsync(_mapper.Map<TeacherDto>(item));
        }
    }
}
