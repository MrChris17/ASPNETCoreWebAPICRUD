using ASPNETCoreWebAPICRUD.Entities;

namespace ASPNETCoreWebAPICRUD.DTO.Response
{
    public class AddCourseResponse
    {
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
        public bool IsSuccess { get; set; } = true;
    }
}
