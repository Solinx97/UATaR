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
    internal class LoadService : IService<Load, int>
    {
        private readonly IGenericRepository<LoadDto, int> _loadRepository;
        private readonly IMapper _mapper;

        public LoadService(IGenericRepository<LoadDto, int> loadRepository,
                IMapper mapper)
        {
            _loadRepository = loadRepository;
            _mapper = mapper;
        }

        public Task<int> CreateAsync(Load item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return CreateInternalAsync(item);
        }

        public Task DeleteAsync(Load item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return DeleteInternalAsync(item);
        }

        public async Task<List<Load>> GetAllAsync()
        {
            var allData = await _loadRepository.GetAllAsync();
            if (!allData.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Load)} not found", nameof(allData));
            }

            var result = _mapper.Map<List<Load>>(allData);
            return result;
        }

        public async Task<Load> GetByIdAsync(int id)
        {
            var executeLoad = await _loadRepository.GetByIdAsync(id);
            if (executeLoad == null)
            {
                throw new NotFoundException($"Entity {nameof(Load)} by Id not found", nameof(executeLoad));
            }

            var result = _mapper.Map<Load>(executeLoad);
            return result;
        }

        public Task UpdateAsync(Load item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return UpdateInternalAsync(item);
        }

        private async Task<int> CreateInternalAsync(Load item)
        {
            await _loadRepository.CreateAsync(_mapper.Map<LoadDto>(item));

            var allData = await _loadRepository.GetAllAsync();
            var executeLoad = allData.Last();

            return executeLoad.Id;
        }

        private async Task DeleteInternalAsync(Load item)
        {
            var allData = await _loadRepository.GetAllAsync();
            if (!allData.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Load)} not found", nameof(allData));
            }

            await _loadRepository.DeleteAsync(_mapper.Map<LoadDto>(item));
        }

        private async Task UpdateInternalAsync(Load item)
        {
            var allData = await _loadRepository.GetAllAsync();
            if (!allData.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Load)} not found", nameof(allData));
            }

            await _loadRepository.UpdateAsync(_mapper.Map<LoadDto>(item));
        }
    }
}
