using LanguageExt;
using MediatR;

namespace GrpcService.Commands
{
    public record AddStudentCommand : IRequest<Option<StudentDTO>>
    {
        public StudentDTO Student { get; }
        public AddStudentCommand(StudentDTO student) => Student = student;
    }
}

