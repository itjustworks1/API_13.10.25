using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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
        HttpClient client = new HttpClient();
        private List<GroupDTO> groups = [];
        private List<StudentDTO> students = [];
        private GroupDTO selectedGroup;
        private StudentDTO selectStudent;
        private int selectSpeciald;
        private string countGender;

        public event PropertyChangedEventHandler? PropertyChanged;
        public void Signal([CallerMemberName] string? prop = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public List<GroupDTO> Groups { get => groups; set { groups = value; Signal(); } }
        public List<StudentDTO> Students { get => students; set { students = value; Signal(); } }
        public GroupDTO SelectedGroup
        {
            get => selectedGroup;
            set
            {
                selectedGroup = value;
                Signal();
                if (selectedGroup != null)
                {
                    GetCountGenderByIdGroup();
                    GetListStudentByIdGroup();
                }
            }
        }
        public StudentDTO SelectStudent { get => selectStudent; set { selectStudent = value; Signal(); } }
        public int SelectSpecialId { get => selectSpeciald; set { selectSpeciald = value; Signal(); } }
        public string CountGender { get => countGender; set { countGender = value; Signal(); } }
        public MainWindow()
        {
            InitializeComponent();
            client.BaseAddress = new Uri("http://localhost:5205/api/");
            DataContext = this;
            GetListGroupAndStudentInGroup();
        }
        public async void GetCountGenderByIdGroup()
        {
            var result = await client.PostAsync($"DB/GetCountGenderByIdGroup?idGroup=" + SelectedGroup.Id, null);
            var content = await result.Content.ReadAsStringAsync();
            CountGender = content;
        }
        public async void GetListStudentByIdGroup()
        {
            var result = await client.PostAsync($"DB/GetListStudentByIdGroup?idGroup=" + SelectedGroup.Id, null);
            var content = await result.Content.ReadFromJsonAsync<IEnumerable<StudentDTO>>();
            Students = content.ToList();
        }
        public async void GetListGroupAndStudentInGroup()
        {
            var result = await client.PostAsync($"DB/GetListGroupAndStudentInGroup", null);
            var content = await result.Content.ReadFromJsonAsync<IEnumerable<GroupDTO>>();
            Groups = content.ToList();
            SelectSpecialId = 0;
        }
        public async void GetListGroupAndStudentInGroupByIdSpecial()
        {
            var result = await client.PostAsync($"DB/GetListGroupAndStudentInGroupByIdSpecial?idSpecial=" + SelectSpecialId, null);
            var content = await result.Content.ReadFromJsonAsync<IEnumerable<GroupDTO>>();
            Groups = content.ToList();
        }

        private void GetListGroupAndStudentInGroupByIdSpecial(object sender, RoutedEventArgs e) => GetListGroupAndStudentInGroupByIdSpecial();

        private void GetListGroupAndStudentInGroup(object sender, RoutedEventArgs e) => GetListGroupAndStudentInGroup();

        private void OpenAddWindow(object sender, RoutedEventArgs e)
        {
            var window = new AddGroupWindow(client, SelectSpecialId);
            window.ShowDialog();
        }

        private void OpenWithoutWindow(object sender, RoutedEventArgs e)
        {
            var window = new ListWithoutDataWindow(client);
            window.ShowDialog();
        }

        private void OpenDublicateWindow(object sender, RoutedEventArgs e)
        {
            var window = new ListDublicateStudentWindow(client);
            window.ShowDialog();
        }
    }
}