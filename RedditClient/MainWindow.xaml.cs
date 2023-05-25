using RedditClient.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Controls;

namespace RedditClient;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private List<User> _users = new();

    public MainWindow()
    {
        InitializeComponent();

        GetUsers();
    }

    private async void GetUsers()
    {
        using HttpClient httpClient = new();
        var response = await httpClient.GetAsync("https://localhost:7214/api/User");

        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();

        await response.Content.ReadFromJsonAsync<List<User>>().ContinueWith(task =>
        {
            _users = task.Result ?? throw new Exception();
        });

        UsersListView.ItemsSource = _users;
    }

    private async void Create_Click(object sender, RoutedEventArgs e)
    {
        User user = new()
        {
            Username = UsernameTextBox.Text,
            Email = EmailTextBox.Text,
            Password = PasswordTextBox.Text,
            AccountCreationDate = (DateTime)AccountCreationDateCalendar.SelectedDate,
            Description = DescriptionTextBox.Text
        };

        using HttpClient httpClient = new();
        var response = await httpClient.PostAsJsonAsync("https://localhost:7214/api/User", user);

        GetUsers();
    }

    private async void Delete_Click(object sender, RoutedEventArgs e)
    {
        if (UsersListView.SelectedValue is not User user)
        {
            return;
        }

        using HttpClient httpClient = new();
        var response = await httpClient.DeleteAsync($"https://localhost:7214/api/User/{user.Id}");

        GetUsers();
    }

    private async void Update_Click(object sender, RoutedEventArgs e)
    {
        if ((UsersListView.SelectedValue as User) == null)
        {
            return;
        }

        User user = new()
        {
            Id = (UsersListView.SelectedValue as User).Id,
            Username = UsernameTextBox.Text,
            Email = EmailTextBox.Text,
            Password = PasswordTextBox.Text,
            AccountCreationDate = (DateTime)AccountCreationDateCalendar.SelectedDate,
            Description = DescriptionTextBox.Text
        };

        using HttpClient httpClient = new();
        var response = await httpClient.PutAsJsonAsync("https://localhost:7214/api/User", user);

        GetUsers();
    }

    private void UsersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (UsersListView.SelectedValue is not User user)
        {
            return;
        }

        UsernameTextBox.Text = user.Username;
        EmailTextBox.Text = user.Email;
        PasswordTextBox.Text = user.Password;
        AccountCreationDateCalendar.SelectedDate = user.AccountCreationDate;
        DescriptionTextBox.Text = user.Description;
    }
}