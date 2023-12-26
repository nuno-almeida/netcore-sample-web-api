using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Netcore.Sample.Web.Api.Models.Entities;
using Newtonsoft.Json;

namespace Netcore.Sample.Web.Api.Models.DTOs
{
    public class StudentDTO
    {
        [JsonProperty(PropertyName = "id")]
        [DisplayName("id")]
        public int Id { get; set; }

        [StringLength(maximumLength: 50, MinimumLength = 10)]
        [JsonProperty(PropertyName = "name")]
        [DisplayName("name")]
        public string Name { get; set; }

        [Range(6, 100)]
        [JsonProperty(PropertyName = "age")]
        [DisplayName("age")]
        public int Age { get; set; }

        [Range(1, int.MaxValue)]
        [JsonProperty(PropertyName = "schoolId")]
        [DisplayName("schoolId")]
        public int SchoolId { get; set; }


        public static StudentDTO fromEntity(Student student)
        {
            return new StudentDTO
            {
                Id = student.Id,
                Name = student.Name,
                Age = student.Age,
                SchoolId = student.SchoolId
            };

        }
    }
}
