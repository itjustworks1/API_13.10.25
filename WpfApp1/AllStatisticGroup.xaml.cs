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
    /// Логика взаимодействия для AllStatisticGroup.xaml
    /// </summary>
    public partial class AllStatisticGroup : Window, INotifyPropertyChanged
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
        public List<SpecialDTO> Specials
        {
            get => specials;
            set
            {
                specials = value;

                Signal();
            }
        }

        public SpecialDTO SelectedSpecial
        {
            get => selecetedSpecial;
            set
            {
                selecetedSpecial = value;
                SetGroupisSpecial();
                Signal();
            }

        }
        private SpecialDTO selecetedSpecial;
 
        HttpClient client = new HttpClient();
        private List<GroupDTO> groups = [];
        private List<SpecialDTO> specials = [];
        public AllStatisticGroup()
        {
            InitializeComponent();
            client.BaseAddress = new Uri("http://localhost:5205/api/");
            SetListSpecial();
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void Signal([CallerMemberName] string prop = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        private async void SetListSpecial()
        {
            var result = await client.PostAsync($"DB/GetListSpecial", null);
            var content = await result.Content.ReadFromJsonAsync<IEnumerable<SpecialDTO>>();
            Specials = content.ToList();
        }
        private async void SetGroupisSpecial()
        {
            if (SelectedSpecial == null)
            {
                Console.WriteLine("Ошибка");
                return;
            }
            var result = await client.PostAsync($"DB/GetListGroupAndStudentInGroupByIdSpecial?idSpecial=" + SelectedSpecial.Id, null);
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
