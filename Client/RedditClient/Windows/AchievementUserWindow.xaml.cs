using RedditPublicAPI;
using RedditPublicAPI.Entities;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace RedditClient.Windows
{
    /// <summary>
    /// Interaction logic for AchievementUserWindow.xaml
    /// </summary>
    public partial class AchievementUserWindow : Window
    {
        private readonly ObservableCollection<AchievementUser> _achievementUsers = new();

        public AchievementUserWindow()
        {
            InitializeComponent();
            AchievementUserListView.ItemsSource = _achievementUsers;

            Initialize();
        }

        private async void Initialize()
        {
            AchievementComboBox.ItemsSource = await Achievements.GetAchievements(App.Token);
            UserComboBox.ItemsSource = await Users.GetUsers(App.Token);
        }

        private async void GetButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var achievementUsers = await Achievements.GetWithUsers(App.Token);

                _achievementUsers.Clear();

                if (achievementUsers is not null)
                    foreach (var achievementUser in achievementUsers)
                        _achievementUsers.Add(achievementUser);
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
                var achievement = (Achievement)AchievementComboBox.SelectedItem;
                var user = (User)UserComboBox.SelectedItem;

                await Achievements.AddAchivementToUser(achievement, user, App.Token);

                _achievementUsers.Add(new AchievementUser
                {
                    Achievement = achievement,
                    AchievementId = achievement.Id,
                    User = user,
                    UserId = user.Id
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AchievementUserListView.SelectedValue is not AchievementUser achievementUser)
                    return;

                var achievements = await Achievements.GetAchievements(App.Token);
                var users = await Users.GetUsers(App.Token);

                var achievement = achievements.Find(x => x.Id == achievementUser.AchievementId);
                var user = users.Find(x => x.Id == achievementUser.UserId);

                if (achievement is null || user is null)
                    throw new Exception("Achievement or user not found.");

                await Achievements.RemoveAchivementFromUser(achievement, user, App.Token);

                _achievementUsers.Remove(achievementUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AchievementUserListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AchievementUserListView.SelectedValue is not AchievementUser achievementUser)
                return;

            AchievementComboBox.SelectedItem = achievementUser.Achievement;
            UserComboBox.SelectedItem = achievementUser.User;
        }
    }
}
