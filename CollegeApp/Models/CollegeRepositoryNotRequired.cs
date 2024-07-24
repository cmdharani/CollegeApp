namespace CollegeApp.Models
{
    public static class CollegeRepositoryNoNeed
    {
        public static List<StudentNoNeed> Students { get; set; } = new List<StudentNoNeed>()
        {
            new() { Id=1, Address="Bangalore", Email="test@123.com",StudentName="dharani", AdmissionDate=DateTime.Now.AddDays(-3),Age=23},
            new (){ Id=2, Address="Delhi", Email="testABC@123.com",StudentName="Madhu" , AdmissionDate = DateTime.Now.AddDays(-3),Age=22}



        };
    }
}
