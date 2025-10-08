using ASPNETCoreWebAPICRUD.DTO.Requests;
using ASPNETCoreWebAPICRUD.DTO.Response;

namespace ASPNETCoreWebAPICRUD.Services.Interfaces
{
    public interface ICourseService
    {
        Task<AddCourseResponse> AddCourseAsync(AddCourseRequest addCourseRequest);
    }
}
