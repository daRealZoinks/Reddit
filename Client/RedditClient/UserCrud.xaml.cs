using RedditPublicAPI;
using RedditPublicAPI.Entities;
using RedditPublicAPI.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace RedditClient
{
    /// <summary>
    /// Interaction logic for UserCrud.xaml
    /// </summary>
    public partial class UserCrud : Window
    {
        private readonly ObservableCollection<User> _users = new();

        private readonly string _token;

        public UserCrud(string token)
        {
            InitializeComponent();

            _token = token;

            UsersListView.ItemsSource = _users;
        }

        private async void Get_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<User>? users = await Users.GetUsers(_token);

                _users.Clear();

                if (users is not null)
                {
                    foreach (var user in users)
                    {
                        _users.Add(user);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Create_Click(object sender, RoutedEventArgs e)
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

                Users.AddUser(user, _token);

                _users.Add(user);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UsersListView.SelectedValue is not User user)
                {
                    return;
                }

                Users.DeleteUser(user, _token);

                _users.Remove(user);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UsersListView.SelectedValue is not User user)
                {
                    return;
                }

                user.Username = UsernameTextBox.Text;
                user.Email = EmailTextBox.Text;
                user.PasswordHash = PasswordHashTextBox.Text;
                user.AccountCreationDate = AccountCreationDateCalendar.DisplayDate;
                user.Description = DescriptionTextBox.Text;

                Users.UpdateUser(user, _token);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UsersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UsersListView.SelectedValue is not User user)
            {
                return;
            }

            UsernameTextBox.Text = user.Username;
            EmailTextBox.Text = user.Email;
            PasswordHashTextBox.Text = user.PasswordHash;
            AccountCreationDateCalendar.SelectedDate = user.AccountCreationDate;
            DescriptionTextBox.Text = user.Description;
        }
    }
}
