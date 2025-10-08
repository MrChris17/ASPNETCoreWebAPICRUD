using ASPNETCoreWebAPICRUD.Data;
using ASPNETCoreWebAPICRUD.DTO.Requests;
using ASPNETCoreWebAPICRUD.DTO.Response;
using ASPNETCoreWebAPICRUD.Entities;
using ASPNETCoreWebAPICRUD.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCoreWebAPICRUD.Services
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CourseService> _logger;

        public CourseService(ApplicationDbContext context, ILogger<CourseService> logger) { 
            _context = context;
            _logger = logger;
        }

        public async Task<AddCourseResponse> AddCourseAsync(AddCourseRequest addCourseRequest)
        {
            // Check if course is already exists
            var courseExists = await _context.Course.AsNoTracking().AnyAsync(course => course.Name.Equals(addCourseRequest.CourseName));
            
            if (courseExists)
            {
                _logger.LogWarning("{CourseName} course is already exists.", addCourseRequest.CourseName);

                return new AddCourseResponse
                {
                    IsSuccess = false,
                    CourseName = addCourseRequest.CourseName
                };
            }
            
            // Create new course if it doesn't exist
            var course = new Course { Name = addCourseRequest.CourseName };

            await _context.Course.AddAsync(course);
            await _context.SaveChangesAsync();

            _logger.LogInformation("{CourseName} course created successfully", addCourseRequest.CourseName);

            return new AddCourseResponse
            {
                CourseId = course.Id,
                CourseName = course.Name
            };
        }
    }
}
