using ASPNETCoreWebAPICRUD.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCoreWebAPICRUD.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Course> Course { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<Student> Student { get; set; }

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
                    .HasColumnOrder(1)
                    .IsRequired();

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

            // Student Entity
            modelBuilder.Entity<Student>(entity =>
            {
                // Set table name to tbl_student for Student entity
                entity.ToTable("tbl_student")
                    .HasKey(student => student.Id); // Set Id column as a primary key

                // Id column property
                entity.Property(student => student.Id)
                    .HasColumnName("id")
                    .HasColumnType("INT")
                    .UseIdentityAlwaysColumn()
                    .HasColumnOrder(0);

                // Name column property
                entity.Property(student => student.Name)
                    .HasColumnName("name")
                    .HasColumnType("VARCHAR(70)")
                    .HasColumnOrder(1)
                    .IsRequired();

                // Address column property
                entity.Property(student => student.Address)
                    .HasColumnName("address")
                    .HasColumnType("VARCHAR(250)")
                    .HasColumnOrder(2)
                    .IsRequired();

                // Email Address column property
                entity.Property(student => student.EmailAddress)
                    .HasColumnName("email_address")
                    .HasColumnType("VARCHAR(100)")
                    .HasColumnOrder(3)
                    .IsRequired();

                // Phone Number column property
                entity.Property(student => student.PhoneNumber)
                    .HasColumnName("phone_number")
                    .HasColumnType("VARCHAR(11)")
                    .HasColumnOrder(4)
                    .IsRequired();
            });

            // Student Subject Entity (Join Table)
            modelBuilder.Entity<StudentSubject>(entity =>
            {
                // Set table name to tbl_student_subject for Student Subject entity
                entity.ToTable("tbl_student_subject");

                // Student Id column property
                entity.Property(student_subject => student_subject.StudentId)
                    .HasColumnName("student_id")
                    .HasColumnType("INT")
                    .IsRequired();

                // Subject Id column property
                entity.Property(student_subject => student_subject.SubjectId)
                    .HasColumnName("subject_id")
                    .HasColumnType("INT")
                    .IsRequired();
            });
        }
    }
}
