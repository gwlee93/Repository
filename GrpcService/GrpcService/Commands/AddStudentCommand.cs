using LanguageExt;
using MediatR;

namespace GrpcService.Commands
{
    public record AddStudentCommand(StudentRequest request) : IRequest<Option<string>>;
}
