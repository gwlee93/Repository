using LanguageExt;
using MediatR;
using EntityStudent = Domain.Entities.Student;
using DtoStudent = Api.Students.Student;
using Application;
using WorkerService.Core.Application.Features.Commands;
using Application.Mapper;
using Application.Persistences;

namespace WorkerService.Core.Application.Features.Handlers
{
    public class AddStudentHandler : IRequestHandler<AddStudentCommand, Option<DtoStudent>>
    {

        private readonly IMapper _mapper;
        private readonly IQueue _queue;
        private readonly IStudentRepository _repository;
        public AddStudentHandler(IMapper mapper, IQueue queue, IStudentRepository repository)
        {
            _mapper = mapper;
            _queue = queue;
            _repository = repository;
        }

        public async Task<Option<DtoStudent>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var entity = new EntityStudent(studentId: request.Student.StudentId,
                                          name: request.Student.Name,
                                           age: request.Student.Age,
                                        course: request.Student.Course);

            await _queue.EnqueueAsync(entity.StudentId);

            await _repository.CreateAsync(entity);

            var dtoStudent = _mapper.Map<DtoStudent>(entity);

            return Option<DtoStudent>.Some(dtoStudent);

        }
    }
}
