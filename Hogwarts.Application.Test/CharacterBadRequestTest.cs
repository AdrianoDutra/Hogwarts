using Hogwarts.Domain.Dtos;
using Hogwarts.Domain.Interfaces.Service;
using Hogwarts.Application.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Hogwarts.Application.Test
{
    public class CharacterBadRequestTest
    {
        private CharacterController _controller;

        private Mock<ICharacterService> _serviceMock;

        public CharacterBadRequestTest()
        {
            _serviceMock = new Mock<ICharacterService>();
            _controller = new CharacterController(_serviceMock.Object);
        }

        [Fact(DisplayName = "Updated")]
        public async Task Controller_Update()
        {

            var name = Faker.Name.FullName();
            var house = Guid.NewGuid().ToString();
            var school = Faker.Name.FullName();
            var patronus = Faker.Name.Last();
            var role = Faker.Name.FullName();

            _serviceMock.Setup(m => m.Put(It.IsAny<CharacterUpdateDto>())).ReturnsAsync(
                new CharacterResultDto
                {
                    id = Guid.NewGuid(),
                    name = name,
                    house = house,
                    school = school,
                    patronus = patronus,
                    role = role
                }
            );


            _controller.ModelState.AddModelError("school", "É um campo obrigatorio!");

            var userDtoUpdate = new CharacterUpdateDto
            {
                id = Guid.NewGuid(),
                name = name,
                house = house,
                school = school,
                patronus = patronus,
                role = role
            };

            var result = await _controller.Put(userDtoUpdate);
            Assert.True(result is BadRequestObjectResult);
            Assert.False(_controller.ModelState.IsValid);
        }

        [Fact(DisplayName = "GetAllCharacterHouse")]
        public async Task Controller_GetAllCharacterHouse()
        {

            _serviceMock.Setup(m => m.GetAllCharacterHouse(It.IsAny<string>())).ReturnsAsync(
                 new List<CharacterResultDto>
                 {
                    new CharacterResultDto
                    {
                       id = Guid.NewGuid(),
                       name = Faker.Name.FullName(),
                       house = Guid.NewGuid().ToString(),
                       school = Faker.Name.FullName(),
                       patronus = Faker.Name.Last(),
                       role = Faker.Name.FullName()
                    },
                    new CharacterResultDto
                    {
                       id = Guid.NewGuid(),
                       name = Faker.Name.FullName(),
                       house = Guid.NewGuid().ToString(),
                       school = Faker.Name.FullName(),
                       patronus = Faker.Name.Last(),
                       role = Faker.Name.FullName()
                    }
                 });
            _controller.ModelState.AddModelError("Id", "Formato Invalido");

            var result = await _controller.GetAllCharacterHouse(It.IsAny<string>());
            Assert.True(result is BadRequestObjectResult);
        }


        [Fact(DisplayName = "Get")]
        public async Task Controller_Get()
        {

            var nome = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            _serviceMock.Setup(m => m.Get(It.IsAny<Guid>())).ReturnsAsync(
                 new CharacterResultDto
                 {
                     id = Guid.NewGuid(),
                     name = Faker.Name.FullName(),
                     house = Guid.NewGuid().ToString(),
                     school = Faker.Name.FullName(),
                     patronus = Faker.Name.Last(),
                     role = Faker.Name.FullName()
                 }
            );

            _controller.ModelState.AddModelError("Id", "Formato Inválido");

            var result = await _controller.Get(Guid.NewGuid());
            Assert.True(result is BadRequestObjectResult);

        }

        [Fact(DisplayName = "Deleted.")]
        public async Task Controller_Delete()
        {

            _serviceMock.Setup(m => m.Delete(It.IsAny<Guid>()))
                       .ReturnsAsync(false);

            _controller.ModelState.AddModelError("Id", "Formato Inválido");

            var result = await _controller.Delete(default(Guid));
            Assert.True(result is BadRequestObjectResult);
            Assert.False(_controller.ModelState.IsValid);
        }

        [Fact(DisplayName = "Created")]
        public async Task Controller_Create()
        {


            _serviceMock.Setup(m => m.Post(It.IsAny<CharacterInsertDto>())).ReturnsAsync(
                new CharacterResultDto
                {
                    id = Guid.NewGuid(),
                    name = Faker.Name.FullName(),
                    house = Guid.NewGuid().ToString(),
                    school = Faker.Name.FullName(),
                    patronus = Faker.Name.Last(),
                    role = Faker.Name.FullName()
                }
            );

            _controller.ModelState.AddModelError("Name", "É um Campo Obrigatório");

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
            _controller.Url = url.Object;

            var characterInsertDto = new CharacterInsertDto
            {
                name = Faker.Name.FullName(),
                house = Guid.NewGuid().ToString(),
                school = Faker.Name.FullName(),
                patronus = Faker.Name.Last(),
                role = Faker.Name.FullName()
            };

            var result = await _controller.Post(characterInsertDto);
            Assert.True(result is BadRequestObjectResult);

        }
    }
}
