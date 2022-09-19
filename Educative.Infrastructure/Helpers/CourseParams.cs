namespace Educative.Infrastructure.Helpers
{
    public class CourseParams : PaginationParams
    {
        public string OrderBy { get; set; }
        public string SearchTerm { get; set; }
        public string Name { get; set; }
        public string Topic { get; set; }

    }
}