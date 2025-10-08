using System.ComponentModel.DataAnnotations;

namespace ASPNETCoreWebAPICRUD.DTO.Requests
{
    public class AddCourseRequest
    {
        [Required(ErrorMessage = "Course name is required.")]
        [StringLength(6, MinimumLength = 4, ErrorMessage = "Course name should be between 4 to 6 characters.")]
        [RegularExpression(@"^[A-Z\s]*$", ErrorMessage = "Only capitalized alphabetic characters are allowed.")]
        public string CourseName { get; set; } = null!;
    }
}
