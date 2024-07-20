namespace CollegeApp.Models
{
    public static class CollegeRepository
    {
        public static List<Student> Students { get; set; } = new List<Student>()
        {
            new() { Id=1, Address="Bangalore", Email="test@123.com",StudentName="dharani" },
            new (){ Id=2, Address="Delhi", Email="testABC@123.com",StudentName="Madhu" }



        };
    }
}
