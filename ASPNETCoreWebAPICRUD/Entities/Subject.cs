namespace ASPNETCoreWebAPICRUD.Entities
{
    public class Subject : BaseEntity
    {
        public string? Description { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public ICollection<Student> Students { get; } = new List<Student>();
    }
}
