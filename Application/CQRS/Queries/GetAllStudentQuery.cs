using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries
{
    public class GetAllStudentQuery : IRequest<IEnumerable<Student>>
    {
        public class GetAllStudentQueryHandler : IRequestHandler<GetAllStudentQuery, IEnumerable<Student>>
        {
            private readonly IAppDbContext _context;

            public GetAllStudentQueryHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Student>> Handle(GetAllStudentQuery request, CancellationToken cancellationToken)
            {
                var studentList = await _context.Students.ToListAsync();
                if (studentList == default) return null;

                return studentList.AsReadOnly();
            }
        }
    }
}