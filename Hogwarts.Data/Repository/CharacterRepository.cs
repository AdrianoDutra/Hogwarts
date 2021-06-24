using Hogwarts.Data.Context;
using Hogwarts.Domain.Entities;
using Hogwarts.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.Data.Repository
{
    public class CharacterRepository : Repository<CharacterEntity>, ICharacterRepository
    {
        private DbSet<CharacterEntity> _dbSet;

        public CharacterRepository(MyContext context) : base(context)
        {
            _dbSet = context.Set<CharacterEntity>();
        }        
        
       
        public async Task<IEnumerable<CharacterEntity>> SelectAllCharacterHouseAsync(string house)
        {
            return await _dbSet.Where(T => T.house == house).ToListAsync();
        }
    }
}
