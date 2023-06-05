using System;
using System.Threading.Tasks;
using System.Windows;
using RedditClient.Windows;
using RedditPublicAPI;
using RedditPublicAPI.Dtos;
using RedditPublicAPI.Enums;

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

    private static async Task<string> Login(string email, string password)
    {
        LoginDto loginDto = new()
        {
            Email = email,
            Password = password
        };

        try
        {
            App.Token = await Users.Login(loginDto);

            Menu menu = new();
            menu.Show();

            return "Login successful.";
        }
        catch (Exception ex)
        {
            return "Login failed:" + ex.Message;
        }
    }

    private async void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            await Login(EmailTextBox.Text, PasswordPasswordBox.Password);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private async void RegisterButton_Click(object sender, RoutedEventArgs e)
    {
        RegisterDto registerDto = new()
        {
            Email = EmailTextBox.Text,
            Password = PasswordPasswordBox.Password,
            Role = AdminCheckBox.IsChecked ?? false ? Role.Administrator : Role.User
        };

        try
        {
            await Users.Register(registerDto);

            await Login(EmailTextBox.Text, PasswordPasswordBox.Password);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}