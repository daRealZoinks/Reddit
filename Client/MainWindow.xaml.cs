using System.Net.Http;
using System.Windows;

namespace Client {
	public partial class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();
		}

		private async void JsonData_button_Click(object sender, RoutedEventArgs e) {
			using(HttpClient httpClient = new HttpClient()) {
				HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7214/api/User");
				response.EnsureSuccessStatusCode();
				string responseBody = await response.Content.ReadAsStringAsync();
				JsonData_textblock.Text = responseBody;
			}
		}
	}
}
