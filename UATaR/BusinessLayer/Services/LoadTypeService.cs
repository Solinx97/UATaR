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
    internal class LoadTypeService : IService<LoadType, int>
    {
        private readonly IGenericRepository<LoadTypeDto, int> _loadTypeRepository;
        private readonly IMapper _mapper;

        public LoadTypeService(IGenericRepository<LoadTypeDto, int> loadTypeRepository,
                IMapper mapper)
        {
            _loadTypeRepository = loadTypeRepository;
            _mapper = mapper;
        }

        public Task<int> CreateAsync(LoadType item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return CreateInternalAsync(item);
        }

        public Task DeleteAsync(LoadType item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return DeleteInternalAsync(item);
        }

        public async Task<List<LoadType>> GetAllAsync()
        {
            var allData = await _loadTypeRepository.GetAllAsync();
            if (!allData.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(LoadType)} not found", nameof(allData));
            }

            var result = _mapper.Map<List<LoadType>>(allData);
            return result;
        }

        public async Task<LoadType> GetByIdAsync(int id)
        {
            var executeLoad = await _loadTypeRepository.GetByIdAsync(id);
            if (executeLoad == null)
            {
                throw new NotFoundException($"Entity {nameof(LoadType)} by Id not found", nameof(executeLoad));
            }

            var result = _mapper.Map<LoadType>(executeLoad);
            return result;
        }

        public Task UpdateAsync(LoadType item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return UpdateInternalAsync(item);
        }

        private async Task<int> CreateInternalAsync(LoadType item)
        {
            await _loadTypeRepository.CreateAsync(_mapper.Map<LoadTypeDto>(item));

            var allData = await _loadTypeRepository.GetAllAsync();
            var executeLoad = allData.Last();

            return executeLoad.Id;
        }

        private async Task DeleteInternalAsync(LoadType item)
        {
            var allData = await _loadTypeRepository.GetAllAsync();
            if (!allData.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(LoadType)} not found", nameof(allData));
            }

            await _loadTypeRepository.DeleteAsync(_mapper.Map<LoadTypeDto>(item));
        }

        private async Task UpdateInternalAsync(LoadType item)
        {
            var allData = await _loadTypeRepository.GetAllAsync();
            if (!allData.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(LoadType)} not found", nameof(allData));
            }

            await _loadTypeRepository.UpdateAsync(_mapper.Map<LoadTypeDto>(item));
        }
    }
}
