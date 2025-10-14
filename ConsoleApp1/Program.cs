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
                }
            } while (true);
        }
    }
}
