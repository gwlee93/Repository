using Grpc.Core;
using MediatR;
using Api.Students;
using DtoStudent = Api.Students.Student;
using WorkerService.Core.Application.Features.Commands;
namespace WorkerService.Controller
{
    public class StudentController: StudentsGrpc.StudentsGrpcBase
    {
        private readonly IMediator _mediator;
        public StudentController(IMediator mediator)
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
