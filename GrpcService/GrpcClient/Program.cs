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
                    StudentId = "1",
                    Age = 12,
                    Course = "Math",
                    Name = "이건우"
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
