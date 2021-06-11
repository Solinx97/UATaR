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
    internal class SpecialityService : IService<Speciality, int>
    {
        private readonly IGenericRepository<SpecialityDto, int> _specialityRepository;
        private readonly IMapper _mapper;

        public SpecialityService(IGenericRepository<SpecialityDto, int> specialityRepository,
                IMapper mapper)
        {
            _specialityRepository = specialityRepository;
            _mapper = mapper;
        }

        public Task<int> CreateAsync(Speciality item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return CreateInternalAsync(item);
        }

        public Task DeleteAsync(Speciality item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return DeleteInternalAsync(item);
        }

        public async Task<List<Speciality>> GetAllAsync()
        {
            var allData = await _specialityRepository.GetAllAsync();
            if (!allData.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Speciality)} not found", nameof(allData));
            }

            var result = _mapper.Map<List<Speciality>>(allData);
            return result;
        }

        public async Task<Speciality> GetByIdAsync(int id)
        {
            var executeLoad = await _specialityRepository.GetByIdAsync(id);
            if (executeLoad == null)
            {
                throw new NotFoundException($"Entity {nameof(Speciality)} by Id not found", nameof(executeLoad));
            }

            var result = _mapper.Map<Speciality>(executeLoad);
            return result;
        }

        public Task UpdateAsync(Speciality item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return UpdateInternalAsync(item);
        }

        private async Task<int> CreateInternalAsync(Speciality item)
        {
            if (string.IsNullOrEmpty(item.Name))
            {
                throw new ArgumentNullException(nameof(item.Name));
            }

            await _specialityRepository.CreateAsync(_mapper.Map<SpecialityDto>(item));

            var allData = await _specialityRepository.GetAllAsync();
            var executeLoad = allData.Last();

            return executeLoad.Id;
        }

        private async Task DeleteInternalAsync(Speciality item)
        {
            var allData = await _specialityRepository.GetAllAsync();
            if (!allData.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Speciality)} not found", nameof(allData));
            }

            await _specialityRepository.DeleteAsync(_mapper.Map<SpecialityDto>(item));
        }

        private async Task UpdateInternalAsync(Speciality item)
        {
            var allData = await _specialityRepository.GetAllAsync();
            if (!allData.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Speciality)} not found", nameof(allData));
            }

            if (string.IsNullOrEmpty(item.Name))
            {
                throw new ArgumentNullException(nameof(item.Name));
            }

            await _specialityRepository.UpdateAsync(_mapper.Map<SpecialityDto>(item));
        }
    }
}
