using Api.Students;
using Grpc.Net.Client;
using DtoStudent = Api.Students.Student;
namespace GrpcClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {            
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new StudentsGrpc.StudentsGrpcClient(channel);            
    
            var studentReply = await client.AddStudentInfoAsync(new StudentRequest
            {
                Student = new DtoStudent
                {
                    StudentId = "4",
                    Age = 26,
                    Course = "PHP",
                    Name = "이수현"
                }
            });

            Console.WriteLine($"StudentId: {studentReply.Student.StudentId}, " +
                $"Name: {studentReply.Student.Name}, " +
                $"Age: {studentReply.Student.Age}," +
                $" Course: {studentReply.Student.Course}");
            Console.ReadKey();
        }
    }
}
