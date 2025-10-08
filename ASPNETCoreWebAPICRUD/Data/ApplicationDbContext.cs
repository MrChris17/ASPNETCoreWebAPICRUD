using Microsoft.EntityFrameworkCore;

namespace ASPNETCoreWebAPICRUD.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
