using Grpc.Core;
using GrpcService.Commands;
using MediatR;

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
            var result = await _mediator.Send(new AddStudentCommand(request));

            return result.Match(
                Some: value => new StudentReply { StudentId = value },
                None: () => new StudentReply { StudentId = "Error: Student ID not available" }
            );
        }
    }
}
