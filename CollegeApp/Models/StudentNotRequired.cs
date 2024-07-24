namespace CollegeApp.Models
{
    public class StudentNoNeed
    {
        public int Id { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime AdmissionDate { get; set; }
        public int Age { get; set; }
    }
}
