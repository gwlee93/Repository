using LanguageExt;
using MediatR;
using DtoStudent = Api.Students.Student;
namespace WorkerService.Core.Application.Features.Commands
{
    public record AddStudentCommand : IRequest<Option<DtoStudent>>
    {
        public DtoStudent Student { get; }
        public AddStudentCommand(DtoStudent student) => Student = student;
    }
}

