using RedditPublicAPI;
using RedditPublicAPI.Entities;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace RedditClient.Windows;

public partial class PostCrud : Window
{
    private readonly ObservableCollection<Post> _posts = new();

    public PostCrud()
    {
        InitializeComponent();
        PostsListView.ItemsSource = _posts;

        Initialize();
    }

    private async void Initialize()
    {
        AuthorComboBox.ItemsSource = await Users.GetUsers(App.Token);
        CommunityComboBox.ItemsSource = await Communities.GetCommunities(App.Token);
    }

    private async void GetButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var posts = await Posts.GetPosts(App.Token);

            _posts.Clear();

            if (posts is not null)
                foreach (var post in posts)
                    _posts.Add(post);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private async void CreateButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Post post = new()
            {
                PostDate = PostDateCalendar.DisplayDate,
                Title = TitleTextBox.Text,
                Content = ContentTextBox.Text,
                AuthorId = ((User)AuthorComboBox.SelectedItem).Id,
                CommunityId = ((Community)CommunityComboBox.SelectedItem).Id
            };

            await Posts.AddPost(post, App.Token);

            _posts.Add(post);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void UpdateButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (PostsListView.SelectedValue is not Post post)
                return;

            post.PostDate = PostDateCalendar.DisplayDate;
            post.Title = TitleTextBox.Text;
            post.Content = ContentTextBox.Text;
            post.AuthorId = ((User)AuthorComboBox.SelectedItem).Id;
            post.CommunityId = ((Community)CommunityComboBox.SelectedItem).Id;

            Posts.UpdatePost(post, App.Token);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (PostsListView.SelectedValue is not Post post)
                return;

            Posts.DeletePost(post, App.Token);

            _posts.Remove(post);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void PostsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (PostsListView.SelectedValue is not Post post)
            return;

        PostDateCalendar.DisplayDate = post.PostDate;
        TitleTextBox.Text = post.Title;
        ContentTextBox.Text = post.Content;
        AuthorComboBox.SelectedItem = post.Author;
        CommunityComboBox.SelectedItem = post.Community;
    }
}