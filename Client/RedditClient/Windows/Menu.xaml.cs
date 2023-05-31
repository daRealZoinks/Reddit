using System.Windows;

namespace RedditClient.Windows;

/// <summary>
///     Interaction logic for Menu.xaml
/// </summary>
public partial class Menu : Window
{
    public Menu()
    {
        InitializeComponent();
    }

    private void UsersButton_Click(object sender, RoutedEventArgs e)
    {
        UserCrud userCrud = new();
        userCrud.Show();
    }

    private void MessagesButton_Click(object sender, RoutedEventArgs e)
    {
        MessageCrud messageCrud = new();
        messageCrud.Show();
    }

    private void AchievementsButton_Click(object sender, RoutedEventArgs e)
    {
        AchievementCrud achievementCrud = new();
        achievementCrud.Show();
    }
}