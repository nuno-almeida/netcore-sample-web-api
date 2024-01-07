using System;
using System.Linq;
using System.Linq.Expressions;
using Netcore.Sample.Web.Api.Models.Entities;
using Newtonsoft.Json;

namespace Netcore.Sample.Web.Api.Models.DTOs
{
    public class GetStudentsQueryDTO
    {
        [JsonProperty(PropertyName = "orderBy")]
        public string OrderBy { get; set; } = "id";

        [JsonProperty(PropertyName = "orderAs")]
        public string OrderAs { get; set; } = "asc";

        [JsonProperty(PropertyName = "page")]
        public int Page { get; set; } = 1;

        [JsonProperty(PropertyName = "pageSize")]
        public int PageSize { get; set; } = 10;

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "age")]
        public int? Age { get; set; }


        public IQueryable<Student> GetQuery(IQueryable<Student> query)
        {
            query = ApplySorting(query);
            query = ApplyFiltering(query);

            return query;
        }

        private IQueryable<Student> ApplySorting(IQueryable<Student> query)
        {
            var sortProperty = GetSortProperty();

            if ((OrderAs ?? "").ToLower() == "desc")
                query = query.OrderByDescending(sortProperty);
            else
                query = query.OrderBy(sortProperty);

            return query;
        }

        private IQueryable<Student> ApplyFiltering(IQueryable<Student> query)
        {
            if (!string.IsNullOrEmpty(Name))
                query = query
                    .Where(student =>
                        !string.IsNullOrEmpty(student.Name) &&
                        student.Name.ToLower().Contains(Name.ToLower())
                        );

            if (Age != null)
                query = query.Where(student => student.Age == Age);

            return query;
        }

        private Expression<Func<Student, object>> GetSortProperty()
        {
            Expression<Func<Student, object>> expression;

            var orderBy = (OrderBy ?? "").ToLower();

            if (orderBy == "name")
                expression = student => student.Name;
            else if (orderBy == "age")
                expression = student => student.Age;
            else
                expression = student => student.Id;

            return expression;
        }
    }
}
