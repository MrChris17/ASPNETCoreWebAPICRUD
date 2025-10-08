using ASPNETCoreWebAPICRUD.DTO.Requests;
using ASPNETCoreWebAPICRUD.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreWebAPICRUD.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly ILogger<CoursesController> _logger;

        public CoursesController(ICourseService courseService, ILogger<CoursesController> logger)
        {
            _courseService = courseService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllCourses()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> CreateCourse([FromBody] AddCourseRequest addCourseRequest)
        {
            try
            {
                var courseResult = await _courseService.AddCourseAsync(addCourseRequest);

                if(!courseResult.IsSuccess)
                {
                    _logger.LogWarning("Course creation failed with status: {Status}", courseResult.IsSuccess);

                    return BadRequest(new { message = $"{courseResult.CourseName} course is already exists." });
                }

                _logger.LogInformation("Course created successfully with ID: {CourseID}", courseResult.CourseId);

                return CreatedAtAction(nameof(GetAllCourses), new { Id = courseResult.CourseId }, courseResult);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating course {CourseName}", addCourseRequest.CourseName);

                return StatusCode(500, new { message = "An error occurred while processing your request" });
            }
        }
    }
}
