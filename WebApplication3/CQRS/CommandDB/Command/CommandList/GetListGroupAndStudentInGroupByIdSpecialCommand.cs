using Microsoft.EntityFrameworkCore;
using MyMediator.Interfaces;
using WebApplication3.CQRS.CommandDB.Command.CommandList;
using WebApplication3.CQRS.CommandDB.DTO;
using WebApplication3.DB;

namespace WebApplication3.CQRS.CommandDB.Command
{
    public class GetListGroupAndStudentInGroupByIdSpecialCommand : IRequest<IEnumerable<GroupDTO>>
    {
        public required int IdSpecial { get; set; }
        public class GetListGroupAndStudentInGroupByIdSpecialCommandHandler : IRequestHandler<GetListGroupAndStudentInGroupByIdSpecialCommand, IEnumerable<GroupDTO>>
        {

            private readonly Db131025Context db;
            public GetListGroupAndStudentInGroupByIdSpecialCommandHandler(Db131025Context db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<GroupDTO>> HandleAsync(GetListGroupAndStudentInGroupByIdSpecialCommand request, CancellationToken ct = default)
            {
                return db.Groups.Where(s => s.IdSpecial == request.IdSpecial).Include(s => s.Students).Select(s => new GroupDTO { Id = s.Id, Title = s.Title, IdSpecial = s.IdSpecial, CountStudent = s.Students.Count });
            }
        }
    }
}
