using GrpcService.Commands;
using GrpcService.Entities;
using GrpcService.Services;
using LanguageExt;
using MediatR;
using StudentDTO = GrpcService.StudentDTO;

namespace GrpcService.Handlers
{
    public class AddStudentHandler : IRequestHandler<AddStudentCommand, Option<string>>
    {

        private readonly RedisService _redisService;
        public AddStudentHandler(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<Option<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var entity = Student.Create("1", "이건우", 32, "Math");            
            await _redisService.EnqueueAsync(entity);
            var result = await _redisService.PopAsync();            
            if (result is null)
                return Option<string>.None;
           
            return Option<string>.Some(result);

        }     
    }
}
