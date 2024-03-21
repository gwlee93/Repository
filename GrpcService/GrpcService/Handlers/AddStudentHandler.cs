using GrpcService.Commands;
using GrpcService.Entities;
using GrpcService.Services;
using LanguageExt;
using MediatR;
using StudentDTO = GrpcService.StudentDTO;

namespace GrpcService.Handlers
{
    public class AddStudentHandler : IRequestHandler<AddStudentCommand, Option<StudentDTO>>
    {

        private readonly RedisService _redisService;
        public AddStudentHandler(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<Option<StudentDTO>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {            
            var entity = new Student(studentId : request.Student.StudentId, 
                                          name : request.Student.Name,
                                           age : request.Student.Age,
                                        course : request.Student.Course);            
            await _redisService.EnqueueAsync(entity);
            
            if (entity is null)
                return Option<StudentDTO>.None;
           
            return Option<StudentDTO>.Some(new StudentDTO { StudentId = entity.StudentId,
                                                            Name = entity.Name,
                                                            Age = entity.Age,
                                                            Course = entity.Course});

        }     
    }
}
