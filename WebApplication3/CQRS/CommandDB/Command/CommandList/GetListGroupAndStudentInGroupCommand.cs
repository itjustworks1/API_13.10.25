using Microsoft.EntityFrameworkCore;
using MyMediator.Interfaces;
using WebApplication3.CQRS.CommandDB.DTO;
using WebApplication3.DB;

namespace WebApplication3.CQRS.CommandDB.Command.CommandList
{
    public class GetListGroupAndStudentInGroupCommand : IRequest<IEnumerable<GroupDTO>>
    {
        public class GetListGroupAndStudentInGroupCommandHandler : IRequestHandler<GetListGroupAndStudentInGroupCommand, IEnumerable<GroupDTO>>
        {

            private readonly Db131025Context db;
            public GetListGroupAndStudentInGroupCommandHandler(Db131025Context db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<GroupDTO>> HandleAsync(GetListGroupAndStudentInGroupCommand request, CancellationToken ct = default)
            {
                return db.Groups.Include(s => s.Students).Select(s => new GroupDTO { Id = s.Id, Title = s.Title, IdSpecial = s.IdSpecial, CountStudent = s.Students.Count });
            }
        }
    }
}
