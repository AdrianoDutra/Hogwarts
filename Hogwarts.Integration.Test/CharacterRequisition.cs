using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Hogwarts.Domain.Dtos;
using Newtonsoft.Json;
using Xunit;

namespace Hogwarts.Integration.Test
{
    public class CharacterRequisition : BaseIntegration
    {
        private string nameChar { get; set; }
        private string roleChar { get; set; }
        private string schoolChar { get; set; }
        private string houseChar { get; set; }
        private string patronusChar { get; set; }

        [Fact]
        public async Task Character_Test()
        {

            nameChar = Faker.Name.FullName();
            roleChar = Faker.Name.FullName();
            schoolChar = Faker.Name.FullName();
            houseChar = Guid.NewGuid().ToString();
            patronusChar = Faker.Name.Last();

            var insertDto = new CharacterInsertDto()
            {
                house = houseChar,
                name = nameChar,
                patronus = patronusChar,
                role = roleChar,
                school = schoolChar
            };

            //Post
            var response = await PostJsonAsync(insertDto, $"{hostApi}character", client);
            var postResult = await response.Content.ReadAsStringAsync();
            var registroPost = JsonConvert.DeserializeObject<CharacterResultDto>(postResult);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(nameChar, registroPost.name);
            Assert.Equal(roleChar, registroPost.role);
            Assert.Equal(schoolChar, registroPost.school);
            Assert.Equal(patronusChar, registroPost.patronus);
            Assert.True(registroPost.id != default(Guid));

            //Get All
            response = await client.GetAsync($"{hostApi}character/GetAllCharacterHouse/house={insertDto.house}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var listaFromJson = JsonConvert.DeserializeObject<IEnumerable<CharacterResultDto>>(jsonResult);
            Assert.NotNull(listaFromJson);
            Assert.True(listaFromJson.Count() > 0);
            Assert.True(listaFromJson.Where(r => r.id == registroPost.id).Count() == 1);

            var UpdateDto = new CharacterUpdateDto()
            {
                id = registroPost.id,
                name = Faker.Name.FullName(),
                role = Faker.Name.FullName(),
                school = Faker.Name.FullName(),
                house = Guid.NewGuid().ToString(),
                patronus = Faker.Name.Last()
            };

            //PUT
            var stringContent = new StringContent(JsonConvert.SerializeObject(UpdateDto),
                                    Encoding.UTF8, "application/json");
            response = await client.PutAsync($"{hostApi}character", stringContent);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroAtualizado = JsonConvert.DeserializeObject<CharacterResultDto>(jsonResult);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);            
            Assert.NotEqual(registroAtualizado.name, registroPost.name);
            Assert.NotEqual(registroAtualizado.role, registroPost.role);
            Assert.NotEqual(registroAtualizado.school, registroPost.school);
            Assert.NotEqual(registroAtualizado.patronus, registroPost.patronus);




            //GET Id
            response = await client.GetAsync($"{hostApi}character/{registroAtualizado.id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroSelecionado = JsonConvert.DeserializeObject<CharacterResultDto>(jsonResult);
            Assert.NotNull(registroSelecionado);
            Assert.Equal(registroSelecionado.name, registroAtualizado.name);
            Assert.Equal(registroSelecionado.role, registroAtualizado.role);
            Assert.Equal(registroSelecionado.school, registroAtualizado.school);
            Assert.Equal(registroSelecionado.patronus, registroAtualizado.patronus);

            //DELETE
            response = await client.DeleteAsync($"{hostApi}character/{registroSelecionado.id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            

        }

    }
}
