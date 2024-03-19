using Grpc.Net.Client;
using GrpcService;

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
                Student = new StudentDTO
                {
                    StudentId = "1",
                    Age = 12,
                    Course = "Math",
                    Name = "이건우"
                }
            });

            Console.WriteLine($"StudentId: {studentReply.StudentId}");                            
            Console.ReadKey();
        }
    }
}
