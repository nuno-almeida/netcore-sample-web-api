using System;
using System.ComponentModel.DataAnnotations.Schema;
using Netcore.Sample.Web.Api.Models.DTOs;

namespace Netcore.Sample.Web.Api.Models.Entities
{
    [Table("student")]
    public class Student
    {
        [Column("id")]
        public int? Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("age")]
        public int Age { get; set; }

        [Column("schoolid")]
        public int SchoolId { get; set; }

        [Column("lastupdateat")]
        public long LastUpdateAt { get; set; }


        public void fromDTO(StudentDTO dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            Age = dto.Age;
            SchoolId = dto.SchoolId;
            LastUpdateAt = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        }
    }
}
