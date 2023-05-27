using RedditPublicAPI;
using RedditPublicAPI.Entities;
using RedditPublicAPI.Enums;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace RedditClient.Windows;

/// <summary>
///     Interaction logic for UserCrud.xaml
/// </summary> 
public partial class UserCrud : Window
{
    private readonly ObservableCollection<User> _users = new();

    public UserCrud()
    {
        InitializeComponent();
        UsersListView.ItemsSource = _users;
    }

    private async void GetButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var users = await Users.GetUsers(App.Token);

            _users.Clear();

            if (users is not null)
                foreach (var user in users)
                    _users.Add(user);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void CreateButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            User user = new()
            {
                Username = UsernameTextBox.Text,
                Email = EmailTextBox.Text,
                PasswordHash = PasswordHashTextBox.Text,
                AccountCreationDate = AccountCreationDateCalendar.DisplayDate,
                Role = Role.User,
                Description = DescriptionTextBox.Text
            };

            Users.AddUser(user, App.Token);

            _users.Add(user);
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
            if (UsersListView.SelectedValue is not User user) return;

            Users.DeleteUser(user, App.Token);

            _users.Remove(user);
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
            if (UsersListView.SelectedValue is not User user) return;

            user.Username = UsernameTextBox.Text;
            user.Email = EmailTextBox.Text;
            user.PasswordHash = PasswordHashTextBox.Text;
            user.AccountCreationDate = AccountCreationDateCalendar.DisplayDate;
            user.Description = DescriptionTextBox.Text;

            Users.UpdateUser(user, App.Token);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void UsersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (UsersListView.SelectedValue is not User user) return;

        UsernameTextBox.Text = user.Username;
        EmailTextBox.Text = user.Email;
        PasswordHashTextBox.Text = user.PasswordHash;
        AccountCreationDateCalendar.SelectedDate = user.AccountCreationDate;
        DescriptionTextBox.Text = user.Description;
    }
}