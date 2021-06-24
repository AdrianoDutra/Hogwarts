using Hogwarts.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hogwarts.Domain.Interfaces.Repository
{
    public interface ICharacterRepository : IRepository<CharacterEntity>
    {
        Task<IEnumerable<CharacterEntity>> SelectAllCharacterHouseAsync(string House);
    }
}
