using RedditPublicAPI;
using RedditPublicAPI.Entities;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace RedditClient.Windows;

public partial class CommentCrud : Window {
	private readonly ObservableCollection<Comment> _comments = new();

	public CommentCrud() {
		InitializeComponent();
		CommentsListView.ItemsSource = _comments;

		Initialize();
	}

	private async void Initialize() {
		PostComboBox.ItemsSource = await Posts.GetPosts(App.Token);
		AuthorComboBox.ItemsSource = await Users.GetUsers(App.Token);
	}

	private async void GetButton_Click(object sender, RoutedEventArgs e) {
		try {
			var comments = await Comments.GetComments(App.Token);

			_comments.Clear();

			if(comments is not null)
				foreach(var comment in comments)
					_comments.Add(comment);
		}
		catch(Exception ex) {
			MessageBox.Show(ex.Message);
		}
	}

	private async void CreateButton_Click(object sender, RoutedEventArgs e) {
		try {
			Comment comment = new() {
				PostDate = PostDateCalendar.DisplayDate,
				Content = ContentTextBox.Text,
				PostId = ((Post)PostComboBox.SelectedItem).Id,
				AuthorId = ((User)AuthorComboBox.SelectedItem).Id
			};

			await Comments.AddComment(comment, App.Token);

			_comments.Add(comment);
		}
		catch(Exception ex) {
			MessageBox.Show(ex.Message);
		}
	}

	private void UpdateButton_Click(object sender, RoutedEventArgs e) {
		try {
			if(CommentsListView.SelectedValue is not Comment comment)
				return;

			comment.PostDate = PostDateCalendar.DisplayDate;
			comment.Content = ContentTextBox.Text;
			comment.PostId = ((Post)PostComboBox.SelectedItem).Id;
			comment.AuthorId = ((User)AuthorComboBox.SelectedItem).Id;

			Comments.UpdateComment(comment, App.Token);
		}
		catch(Exception ex) {
			MessageBox.Show(ex.Message);
		}
	}

	private void DeleteButton_Click(object sender, RoutedEventArgs e) {
		try {
			if(CommentsListView.SelectedValue is not Comment comment)
				return;

			Comments.DeleteComment(comment, App.Token);

			_comments.Remove(comment);
		}
		catch(Exception ex) {
			MessageBox.Show(ex.Message);
		}
	}

	private void CommentsListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
		if(CommentsListView.SelectedValue is not Comment comment)
			return;

		PostDateCalendar.DisplayDate = comment.PostDate;
		ContentTextBox.Text = comment.Content;
		PostComboBox.SelectedItem = comment.Post;
		AuthorComboBox.SelectedItem = comment.Author;
	}
}