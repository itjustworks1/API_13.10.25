using Microsoft.EntityFrameworkCore;
using MyMediator.Interfaces;
using MyMediator.Types;
using WebApplication3.CQRS.CommandDB.DTO;
using WebApplication3.DB;

namespace WebApplication3.CQRS.CommandDB.Command.CommandList
{
    public class TransferStudentToGroupCommand : IRequest
    {
        public required int IdStudent { get; set; }
        public required int IdGroup { get; set; }
        public class TransferStudentToGroupCommandHandler : IRequestHandler<TransferStudentToGroupCommand, Unit>
        {

            private readonly Db131025Context db;
            public TransferStudentToGroupCommandHandler(Db131025Context db)
            {
                this.db = db;
            }

            public async Task<Unit> HandleAsync(TransferStudentToGroupCommand request, CancellationToken ct = default)
            {
                var student = db.Students.FirstOrDefault(s => s.Id == request.IdStudent);
                student.Id = request.IdGroup;

                await db.Students.Where(s => s.IdGroup == null).Select(s => new StudentDTO { Id = s.Id, FirstName = s.FirstName, Gender = s.Gender, LastName = s.LastName, Phone = s.Phone, IdGroup = s.IdGroup }).ToListAsync();
                return Unit.Value;
            }
        }
    }
}
