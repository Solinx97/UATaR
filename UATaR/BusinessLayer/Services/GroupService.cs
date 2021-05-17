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
    internal class GroupService : IService<Group, int>
    {
        private readonly IGenericRepository<GroupDto, int> _groupRepository;
        private readonly IMapper _mapper;

        public GroupService(IGenericRepository<GroupDto, int> groupRepository,
                IMapper mapper)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        public Task<int> CreateAsync(Group item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return CreateInternalAsync(item);
        }

        public Task DeleteAsync(Group item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return DeleteInternalAsync(item);
        }

        public async Task<List<Group>> GetAllAsync()
        {
            var allData = await _groupRepository.GetAllAsync();
            if (!allData.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Group)} not found", nameof(allData));
            }

            var result = _mapper.Map<List<Group>>(allData);
            return result;
        }

        public async Task<Group> GetByIdAsync(int id)
        {
            var executeLoad = await _groupRepository.GetByIdAsync(id);
            if (executeLoad == null)
            {
                throw new NotFoundException($"Entity {nameof(Group)} by Id not found", nameof(executeLoad));
            }

            var result = _mapper.Map<Group>(executeLoad);
            return result;
        }

        public Task UpdateAsync(Group item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return UpdateInternalAsync(item);
        }

        private async Task<int> CreateInternalAsync(Group item)
        {
            if (string.IsNullOrEmpty(item.Name))
            {
                throw new ArgumentNullException(nameof(item.Name));
            }

            await _groupRepository.CreateAsync(_mapper.Map<GroupDto>(item));

            var allData = await _groupRepository.GetAllAsync();
            var executeLoad = allData.Last();

            return executeLoad.Id;
        }

        private async Task DeleteInternalAsync(Group item)
        {
            var allData = await _groupRepository.GetAllAsync();
            if (!allData.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Group)} not found", nameof(allData));
            }

            await _groupRepository.DeleteAsync(_mapper.Map<GroupDto>(item));
        }

        private async Task UpdateInternalAsync(Group item)
        {
            var allData = await _groupRepository.GetAllAsync();
            if (!allData.Any())
            {
                throw new NotFoundException($"Collection entity {nameof(Group)} not found", nameof(allData));
            }

            if (string.IsNullOrEmpty(item.Name))
            {
                throw new ArgumentNullException(nameof(item.Name));
            }

            await _groupRepository.UpdateAsync(_mapper.Map<GroupDto>(item));
        }
    }
}
