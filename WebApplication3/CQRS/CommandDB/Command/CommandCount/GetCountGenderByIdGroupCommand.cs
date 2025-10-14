using MyMediator.Interfaces;
using WebApplication3.CQRS.CommandDB.Command.CommandList;
using WebApplication3.CQRS.CommandDB.DTO;
using WebApplication3.DB;

namespace WebApplication3.CQRS.CommandDB.Command.CommandCount
{
    public class GetCountGenderByIdGroupCommand : IRequest<string>
    {
        public required int IdGroup { get; set; }
        public class GetCountGenderByIdGroupCommandHandler : IRequestHandler<GetCountGenderByIdGroupCommand, string>
        {

            private readonly Db131025Context db;
            public GetCountGenderByIdGroupCommandHandler(Db131025Context db)
            {
                this.db = db;
            }

            public async Task<string> HandleAsync(GetCountGenderByIdGroupCommand request, CancellationToken ct = default)
            {
                var Db = db.Students.Where(s => s.IdGroup == request.IdGroup).Select(s => new StudentDTO { Id = s.Id, FirstName = s.FirstName, Gender = s.Gender, LastName = s.LastName, Phone = s.Phone, IdGroup = s.IdGroup }).ToList();
                return $"Мальчики = {Db.Where(s => s.Gender == 1).ToList().Count().ToString()}; Девочки = {Db.Where(s => s.Gender == 0).ToList().Count().ToString()}";
            }
        }
    }
}
