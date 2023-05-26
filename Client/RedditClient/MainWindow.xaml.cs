using RedditPublicAPI;
using RedditPublicAPI.Dtos;
using RedditPublicAPI.Enums;
using System;
using System.Windows;

namespace RedditClient;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void Login(LoginDto loginDto)
    {
        try
        {
            var token = await Users.Login(loginDto);

            MessageBox.Show(token);

            UserCrud userCrud = new(token);
            userCrud.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        LoginDto loginDto = new()
        {
            Email = EmailTextBox.Text,
            Password = PasswordPasswordBox.Password
        };

        Login(loginDto);
    }

    private void RegisterButton_Click(object sender, RoutedEventArgs e)
    {
        RegisterDto registerDto = new()
        {
            Email = EmailTextBox.Text,
            Password = PasswordPasswordBox.Password,
            Role = AdminCheckBox.IsChecked ?? false ? Role.Administrator : Role.User
        };

        try
        {
            Users.Register(registerDto);

            LoginDto loginDto = new()
            {
                Email = EmailTextBox.Text,
                Password = PasswordPasswordBox.Password
            };

            Login(loginDto);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}
