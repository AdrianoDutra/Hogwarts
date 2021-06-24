using AutoMapper;
using Hogwarts.Domain.Dtos;
using Hogwarts.Domain.Entities;
using Hogwarts.Domain.Interfaces.Repository;
using Hogwarts.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hogwarts.Service.Services
{
    public class CharacterService : ICharacterService
    {
        private ICharacterRepository _repository;
        private readonly IMapper _mapper;

        public CharacterService(ICharacterRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<CharacterResultDto> Get(Guid id)
        {
            var result = await _repository.SelectAsync(id);
            return _mapper.Map<CharacterResultDto>(result) ?? new CharacterResultDto();
        }

        public async Task<IEnumerable<CharacterResultDto>> GetAllCharacterHouse(string house)
        {
            var result = await _repository.SelectAllCharacterHouseAsync(house);
            return _mapper.Map<IEnumerable<CharacterResultDto>>(result) ?? null;
        }

        public async Task<CharacterResultDto> Post(CharacterInsertDto character)
        {
            var entity = _mapper.Map<CharacterEntity>(character);
            var result = await _repository.InsertAsync(entity);
            return _mapper.Map<CharacterResultDto>(result);
        }

        public async Task<CharacterResultDto> Put(CharacterUpdateDto character)
        {
            var entity = _mapper.Map<CharacterEntity>(character);
            var result = await _repository.UpdateAsync(entity);
            return _mapper.Map<CharacterResultDto>(result);
        }
    }
}
