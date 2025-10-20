using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.DTO;
using WebApplication3.DB;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для ListWithoutDataWindow.xaml
    /// </summary>
    public partial class ListWithoutDataWindow : Window, INotifyPropertyChanged
    {
        HttpClient client = new HttpClient();
        private List<GroupDTO> groups = [];
        private List<StudentDTO> students = [];

        public event PropertyChangedEventHandler? PropertyChanged;
        public void Signal([CallerMemberName] string? prop = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        public List<GroupDTO> Groups { get => groups; set { groups = value; Signal(); } }
        public List<StudentDTO> Students { get => students; set { students = value; Signal(); } }
        public ListWithoutDataWindow(HttpClient client)
        {
            InitializeComponent();
            this.client = client;
            DataContext = this;
            GetListStudentNotInGroup();
            GetListGroupNotHaveStudent();
        }
        public async void GetListStudentNotInGroup()
        {
            var result = await client.PostAsync($"DB/GetListStudentNotInGroup", null);
            var content = await result.Content.ReadFromJsonAsync<IEnumerable<StudentDTO>>();
            Students = content.ToList();
        }
        public async void GetListGroupNotHaveStudent()
        {
            var result = await client.PostAsync($"DB/GetListGroupNotHaveStudent", null);
            var content = await result.Content.ReadFromJsonAsync<IEnumerable<GroupDTO>>();
            Groups = content.ToList();
        }

    }
}
