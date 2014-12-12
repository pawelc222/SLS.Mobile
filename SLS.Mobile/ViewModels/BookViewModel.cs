using System;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SLS.Mobile.ViewModels
{
    public class BookViewModel : INotifyPropertyChanged
    {
        private string _title;
        public string Title {
            get
            {
                return _title;
            }
            set
            {
                if (value != _title)
                {
                    _title = value;
                    NotifyPropertyChanged("Title");
                }
            }
        }

        private DateTime _toDate;
        public DateTime ToDate
        {
            get { return _toDate; }
            set
            {
                if (value != _toDate)
                {
                    _toDate = value;
                    NotifyPropertyChanged("ToDate");
                }
            }
        }

        private BitmapImage _coverImage = new BitmapImage();

        public BitmapImage CoverImage
        {
            get { return _coverImage; }
            set
            {
                if (value != _coverImage)
                {
                    _coverImage = value;
                    NotifyPropertyChanged("CoverImage");
                }
            }
        }

        //BitmapImage bImage = new BitmapImage();
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
