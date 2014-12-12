using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using SLS.Phone.DbLibrary;

namespace SLS.Mobile
{
    public partial class Settings : PhoneApplicationPage
    {
        public Settings()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            App.IsolatedData.SLSMobileServiceAddress = WCFAddressTbx.Text;
            App.IsolatedData.Username = UsernameTbx.Text;
            MutexedIsoStorageFile.Write(App.IsolatedData);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.IsolatedData.FirstRun)
            {
                WCFAddressTbx.Text = App.WcfServiceAddress;
                UsernameTbx.Text = App.Login;
            }
            base.OnNavigatedTo(e);
        }
    }
}