using Hogwarts.Data.Context;
using Hogwarts.Data.Repository;
using Hogwarts.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Hogwarts.Data.Test
{
    public class CharacterRepositoryTest : BaseTest, IClassFixture<DbTeste>
    {
        private ServiceProvider _serviceProvide;

        public CharacterRepositoryTest(DbTeste dbTeste)
        {
            _serviceProvide = dbTeste.ServiceProvider;
        }

        [Fact(DisplayName = "CRUD de Personagem (Character)")]
        [Trait("CRUD", "CharacterEntity")]
        public async Task Test_CRUD_Character()
        {
            using (var context = _serviceProvide.GetService<MyContext>())
            {
                CharacterRepository _repository = new CharacterRepository(context);

                CharacterEntity _entity = new CharacterEntity
                {
                    name = Faker.Name.FullName(),
                    role = "student",
                    school = "Hogwarts School of Witchcraft and Wizardry",
                    house = "1760529f-6d51-4cb1-bcb1-25087fce5bde",
                    patronus= "stag"
                };
                
                //Teste Insert
                var _registroCriado = await _repository.InsertAsync(_entity);
                Assert.NotNull(_registroCriado);
                Assert.Equal(_entity.name, _registroCriado.name);
                Assert.Equal(_entity.role, _registroCriado.role);
                Assert.Equal(_entity.school, _registroCriado.school);
                Assert.Equal(_entity.patronus, _registroCriado.patronus);
                Assert.False(_registroCriado.id == Guid.Empty);


                //Teste Update
                _entity.name = Faker.Name.First();
                var _registroAtualizado = await _repository.UpdateAsync(_entity);
                Assert.NotNull(_registroAtualizado);
                Assert.Equal(_entity.name, _registroAtualizado.name);
                Assert.Equal(_entity.role, _registroAtualizado.role);
                Assert.Equal(_entity.school, _registroAtualizado.school);
                Assert.Equal(_entity.patronus, _registroAtualizado.patronus);

                //Teste se existe
                var _registroExiste = await _repository.ExistAsync(_registroAtualizado.id);
                Assert.True(_registroExiste);

                //Teste consulta
                var _registroSelecionado = await _repository.SelectAsync(_registroAtualizado.id);
                Assert.NotNull(_registroSelecionado);
                Assert.Equal(_registroSelecionado.name, _registroAtualizado.name);
                Assert.Equal(_registroSelecionado.role, _registroAtualizado.role);
                Assert.Equal(_registroSelecionado.school, _registroAtualizado.school);
                Assert.Equal(_registroSelecionado.patronus, _registroAtualizado.patronus);

                //teste busca por casas
                var _todosRegistros = await _repository.SelectAllCharacterHouseAsync(_registroAtualizado.house);
                Assert.NotNull(_todosRegistros);
                Assert.True(_todosRegistros.Count() > 0);

                var _removeu = await _repository.DeleteAsync(_registroSelecionado.id);
                Assert.True(_removeu);

            }
        }
    }
}
