namespace WorkerService.Entities
{
    public class Student
    {
        public string StudentId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public int Age { get; set; } = default!;
        public string Course { get; set; } = default!;
        public Student(string studentId, string name, int age, string course)
        {
            if (string.IsNullOrEmpty(studentId)) throw new Exception($"{nameof(studentId)} is empty.");

            this.StudentId = studentId;
            this.Name = name;
            this.Age = age;
            this.Course = course;
        }        
    }
}
