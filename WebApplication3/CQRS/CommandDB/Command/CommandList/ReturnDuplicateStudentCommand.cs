using Microsoft.EntityFrameworkCore;
using MyMediator.Interfaces;
using System.Collections.Generic;
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
                var Db = db.Students.ToList();
                List<StudentDTO> students = [] ;
                for (int i = 0; i < Db.Count; i++)
                {
                    for (int j = i+1; j < Db.Count; j++)
                    {
                        Student studenti = Db[i];
                        Student studentj = Db[j];
                        if (studentj.FirstName == studenti.FirstName &&
                            studentj.Gender == studenti.Gender &&
                            studentj.LastName == studenti.LastName &&
                            studentj.Phone == studenti.Phone)
                            students.Add(new StudentDTO { FirstName = studenti.FirstName, LastName = studenti.LastName, Gender = studenti.Gender, IdGroup = studenti.IdGroup, Phone = studenti.Phone});
                    }
                }
                return students;
            }
        }
    }
}
