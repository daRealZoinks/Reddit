using System.Collections.Generic;
using System.Windows;

namespace Client {
	public partial class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();

			PopulateUserListBox();
		}

		private void PopulateUserListBox() {
			var items = new List<string> { "Item 1", "Item 2", "Item 3" };
			UserListBox.ItemsSource = items;
		}

		private void UserImage_Click(object sender, RoutedEventArgs e) {
			if(UserListBox.Visibility == Visibility.Visible)
				UserListBox.Visibility = Visibility.Collapsed;
			else if(UserListBox.Visibility == Visibility.Collapsed)
				UserListBox.Visibility = Visibility.Visible;
		}
	}
}
