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

            var result = await client.PostAsJsonAsync($"DB/GetListStudentByIdGroup", idGroup);
            var content = await result.Content.ReadFromJsonAsync<IEnumerable<StudentDTO>>();
            foreach(StudentDTO student in content)
                Console.WriteLine(student.LastName);
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
                }
            } while (true);
        }
    }
}
