using RedditPublicAPI;
using RedditPublicAPI.Entities;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace RedditClient.Windows;

public partial class CommunityCrud : Window {
	private readonly ObservableCollection<Community> _communities = new();

	public CommunityCrud() {
		InitializeComponent();
		AchievementListView.ItemsSource = _communities;
	}

	private async void GetButton_Click(object sender, RoutedEventArgs e) {
		try {
			var communities = await Communities.GetCommunities(App.Token);

			_communities.Clear();

			if(communities is not null)
				foreach(var community in communities)
					_communities.Add(community);
		}
		catch(Exception ex) {
			MessageBox.Show(ex.Message);
		}
	}

	private async void CreateButton_Click(object sender, RoutedEventArgs e) {
		try {
			Community community = new() {
				Name = NameTextBox.Text,
				Description = DescriptionTextBox.Text,
				// TODO
			};

			await Communities.AddCommunity(community, App.Token);

			_communities.Add(community);
		}
		catch(Exception ex) {
			MessageBox.Show(ex.Message);
		}
	}

	private async void DeleteButton_Click(object sender, RoutedEventArgs e) {
		try {
			if(AchievementListView.SelectedValue is not Community community)
				return;

			await Communities.DeleteCommunity(community, App.Token);

			_communities.Remove(community);
		}
		catch(Exception ex) {
			MessageBox.Show(ex.Message);
		}
	}

	private async void UpdateButton_Click(object sender, RoutedEventArgs e) {
		try {
			if(AchievementListView.SelectedValue is not Community community)
				return;

			community.Name = NameTextBox.Text;
			community.Description = DescriptionTextBox.Text;
			// TODO

			await Communities.UpdateCommunity(community, App.Token);
		}
		catch(Exception ex) {
			MessageBox.Show(ex.Message);
		}
	}

	private void AchievementListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
		if(AchievementListView.SelectedValue is not Community community)
			return;

		NameTextBox.Text = community.Name;
		DescriptionTextBox.Text = community.Description;
		// TODO
	}
}