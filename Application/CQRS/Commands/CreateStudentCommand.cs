using Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Commands
{
    public class CreateStudentCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Standard { get; set; }
        public int Rank { get; set; }
    }

    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, int>
    {
        private readonly IAppDbContext _context;

        public CreateStudentCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            Student student = new();

            student.Name = request.Name;
            student.Rank = request.Rank;
            student.Standard = request.Standard;

            _context.Students.Add(student);

            await _context.SaveChangesAsync();

            return student.Id;
        }
    }
}