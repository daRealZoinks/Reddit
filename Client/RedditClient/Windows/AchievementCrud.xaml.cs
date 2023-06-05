using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using RedditPublicAPI;
using RedditPublicAPI.Entities;

namespace RedditClient.Windows;

/// <summary>
///     Interaction logic for AchievementCrud.xaml
/// </summary>
public partial class AchievementCrud : Window
{
    private readonly ObservableCollection<Achievement> _achievements = new();


    public AchievementCrud()
    {
        InitializeComponent();
        AchievementListView.ItemsSource = _achievements;
    }

    private async void GetButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var achievements = await Achievements.GetAchievements(App.Token);

            _achievements.Clear();

            if (achievements is not null)
                foreach (var achievement in achievements)
                    _achievements.Add(achievement);
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
            Achievement achievement = new()
            {
                Name = NameTextBox.Text,
                Description = DescriptionTextBox.Text,
                Value = int.Parse(ValueTextBox.Text)
            };

            await Achievements.AddAchievement(achievement, App.Token);

            _achievements.Add(achievement);
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
            if (AchievementListView.SelectedValue is not Achievement achievement) return;

            await Achievements.DeleteAchievement(achievement, App.Token);

            _achievements.Remove(achievement);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private async void UpdateButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (AchievementListView.SelectedValue is not Achievement achievement) return;

            achievement.Name = NameTextBox.Text;
            achievement.Description = DescriptionTextBox.Text;
            achievement.Value = int.Parse(ValueTextBox.Text);

            await Achievements.UpdateAchievement(achievement, App.Token);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void AchievementListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (AchievementListView.SelectedValue is not Achievement achievement) return;

        NameTextBox.Text = achievement.Name;
        DescriptionTextBox.Text = achievement.Description;
        ValueTextBox.Text = achievement.Value.ToString();
    }
}