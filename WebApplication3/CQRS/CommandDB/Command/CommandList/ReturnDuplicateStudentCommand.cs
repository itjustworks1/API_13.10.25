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
                var stspisok = db.Students.ToList();
                List<StudentDTO> students = [];
                for (int j = 0; j < stspisok.Count; j++)
                {
                    for (int i = 1+j; i < stspisok.Count; i++)
                    {
                        Student st = stspisok[i];
                        Student student = stspisok[j];
                        if (
                            student.FirstName == st.FirstName &&
                            student.Gender == st.Gender &&
                            student.IdGroup == st.IdGroup &&
                            student.Phone == st.Phone &&
                            student.LastName == st.LastName)
                        {
                            students.Add(new StudentDTO { FirstName = student.FirstName, Gender = student.Gender, LastName = student.LastName, Phone = student.Phone, IdGroup = student.IdGroup });
                        }
                    }
                          
                }
                return students;
    
            }


            
        }
        
    }
    
}

