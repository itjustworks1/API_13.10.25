using Microsoft.EntityFrameworkCore;
using MyMediator.Interfaces;
using WebApplication3.CQRS.CommandDB.DTO;
using WebApplication3.DB;

namespace WebApplication3.CQRS.CommandDB.Command.CommandList
{
    public class ReturnDuplicateStudentCommand : IRequest<IEnumerable<StudentDTO>>
    {
        public class ReturnDuplicateStudentCommandHandler : IRequestHandler<ReturnDuplicateStudentCommand, IEnumerable<StudentDTO>>
        {

            private readonly Db131025Context db;
            public ReturnDuplicateStudentCommandHandler(Db131025Context db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<StudentDTO>> HandleAsync(ReturnDuplicateStudentCommand request, CancellationToken ct = default)
            {
                return await db.Students.Where(s => s.IdGroup == null).Select(s => new StudentDTO { Id = s.Id, FirstName = s.FirstName, Gender = s.Gender, LastName = s.LastName, Phone = s.Phone, IdGroup = s.IdGroup }).ToListAsync();
            }
        }
    }
}
