using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Commands
{
    public class DeleteStudentByIdCommand : IRequest<int>
    {
        public int Id { get; set; }

        public class DeleteStudentByIdCommandHandler : IRequestHandler<DeleteStudentByIdCommand, int>
        {
            private readonly IAppDbContext _context;
            public DeleteStudentByIdCommandHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(DeleteStudentByIdCommand request, CancellationToken cancellationToken)
            {
                var student = await _context.Students.FindAsync(request.Id);

                if (student == null) return default;

                _context.Students.Remove(student);
                await _context.SaveChangesAsync();

                return student.Id;
            }
        }
    }
}