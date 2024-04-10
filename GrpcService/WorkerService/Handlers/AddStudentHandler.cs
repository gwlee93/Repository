using WorkerService.Commands;
using LanguageExt;
using MediatR;
using EntityStudent = Domain.Entities.Student;
using DtoStudent = Api.Students.Student;
using Application;

namespace WorkerService.Handlers
{
    public class AddStudentHandler : IRequestHandler<AddStudentCommand, Option<DtoStudent>>
    {

        private readonly IMapper _mapper;
        private readonly IQueue _queue;

        public AddStudentHandler(IMapper mapper, IQueue queue)
        {
            _mapper = mapper;
            _queue = queue;
        }

        public async Task<Option<DtoStudent>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {            
            var entity = new EntityStudent(studentId : request.Student.StudentId, 
                                          name : request.Student.Name,
                                           age : request.Student.Age,
                                        course : request.Student.Course);            

            await _queue.EnqueueAsync(entity.StudentId);

            var dtoStudent = _mapper.Map<DtoStudent>(entity);

            return Option<DtoStudent>.Some(dtoStudent);

        }     
    }
}
