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
    internal class SubjectService : IService<Subject, int>
    {
        private readonly IGenericRepository<SubjectDto, int> _subjectRepository;
        private readonly IMapper _mapper;

        public SubjectService(IGenericRepository<SubjectDto, int> subjectRepository,
                IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }

        public Task<int> CreateAsync(Subject item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return CreateInternalAsync(item);
        }

        public Task DeleteAsync(Subject item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return DeleteInternalAsync(item);
        }

        public async Task<List<Subject>> GetAllAsync()
        {
            var allData = await _subjectRepository.GetAllAsync();
            if (!allData.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Subject)} not found", nameof(allData));
            }

            var result = _mapper.Map<List<Subject>>(allData);
            return result;
        }

        public async Task<Subject> GetByIdAsync(int id)
        {
            var executeLoad = await _subjectRepository.GetByIdAsync(id);
            if (executeLoad == null)
            {
                throw new NotFoundException($"Entity {nameof(Subject)} by Id not found", nameof(executeLoad));
            }

            var result = _mapper.Map<Subject>(executeLoad);
            return result;
        }

        public Task UpdateAsync(Subject item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return UpdateInternalAsync(item);
        }

        private async Task<int> CreateInternalAsync(Subject item)
        {
            if (string.IsNullOrEmpty(item.Name))
            {
                throw new ArgumentNullException(nameof(item.Name));
            }

            await _subjectRepository.CreateAsync(_mapper.Map<SubjectDto>(item));

            var allData = await _subjectRepository.GetAllAsync();
            var executeLoad = allData.Last();

            return executeLoad.Id;
        }

        private async Task DeleteInternalAsync(Subject item)
        {
            var allData = await _subjectRepository.GetAllAsync();
            if (!allData.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Subject)} not found", nameof(allData));
            }

            await _subjectRepository.DeleteAsync(_mapper.Map<SubjectDto>(item));
        }

        private async Task UpdateInternalAsync(Subject item)
        {
            var allData = await _subjectRepository.GetAllAsync();
            if (!allData.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Subject)} not found", nameof(allData));
            }

            if (string.IsNullOrEmpty(item.Name))
            {
                throw new ArgumentNullException(nameof(item.Name));
            }

            await _subjectRepository.UpdateAsync(_mapper.Map<SubjectDto>(item));
        }
    }
}
