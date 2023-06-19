using RedditPublicAPI;
using RedditPublicAPI.Entities;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace RedditClient.Windows
{
    /// <summary>
    /// Interaction logic for CommunityUserWindow.xaml
    /// </summary>
    public partial class CommunityUserWindow : Window
    {
        private readonly ObservableCollection<CommunityUser> _communityUsers = new();

        public CommunityUserWindow()
        {
            InitializeComponent();
            CommunityUserListView.ItemsSource = _communityUsers;

            Initialize();
        }

        private async void Initialize()
        {
            CommunityComboBox.ItemsSource = await Communities.GetCommunities(App.Token);
            UserComboBox.ItemsSource = await Users.GetUsers(App.Token);
        }

        private async void GetButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var achievementUsers = await Communities.GetWithUsers(App.Token);

                _communityUsers.Clear();

                if (achievementUsers is not null)
                    foreach (var achievementUser in achievementUsers)
                        _communityUsers.Add(achievementUser);
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
                var community = (Community)CommunityComboBox.SelectedItem;
                var user = (User)UserComboBox.SelectedItem;

                await Communities.AddUserToCommunity(community, user, App.Token);

                _communityUsers.Add(new CommunityUser
                {
                    Community = community,
                    CommunityId = community.Id,
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
                if (CommunityUserListView.SelectedValue is not CommunityUser communityUser)
                    return;

                var communities = await Communities.GetCommunities(App.Token);
                var users = await Users.GetUsers(App.Token);

                var community = communities.Find(x => x.Id == communityUser.CommunityId);
                var user = users.Find(x => x.Id == communityUser.UserId);

                if (community is null || user is null)
                    throw new Exception("Community or user not found.");

                await Communities.RemoveUserFromCommunity(community, user, App.Token);

                _communityUsers.Remove(communityUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CommunityUserListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CommunityUserListView.SelectedValue is not CommunityUser communityUser)
                return;

            CommunityComboBox.SelectedItem = communityUser.Community;
            UserComboBox.SelectedItem = communityUser.User;
        }
    }
}
