using WorkerService.Commands;
using WorkerService.Services;
using LanguageExt;
using MediatR;
using EntityStudent = Domain.Entities.Student;
using DtoStudent = Api.Students.Student;
using Application.Mappers;

namespace WorkerService.Handlers
{
    public class AddStudentHandler : IRequestHandler<AddStudentCommand, Option<DtoStudent>>
    {

        private readonly IMapper _mapper;
        private readonly RedisService _redisService;

        public AddStudentHandler(IMapper mapper, RedisService redisService)
        {
            _mapper = mapper;
            _redisService = redisService;
        }

        public async Task<Option<DtoStudent>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {            
            var entity = new EntityStudent(studentId : request.Student.StudentId, 
                                          name : request.Student.Name,
                                           age : request.Student.Age,
                                        course : request.Student.Course);            

            await _redisService.EnqueueAsync(entity);

            var dtoStudent = _mapper.Map<DtoStudent>(entity);

            return Option<DtoStudent>.Some(dtoStudent);

        }     
    }
}
