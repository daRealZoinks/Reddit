using System.Windows;

namespace RedditClient.Windows;

public partial class Menu : Window {
	public Menu() {
		InitializeComponent();
	}

	private void UsersButton_Click(object sender, RoutedEventArgs e) {
		UserCrud userCrud = new();
		userCrud.Show();
	}

	private void MessagesButton_Click(object sender, RoutedEventArgs e) {
		MessageCrud messageCrud = new();
		messageCrud.Show();
	}

	private void AchievementsButton_Click(object sender, RoutedEventArgs e) {
		AchievementCrud achievementCrud = new();
		achievementCrud.Show();
	}

	private void CommunitiesButton_Click(object sender, RoutedEventArgs e) {
		CommunityCrud communityCrud = new();
		communityCrud.Show();
	}

	private void PostsButton_Click(object sender, RoutedEventArgs e) {
		PostCrud postCrud = new();
		postCrud.Show();
	}

	private void CommentsButton_Click(object sender, RoutedEventArgs e) {
		CommentCrud commentCrud = new();
		commentCrud.Show();
	}

	private void VotesButton_Click(object sender, RoutedEventArgs e) {
		VoteCrud voteCrud = new();
		voteCrud.Show();
	}

	private void CommunityUsersButton_Click(object sender, RoutedEventArgs e) {
		CommunityUserWindow communityUserWindow = new();
		communityUserWindow.Show();
	}

	private void AchievementUsersButton_Click(object sender, RoutedEventArgs e) {
		AchievementUserWindow achievementUserWindow = new();
		achievementUserWindow.Show();
	}
}