using NetDemo.Models;

namespace NetDemo.Repository
{
    public static class CollegeRepository
    {
        public static List<StudentModel> Students { get; set; } = new List<StudentModel>() {
                new StudentModel
                {
                    Id = 1,
                    StudentName = "Student A",
                    Email = "studenta@gmail.com",
                    Address = "Address A"
                },
                new StudentModel
                {
                    Id = 2,
                    StudentName = "Student B",
                    Email = "studentb@gmail.com",
                    Address = "Address B"
                }
            };
    }
}
