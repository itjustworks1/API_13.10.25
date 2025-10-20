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
    /// Логика взаимодействия для AddGroup.xaml
    /// </summary>
    public partial class AddGroup : Window, INotifyPropertyChanged
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
                Signal();
            }

        }
        private SpecialDTO selecetedSpecial;

        HttpClient client = new HttpClient();
        private List<GroupDTO> groups = [];
        private List<SpecialDTO> specials = [];
        public AddGroup()
        {
            InitializeComponent();
            new Uri("http://localhost:5205/api/");
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
     
        private void Close(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private async void AddGroups(object sender, RoutedEventArgs e)
        {
            if()
            {

            }
            var result = await client.PostAsync($"DB/AddGroupInSpecial?idSpecial=" + SelectedSpecial.Id + "&title=" + Title, null);
        }
    }
}
