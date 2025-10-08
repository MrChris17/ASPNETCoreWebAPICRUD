using ASPNETCoreWebAPICRUD.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCoreWebAPICRUD.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Course> Course { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Course Entity
            modelBuilder.Entity<Course>(entity =>
            {
                // Set table name to tbl_course for Course entity
                entity.ToTable("tbl_course")
                    .HasKey(course => course.Id); // Set Id column as a primary key

                // Id column property
                entity.Property(course => course.Id)
                    .HasColumnName("id")
                    .HasColumnType("INT")
                    .UseIdentityAlwaysColumn()
                    .HasColumnOrder(0);

                // Name column property
                entity.Property(course => course.Name)
                    .HasColumnName("name")
                    .HasColumnType("VARCHAR(6)")
                    .HasColumnOrder(1);

                // Course and Subject entity relationship
                entity
                    .HasMany(course => course.Subjects)
                    .WithOne(subject => subject.Course)
                    .HasForeignKey(subject => subject.CourseId)
                    .IsRequired();
            });
        }
    }
}
