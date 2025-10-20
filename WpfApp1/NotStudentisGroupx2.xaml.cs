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
namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для NotStudentisGroupx2.xaml
    /// </summary>
    public partial class NotStudentisGroupx2 : Window, INotifyPropertyChanged
    {
        public List<GroupDTO> Groups
        {
            get => groups;
            set
            {
                groups = value;
                Signal();
            }
        }
        public List<StudentDTO> Students
        {
            get => students;
            set
            {
                students = value;
                Signal();
            }
        }

        HttpClient client = new HttpClient();
        private List<GroupDTO> groups = [];
        private List<StudentDTO> students = [];
        public NotStudentisGroupx2()
        {
            InitializeComponent();
            client.BaseAddress = new Uri("http://localhost:5205/api/");
            SetListStudentNotGroup();
            SetListGroupNotStudent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void Signal([CallerMemberName] string prop = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        private async void SetListStudentNotGroup()
        {
            var result = await client.PostAsync($"DB/GetListStudentNotInGroup", null);
            var content = await result.Content.ReadFromJsonAsync<IEnumerable<StudentDTO>>();
            Students = content.ToList();
        }
        private async void SetListGroupNotStudent()
        {
            var result = await client.PostAsync($"DB/GetListGroupNotHaveStudent", null);
            var content = await result.Content.ReadFromJsonAsync<IEnumerable<GroupDTO>>();
            Groups = content.ToList();
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}
