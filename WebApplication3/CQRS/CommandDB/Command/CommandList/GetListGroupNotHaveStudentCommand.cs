using Microsoft.EntityFrameworkCore;
using MyMediator.Interfaces;
using WebApplication3.CQRS.CommandDB.DTO;
using WebApplication3.DB;

namespace WebApplication3.CQRS.CommandDB.Command.CommandList
{
    public class GetListGroupNotHaveStudentCommand : IRequest<IEnumerable<GroupDTO>>
    {
        public class GetListGroupNotHaveStudentCommandHandler : IRequestHandler<GetListGroupNotHaveStudentCommand, IEnumerable<GroupDTO>>
        {

            private readonly Db131025Context db;
            public GetListGroupNotHaveStudentCommandHandler(Db131025Context db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<GroupDTO>> HandleAsync(GetListGroupNotHaveStudentCommand request, CancellationToken ct = default)
            {
                var dbStudent = db.Students.ToList();
                int?[] massiv = new int?[dbStudent.Count];
                for(int i = 0; i < dbStudent.Count;i++)
                {
                    massiv[i] = dbStudent[i].IdGroup;
                }
                return await db.Groups.Where(s => !massiv.Contains(s.Id)).Select(s => new GroupDTO { Id = s.Id, Title = s.Title, IdSpecial = s.IdSpecial }).ToListAsync();
            }
        }
    }
}
