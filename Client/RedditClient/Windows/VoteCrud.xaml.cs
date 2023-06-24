using RedditPublicAPI;
using RedditPublicAPI.Entities;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace RedditClient.Windows;

public partial class VoteCrud : Window {
	private readonly ObservableCollection<Vote> _votes = new();

	public VoteCrud() {
		InitializeComponent();
		VotesListView.ItemsSource = _votes;

		Initialize();
	}

	private async void Initialize() {
		UserComboBox.ItemsSource = await Users.GetUsers(App.Token);
		PostComboBox.ItemsSource = await Posts.GetPosts(App.Token);
	}

	private async void GetButton_Click(object sender, RoutedEventArgs e) {
		try {
			var votes = await Votes.GetVotes(App.Token);

			_votes.Clear();

			if(votes is not null)
				foreach(var vote in votes)
					_votes.Add(vote);
		}
		catch(Exception ex) {
			MessageBox.Show(ex.Message);
		}
	}

	private async void CreateButton_Click(object sender, RoutedEventArgs e) {
		try {
			Vote vote = new() {
				Upvote = UpvoteCheckBox.IsChecked ?? false,
				UserId = ((User)UserComboBox.SelectedItem).Id,
				PostId = ((Post)PostComboBox.SelectedItem).Id
			};

			await Votes.AddVote(vote, App.Token);

			_votes.Add(vote);
		}
		catch(Exception ex) {
			MessageBox.Show(ex.Message);
		}
	}

	private void UpdateButton_Click(object sender, RoutedEventArgs e) {
		try {
			if(VotesListView.SelectedValue is not Vote vote)
				return;

			vote.Upvote = UpvoteCheckBox.IsChecked ?? false;
			vote.UserId = ((User)UserComboBox.SelectedItem).Id;
			vote.PostId = ((Post)PostComboBox.SelectedItem).Id;

			Votes.UpdateVote(vote, App.Token);
		}
		catch(Exception ex) {
			MessageBox.Show(ex.Message);
		}
	}

	private void DeleteButton_Click(object sender, RoutedEventArgs e) {
		try {
			if(VotesListView.SelectedValue is not Vote vote)
				return;

			Votes.DeleteVote(vote, App.Token);

			_votes.Remove(vote);
		}
		catch(Exception ex) {
			MessageBox.Show(ex.Message);
		}
	}

	private void VotesListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
		if(VotesListView.SelectedValue is not Vote vote)
			return;

		UpvoteCheckBox.IsChecked = vote.Upvote;
		UserComboBox.SelectedItem = vote.User;
		PostComboBox.SelectedItem = vote.Post;
	}
}
