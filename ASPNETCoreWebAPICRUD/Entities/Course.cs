namespace ASPNETCoreWebAPICRUD.Entities
{
    public class Course : BaseEntity
    {
        public ICollection<Subject> Subjects { get; } = new List<Subject>();
    }
}
