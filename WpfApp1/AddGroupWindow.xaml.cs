using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
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
using WebApplication3.DB;
using WpfApp1.DTO;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для AddGroupWindow.xaml
    /// </summary>
    public partial class AddGroupWindow : Window, INotifyPropertyChanged
    {
        private readonly HttpClient client;
        private readonly int specialId;
        private string groupTitle;

        public event PropertyChangedEventHandler? PropertyChanged;
        public void Signal([CallerMemberName] string? prop  = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        public string GroupTitle { get => groupTitle; set { groupTitle = value; Signal(); } }
        public AddGroupWindow(HttpClient client, int specialId)
        {
            InitializeComponent();
            this.client = client;
            this.specialId = specialId;
            DataContext = this;
        }
        public async void AddGroupInSpecial()
        {
            if (GroupTitle != null)
            {
                var result = await client.PostAsync($"DB/AddGroupInSpecial?idSpecial=" + specialId + "&title=" + GroupTitle, null);
                MessageBox.Show(result.StatusCode.ToString());
            }
        }

        private void AddGroupInSpecial(object sender, RoutedEventArgs e) => AddGroupInSpecial();
    }
}
