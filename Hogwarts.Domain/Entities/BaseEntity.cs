using System;
using System.ComponentModel.DataAnnotations;

namespace Hogwarts.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid id { get; set; }
    }
}
