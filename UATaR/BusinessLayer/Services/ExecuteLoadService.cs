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
    internal class ExecuteLoadService : IService<ExecuteLoad, int>
    {
        private readonly IGenericRepository<ExecuteLoadDto, int> _executeLoadRepository;
        private readonly IMapper _mapper;

        public ExecuteLoadService(IGenericRepository<ExecuteLoadDto, int> executeLoadRepository,
                IMapper mapper)
        {
            _executeLoadRepository = executeLoadRepository;
            _mapper = mapper;
        }

        public Task<int> CreateAsync(ExecuteLoad item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return CreateInternalAsync(item);
        }

        public Task DeleteAsync(ExecuteLoad item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return DeleteInternalAsync(item);
        }

        public async Task<List<ExecuteLoad>> GetAllAsync()
        {
            var allData = await _executeLoadRepository.GetAllAsync();
            if (!allData.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(ExecuteLoad)} not found", nameof(allData));
            }

            var result = _mapper.Map<List<ExecuteLoad>>(allData);
            return result;
        }

        public async Task<ExecuteLoad> GetByIdAsync(int id)
        {
            var executeLoad = await _executeLoadRepository.GetByIdAsync(id);
            if (executeLoad == null)
            {
                throw new NotFoundException($"Entity {nameof(ExecuteLoad)} by Id not found", nameof(executeLoad));
            }

            var result = _mapper.Map<ExecuteLoad>(executeLoad);
            return result;
        }

        public Task UpdateAsync(ExecuteLoad item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return UpdateInternalAsync(item);
        }

        private async Task<int> CreateInternalAsync(ExecuteLoad item)
        {
            if (item.Hours < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(item.Hours), "Количество часов не может быть отрицательным");
            }

            await _executeLoadRepository.CreateAsync(_mapper.Map<ExecuteLoadDto>(item));

            var allData = await _executeLoadRepository.GetAllAsync();
            var executeLoad = allData.Last();

            return executeLoad.Id;
        }

        private async Task DeleteInternalAsync(ExecuteLoad item)
        {
            var allData = await _executeLoadRepository.GetAllAsync();
            if (!allData.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(ExecuteLoad)} not found", nameof(allData));
            }

            await _executeLoadRepository.DeleteAsync(_mapper.Map<ExecuteLoadDto>(item));
        }

        private async Task UpdateInternalAsync(ExecuteLoad item)
        {
            var allData = await _executeLoadRepository.GetAllAsync();
            if (!allData.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(ExecuteLoad)} not found", nameof(allData));
            }

            if (item.Hours < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(item.Hours), "Количество часов не может быть отрицательным");
            }

            await _executeLoadRepository.UpdateAsync(_mapper.Map<ExecuteLoadDto>(item));
        }
    }
}