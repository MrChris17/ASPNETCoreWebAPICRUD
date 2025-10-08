namespace ASPNETCoreWebAPICRUD.Entities
{
    public class Student : BaseEntity
    {
        public string Address { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

        public ICollection<Subject> Subjects { get; } = new List<Subject>();
    }
}
