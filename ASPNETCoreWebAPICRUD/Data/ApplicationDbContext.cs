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

            // Subject Entity
            modelBuilder.Entity<Subject>(entity =>
            {
                // Set table name to tbl_subject for Subject entity
                entity.ToTable("tbl_subject")
                    .HasKey(subject => subject.Id); // Set Id column as a primary key

                // Id column property
                entity.Property(subject => subject.Id)
                    .HasColumnName("id")
                    .HasColumnType("INT")
                    .UseIdentityAlwaysColumn()
                    .HasColumnOrder(0);

                // Name column property
                entity.Property(subject => subject.Name)
                    .HasColumnName("name")
                    .HasColumnType("VARCHAR(50)")
                    .HasColumnOrder(1)
                    .IsRequired();

                // Description column property
                entity.Property(subject => subject.Description)
                    .HasColumnName("description")
                    .HasColumnType("TEXT")
                    .HasColumnOrder(2);

                // CourseId column property
                entity.Property(subject => subject.CourseId)
                    .HasColumnName("course_id")
                    .HasColumnType("INT")
                    .HasColumnOrder(3)
                    .IsRequired();

                // Student and Subject entity relationship with StudentSubject as join table
                entity
                    .HasMany(subject => subject.Students)
                    .WithMany(student => student.Subjects)
                    .UsingEntity<StudentSubject>(
                        join => join.HasOne<Student>().WithMany().HasForeignKey(student => student.StudentId).IsRequired(),
                        join => join.HasOne<Subject>().WithMany().HasForeignKey(subject => subject.SubjectId).IsRequired());
            });
        }
    }
}
