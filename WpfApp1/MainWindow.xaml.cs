using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.DTO;
namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
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

        public StudentDTO SelectedStudent
        {
            get => selectedStudent;
            set
            {
                selectedStudent = value;
                Signal();
            }
                
        }
        private StudentDTO selectedStudent;

        public GroupDTO SelectedGroup 
        { 
            get => selectedGroup; 
            set 
            {
                selectedGroup = value;
                SetListStudent();
                Signal();
            }
        }
        private GroupDTO selectedGroup;

        HttpClient client = new HttpClient();
        private List<GroupDTO> groups = [];
        private List<StudentDTO> students = [];
        private string countGender;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void Signal([CallerMemberName] string prop = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public string CountGender { get => countGender; set { countGender = value; SetListStudent(); Signal(); } }
        public MainWindow()
        {
            InitializeComponent();
            client.BaseAddress = new Uri("http://localhost:5205/api/");
            SetListGroup();
            DataContext = this;
        }


        public async void SetListGroup()
        {
            var result = await client.PostAsync($"DB/GetListGroupAndStudentInGroup", null);
            var content = await result.Content.ReadFromJsonAsync<IEnumerable<GroupDTO>>();
            Groups = content.ToList();
        }

        public async void SetListStudent()
        {
            if(SelectedGroup == null)
            {
                Console.WriteLine("Ошибка");
                return;
            }
            var idGroup = SelectedGroup.Id;
            var result = await client.PostAsync($"DB/GetListStudentByIdGroup?idGroup=" + idGroup, null);
            var content = await result.Content.ReadFromJsonAsync<IEnumerable<StudentDTO>>();
            Students = content.ToList();

            var results = await client.PostAsync($"DB/GetCountGenderByIdGroup?idGroup=" + idGroup, null);
            var contents = await results.Content.ReadAsStringAsync();
            CountGender = contents;
        }

        private void ListGroup(object sender, RoutedEventArgs e)
        {

        }
        private void AllListStat(object sender, RoutedEventArgs e)
        {

        }

        private void ListDuplicate(object sender, RoutedEventArgs e)
        {
            new DuplicateStudent().Show();
            Close();
        }
    }
}