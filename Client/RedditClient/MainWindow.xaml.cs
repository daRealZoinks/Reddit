using RedditClient.Windows;
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

    private static async void Login(string email, string password)
    {
        LoginDto loginDto = new()
        {
            Email = email,
            Password = password
        };

        App.Token = await Users.Login(loginDto);

        Menu menu = new();
        menu.Show();
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Login(EmailTextBox.Text, PasswordPasswordBox.Password);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
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

            Login(EmailTextBox.Text, PasswordPasswordBox.Password);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}
