namespace Netcore.Sample.Web.Api.Models
{
    public static class Constants
    {
        public static class Audit
        {
            public static class Operations
            {
                public const string ReadAll = "ReadAll";
                public const string Read = "Read";
                public const string Create = "Create";
                public const string Update = "Update";
                public const string Delete = "Delete";
            }

            public static class Entities
            {
                public const string Student = "Student";
                public static readonly string School = "School";
            }
        }
    }
}
