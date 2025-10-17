using System.Collections.Generic;
using System.Net.Http.Json;
using WebApplication3.CQRS.CommandDB.DTO;

namespace ConsoleApp1
{
    internal class Program
    {
        static HttpClient client = new HttpClient();

        static async Task Task1()
        {
            Console.WriteLine("Введите N");
            int.TryParse(Console.ReadLine(), out int N);
            int[] A = new int[N];
            for (int i = 0; i < N; i++)
                int.TryParse(Console.ReadLine(), out A[i]);
            Console.WriteLine("Введите K");
            int.TryParse(Console.ReadLine(), out int K);

            var result = await client.PostAsJsonAsync($"Array/Task1/{K}", A);
            Console.WriteLine(await result.Content.ReadFromJsonAsync<int>());
        }

        static async Task GetListStudentByIdGroupCommand()
        {
            Console.WriteLine("Введите Id группы");
            int.TryParse(Console.ReadLine(), out int idGroup);

            var result = await client.PostAsync($"DB/GetListStudentByIdGroup?idGroup="+idGroup, null);
            var content = await result.Content.ReadFromJsonAsync<IEnumerable<StudentDTO>>();
            foreach(StudentDTO student in content)
                Console.WriteLine(student.LastName);
        }

        static async Task GetCountGenderByIdGroup()
        {
            Console.WriteLine("Введите Id группы");
            int.TryParse(Console.ReadLine(), out int idGroup);

            var result = await client.PostAsync($"DB/GetCountGenderByIdGroup?idGroup=" +idGroup, null);
            var content = await result.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }

        static async Task GetListStudentNotInGroup()
        {
            var result = await client.PostAsync($"DB/GetListStudentNotInGroup", null);
            var content = await result.Content.ReadFromJsonAsync<IEnumerable<StudentDTO>>();
            foreach (StudentDTO student in content)
                Console.WriteLine(student.LastName);
        }
        static async Task GetListGroupNotHaveStudent()
        {
            var result = await client.PostAsync($"DB/GetListGroupNotHaveStudent", null);
            var content = await result.Content.ReadFromJsonAsync<IEnumerable<GroupDTO>>();
            foreach (GroupDTO group in content)
                Console.WriteLine(group.Title);
        }
        static async Task GetListGroupAndStudentInGroup()
        {
            var result = await client.PostAsync($"DB/GetListGroupAndStudentInGroup", null);
            var content = await result.Content.ReadFromJsonAsync<IEnumerable<GroupDTO>>();
            foreach (GroupDTO group in content)
                Console.WriteLine(group.Title + "\t" + group.CountStudent);
        }
        static async Task GetListGroupAndStudentInGroupByIdSpecial()
        {
            Console.WriteLine("Введите Id специальности");
            int.TryParse(Console.ReadLine(), out int idSpecial);

            var result = await client.PostAsync($"DB/GetListGroupAndStudentInGroupByIdSpecial?idSpecial="+idSpecial, null);
            var content = await result.Content.ReadFromJsonAsync<IEnumerable<GroupDTO>>();
            foreach (GroupDTO group in content)
                Console.WriteLine(group.Title + "\t" + group.CountStudent);
        }
        static async Task AddGroupInSpecial()
        {
            Console.WriteLine("Введите Id специальности");
            int.TryParse(Console.ReadLine(), out int idSpecial);
            Console.WriteLine("Введите название группы");
            string title = Console.ReadLine();

            var result = await client.PostAsync($"DB/AddGroupInSpecial?idSpecial="+idSpecial+"&title="+title, null);
        }

        static async Task TransferStudentToGroupCommand()
        {
            Console.WriteLine("Введите Id группы");
            int.TryParse(Console.ReadLine(), out int idGroup);
            Console.WriteLine("Введите Id студента");
            int.TryParse(Console.ReadLine(), out int idStudent);

            var result = await client.PostAsync($"DB/TransferStudentToGroup?idGroup=" + idGroup + "&idStudent=" + idStudent, null);
        }

        static async Task ReturnDuplicateStudent()
        {
            var result = await client.PostAsync($"DB/ReturnDuplicateStudent", null);
            var content = await result.Content.ReadFromJsonAsync<IEnumerable<StudentDTO>>();
            foreach (StudentDTO student in content)
                Console.WriteLine(student.FirstName + " " + student.LastName + " " + student.Phone + " " + student.IdGroup);
        }


        static async Task Main(string[] args)
        {
            client.BaseAddress = new Uri("http://localhost:5205/api/");
            do
            {
                Console.WriteLine("Введите задачу");
                int.TryParse(Console.ReadLine(), out int t);
                switch (t)
                {
                    //case 1:
                    //    await Task1();
                    //    break;
                    case 1:
                        await GetListStudentByIdGroupCommand();
                        break;
                    case 2:
                        await GetCountGenderByIdGroup();
                        break;
                    case 3:
                        await GetListStudentNotInGroup();
                        break;
                    case 4:
                        await GetListGroupNotHaveStudent();
                        break;
                    case 5:
                        await GetListGroupAndStudentInGroup();
                        break;
                    case 6:
                        await GetListGroupAndStudentInGroupByIdSpecial();
                        break;
                    case 7:
                        await AddGroupInSpecial();
                        break;
                    case 8:
                        await TransferStudentToGroupCommand();
                        break;
                    case 9:
                        await ReturnDuplicateStudent();
                        break;
                }
            } while (true);
        }
    }
}
