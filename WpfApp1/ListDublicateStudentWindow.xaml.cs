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
    /// Логика взаимодействия для ListDublicateStudentWindow.xaml
    /// </summary>
    public partial class ListDublicateStudentWindow : Window, INotifyPropertyChanged
    {
        private readonly HttpClient client;
        public event PropertyChangedEventHandler? PropertyChanged;
        public void Signal([CallerMemberName] string? prop =  null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        public List<StudentDTO> Students { get; set; }
        public ListDublicateStudentWindow(HttpClient client)
        {
            InitializeComponent();
            this.client = client;
            DataContext = this;
            ReturnDuplicateStudent();
        }
        public async void ReturnDuplicateStudent()
        {
            var result = await client.PostAsync($"DB/ReturnDuplicateStudent", null);
            var content = await result.Content.ReadFromJsonAsync<IEnumerable<StudentDTO>>();
            Students = content.ToList();
        }

    }
}
