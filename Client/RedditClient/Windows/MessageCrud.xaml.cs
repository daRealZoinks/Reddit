using RedditPublicAPI;
using RedditPublicAPI.Entities;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace RedditClient.Windows;

/// <summary>
///     Interaction logic for MessageCrud.xaml
/// </summary>
public partial class MessageCrud : Window
{
    private readonly ObservableCollection<Message> _messages = new();

    public MessageCrud()
    {
        InitializeComponent();
        MessagesListView.ItemsSource = _messages;

        Initialize();
    }

    private async void Initialize()
    {
        SenderComboBox.ItemsSource = await Users.GetUsers(App.Token);
        ReceiverComboBox.ItemsSource = await Users.GetUsers(App.Token);
    }

    private async void GetButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var messages = await Messages.GetMessages(App.Token);

            _messages.Clear();

            if (messages is not null)
                foreach (var message in messages)
                    _messages.Add(message);
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
            Message message = new()
            {
                Content = ContentTextBox.Text,
                DateSent = DateSentCalendar.DisplayDate,
                SenderId = ((User)SenderComboBox.SelectedItem).Id,
                ReceiverId = ((User)ReceiverComboBox.SelectedItem).Id
            };

            await Messages.AddMessage(message, App.Token);

            _messages.Add(message);
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
            if (MessagesListView.SelectedValue is not Message message) return;

            Messages.DeleteMessage(message, App.Token);

            _messages.Remove(message);
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
            if (MessagesListView.SelectedValue is not Message message) return;

            message.Content = ContentTextBox.Text;
            message.DateSent = DateSentCalendar.DisplayDate;
            message.SenderId = ((User)SenderComboBox.SelectedItem).Id;
            message.ReceiverId = ((User)ReceiverComboBox.SelectedItem).Id;

            Messages.UpdateMessage(message, App.Token);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void MessagesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (MessagesListView.SelectedValue is not Message message) return;

        ContentTextBox.Text = message.Content;
        DateSentCalendar.DisplayDate = message.DateSent;
        SenderComboBox.SelectedItem = message.Sender;
        ReceiverComboBox.SelectedItem = message.Receiver;
    }
}