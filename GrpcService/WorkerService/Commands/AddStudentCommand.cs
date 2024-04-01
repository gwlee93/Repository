using GrpcService;
using LanguageExt;
using MediatR;

namespace WorkerService.Commands
{
    public record AddStudentCommand : IRequest<Option<StudentDTO>>
    {
        public StudentDTO Student { get; }
        public AddStudentCommand(StudentDTO student) => Student = student;
    }
}

