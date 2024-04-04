using Grpc.Core;
using WorkerService.Commands;
using MediatR;
using Api.Students;
using DtoStudent = Api.Students.Student;
namespace GrpcService.Services
{
    public class StudentService : StudentsGrpc.StudentsGrpcBase
    {        
        private readonly IMediator _mediator;
        public StudentService(IMediator mediator)
        {
            _mediator = mediator;
        }


        public override async Task<StudentReply> AddStudentInfo(StudentRequest request, ServerCallContext context)
        {
            return await _mediator.Send(new AddStudentCommand(request.Student))
                                  .Match(Some: value => new StudentReply { Student = value },
                                         None: () => new StudentReply { Student = new DtoStudent() });
        }
    }
}
