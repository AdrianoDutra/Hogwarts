using Hogwarts.Domain.Dtos;
using System;
using System.Collections.Generic;

namespace Hogwarts.Service.Test
{
    public class CharacterDtoTest
    {
        public static Guid idChar { get; set; }
        public static string nameChar { get; set; }
        public static string roleChar { get; set; }
        public static string schoolChar { get; set; }
        public static string houseChar { get; set; }
        public static string patronusChar { get; set; }

        public static string nameAlteradoChar { get; set; }
        public static string roleAlteradoChar { get; set; }
        public static string schoolAlteradoChar { get; set; }
        public static string houseAlteradoChar { get; set; }
        public static string patronusAlteradoChar { get; set; }

        public List<CharacterResultDto> listResult = new List<CharacterResultDto>();
        public CharacterInsertDto characterInsertDto;
        public CharacterResultDto characterResultDto;
        public CharacterUpdateDto characterUpdateDto;

        public CharacterDtoTest()
        {
            idChar = Guid.NewGuid();
            nameChar = Faker.Name.FullName();
            roleChar = Faker.Name.FullName();
            schoolChar = Faker.Name.FullName();
            houseChar = Guid.NewGuid().ToString();
            patronusChar = Faker.Name.Last();

            nameAlteradoChar = Faker.Name.FullName();
            roleAlteradoChar = Faker.Name.FullName();
            schoolAlteradoChar = Faker.Name.FullName();
            houseAlteradoChar = Guid.NewGuid().ToString();
            patronusAlteradoChar = Faker.Name.Last();

            for (int i = 10 - 1; i >= 0; i--)
            {
                var characterResultDto = new CharacterResultDto
                {
                    id = Guid.NewGuid(),
                    name = Faker.Name.FullName(),
                    role = Faker.Name.FullName(),
                    school = Faker.Name.FullName(),
                    house = Guid.NewGuid().ToString(),
                    patronus = Faker.Name.Last()
                };
                listResult.Add(characterResultDto);
            }
            characterInsertDto = new CharacterInsertDto
            {
                house = houseChar,
                name = nameChar,
                patronus = patronusChar,
                role = roleChar,
                school = schoolChar
            };
            characterResultDto = new CharacterResultDto()
            {
                id = idChar,
                house = houseChar,
                name = nameChar,
                patronus = patronusChar,
                role = roleChar,
                school = schoolChar
            };
            characterUpdateDto = new CharacterUpdateDto()
            {
                id = idChar,
                house = houseAlteradoChar,
                name = nameAlteradoChar,
                patronus = patronusAlteradoChar,
                role = roleAlteradoChar,
                school = schoolAlteradoChar
            };

        }
    }
}
