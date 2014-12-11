using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Notification;
using Microsoft.Phone.Tasks;
using SLS.Mobile.SLSMobile;
using SLS.Mobile.ViewModels;

namespace SLS.Mobile
{
    public partial class MainPage : PhoneApplicationPage
    {
       // private BookViewModel selectedBook;
        // Constructor
        public MainPage()
        {
            // Holds the push channel that is created or found.
            HttpNotificationChannel pushChannel;

            // The name of our push channel.
            string channelName = "TileSampleChannel";
            DataContext = App.ViewModel;
            InitializeComponent();
            
            Loaded += new RoutedEventHandler(MainPage_Loaded);
            // Try to find the push channel.
            pushChannel = HttpNotificationChannel.Find(channelName);

            // If the channel was not found, then create a new connection to the push service.
            if (pushChannel == null)
            {
                pushChannel = new HttpNotificationChannel(channelName);

                // Register for all the events before attempting to open the channel.
                pushChannel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(PushChannel_ChannelUriUpdated);
                pushChannel.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(PushChannel_ErrorOccurred);

                pushChannel.Open();

                // Bind this new channel for Tile events.
                pushChannel.BindToShellTile();
                Thread.Sleep(5000);
                SendChannelUriToService(pushChannel.ChannelUri.AbsoluteUri);

            }
            else
            {
                // The channel was already open, so just register for all the events.
                pushChannel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(PushChannel_ChannelUriUpdated);
                pushChannel.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(PushChannel_ErrorOccurred);

                // Display the URI for testing purposes. Normally, the URI would be passed back to your web service at this point.
                System.Diagnostics.Debug.WriteLine(pushChannel.ChannelUri.ToString());
                //MessageBox.Show(String.Format("Channel Uri is {0}",
                //    pushChannel.ChannelUri.ToString()));


                SendChannelUriToService(pushChannel.ChannelUri.AbsoluteUri);
            }
            

        }

        protected void SendChannelUriToService(string ChannelUri)
        {
            SLSMobileServiceClient client = new SLSMobileServiceClient();
            client.AddPushNotificationServiceAsync(ChannelUri);
            client.AddPushNotificationServiceCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_AddPushNotificationServiceCompleted);
        }

        private void client_AddPushNotificationServiceCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            MessageBox.Show(String.Format("Channel Uri completed send to SLS Service"));
        }

        private void PushChannel_ChannelUriUpdated(object sender, NotificationChannelUriEventArgs e)
        {

            Dispatcher.BeginInvoke(() =>
            {
                // Display the new URI for testing purposes. Normally, the URI would be passed back to your web service at this point.
                System.Diagnostics.Debug.WriteLine(e.ChannelUri.ToString());
                MessageBox.Show(String.Format("Channel Uri is {0}",
                    e.ChannelUri.ToString()));

            });
            
        }

        private void PushChannel_ErrorOccurred(object sender, NotificationChannelErrorEventArgs e)
        {
            // Error handling logic for your particular application would be here.
            Dispatcher.BeginInvoke(() =>
                MessageBox.Show(String.Format("A push notification {0} error occurred.  {1} ({2}) {3}",
                    e.ErrorType, e.Message, e.ErrorCode, e.ErrorAdditionalData))
                    );
        }
        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private void ApplicationBarMenuItem_OnClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.Relative));
        }

        private void UpdateTile_OnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TakePhotoButton_OnClick(object sender, EventArgs e)
        {
            CameraCaptureTask pcTask = new CameraCaptureTask();
            pcTask.Completed += new EventHandler<PhotoResult>(pcTask_Completed);
            pcTask.Show(); 
        }
        void pcTask_Completed(object sender, PhotoResult e)
        {
            switch (e.TaskResult)
            {
                case TaskResult.OK:
                    App.ViewModel.SelectedItem.CoverImage.SetSource(e.ChosenPhoto);
                    break;
                case TaskResult.Cancel:
                    break;
            }
        }

        private void BooksListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (BooksListBox.SelectedIndex >= 0)
            {
                App.ViewModel.SelectedItem = BooksListBox.SelectedItem as BookViewModel;
            }
            
        }

        private void RemindButton_OnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}