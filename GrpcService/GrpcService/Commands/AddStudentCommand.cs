using DtoStudent = Api.Students.Student;
using LanguageExt;
using MediatR;

namespace GrpcService.Commands
{
    public record AddStudentCommand : IRequest<Option<DtoStudent>>
    {
        public DtoStudent Student { get; }
        public AddStudentCommand(DtoStudent student) => Student = student;
    }
}

