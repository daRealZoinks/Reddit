using System.Net.Http;
using System.Windows;

namespace RedditClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            using HttpClient httpClient = new();
            HttpResponseMessage? response = await httpClient.GetAsync("https://localhost:7214/api/User");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            DebugTextBlock.Text = responseBody;
        }
    }
}
