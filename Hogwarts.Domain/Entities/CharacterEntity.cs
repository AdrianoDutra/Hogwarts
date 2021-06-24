using System;
using System.ComponentModel.DataAnnotations;


namespace Hogwarts.Domain.Entities
{
    public class CharacterEntity : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string name { get; set; }

        [Required]
        [MaxLength(100)]
        public string role { get; set; }

        [Required]
        [MaxLength(100)]
        public string school { get; set; }
        
        [Required]
        [MaxLength(36)]
        public string house { get; set; }

        [Required]
        [MaxLength(100)]
        public string patronus { get; set; }
    }
}
