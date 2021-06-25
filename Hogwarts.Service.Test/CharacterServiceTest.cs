using Hogwarts.Domain.Dtos;
using Hogwarts.Domain.Interfaces.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Hogwarts.Service.Test
{

    public class CharacterServiceTest : CharacterDtoTest
    {
        private ICharacterService _service;
        private Mock<ICharacterService> _serviceMock;

        [Fact(DisplayName = "Método GET ID.")]
        public async Task Metodo_Get_Id()
        {
            _serviceMock = new Mock<ICharacterService>();
            _serviceMock.Setup(m => m.Get(idChar)).ReturnsAsync(characterResultDto);
            _service = _serviceMock.Object;

            var result = await _service.Get(idChar);
            Assert.NotNull(result);
            Assert.True(result.id == idChar);
            Assert.Equal(nameChar, result.name);

            _serviceMock = new Mock<ICharacterService>();
            _serviceMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(Task.FromResult((CharacterResultDto)null));
            _service = _serviceMock.Object;

            var _record = await _service.Get(idChar);
            Assert.Null(_record);

        }
        [Fact(DisplayName = "Método GET Character House.")]
        public async Task Metodo_Get_Character_House()
        {
            _serviceMock = new Mock<ICharacterService>();
            _serviceMock.Setup(m => m.GetAllCharacterHouse(houseChar)).ReturnsAsync(listResult);
            _service = _serviceMock.Object;

            var result = await _service.GetAllCharacterHouse(houseChar);
            Assert.NotNull(result);
            Assert.True(result.Count() == 10);

            var _listResult = new List<CharacterResultDto>();
            _serviceMock = new Mock<ICharacterService>();
            _serviceMock.Setup(m => m.GetAllCharacterHouse(houseChar)).ReturnsAsync(_listResult.AsEnumerable);
            _service = _serviceMock.Object;

            var _resultEmpty = await _service.GetAllCharacterHouse(houseChar);
            Assert.Empty(_resultEmpty);
            Assert.True(_resultEmpty.Count() == 0);

        }

        [Fact(DisplayName = "Método Put (update).")]
        public async Task Metodo_Put()
        {
            var characterResultaltDto = new CharacterResultDto()
            {
                house = characterUpdateDto.house,
                id = characterUpdateDto.id,
                name = characterUpdateDto.name,
                patronus = characterUpdateDto.patronus,
                role = characterUpdateDto.role,
                school = characterUpdateDto.school
            };

            _serviceMock = new Mock<ICharacterService>();
            _serviceMock.Setup(m => m.Put(characterUpdateDto)).ReturnsAsync(characterResultaltDto);
            _service = _serviceMock.Object;

            var result = await _service.Put(characterUpdateDto);
            Assert.NotNull(result);
            Assert.Equal(nameAlteradoChar, result.name);
            Assert.Equal(roleAlteradoChar, result.role);
            Assert.Equal(houseAlteradoChar, result.house);
            Assert.Equal(patronusAlteradoChar, result.patronus);
            Assert.Equal(schoolAlteradoChar, result.school);
        }


        [Fact(DisplayName = "Método Post (Insert)")]
        public async Task E_Possivel_Executar_Metodo_Create()
        {
            _serviceMock = new Mock<ICharacterService>();
            _serviceMock.Setup(m => m.Post(characterInsertDto)).ReturnsAsync(characterResultDto);
            _service = _serviceMock.Object;

            var result = await _service.Post(characterInsertDto);
            Assert.NotNull(result);
            Assert.Equal(nameChar, result.name);
            Assert.Equal(roleChar, result.role);
            Assert.Equal(houseChar, result.house);
            Assert.Equal(patronusChar, result.patronus);
            Assert.Equal(schoolChar, result.school);
        }
     
        [Fact(DisplayName = "Método Delete.")]
        public async Task E_Possivel_Executar_Metodo_Delete()
        {
            _serviceMock = new Mock<ICharacterService>();
            _serviceMock.Setup(m => m.Delete(idChar))
                        .ReturnsAsync(true);
            _service = _serviceMock.Object;

            var deletado = await _service.Delete(idChar);
            Assert.True(deletado);

            _serviceMock = new Mock<ICharacterService>();
            _serviceMock.Setup(m => m.Delete(It.IsAny<Guid>()))
                        .ReturnsAsync(false);
            _service = _serviceMock.Object;

            deletado = await _service.Delete(Guid.NewGuid());
            Assert.False(deletado);

        }

    }
}
