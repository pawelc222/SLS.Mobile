using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SLS.Mobile.ViewModels
{
    public class BooksCollectionViewModel : INotifyPropertyChanged
    {
        public BooksCollectionViewModel()
        {
            this.Items = new ObservableCollection<BookViewModel>();
        }
        public ObservableCollection<BookViewModel> Items { get; private set; }

        private BookViewModel _selectedItem;

        public BookViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (value != _selectedItem)
                {
                    _selectedItem = value;
                    NotifyPropertyChanged("SelectedItem");
                }
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        public void LoadData()
        {
            // Sample data; replace with real data
            this.Items.Add(new BookViewModel(){Title = "Genesis", ToDate = new DateTime(2014,12,20)});
            this.Items.Add(new BookViewModel() { Title = "Tropiciel", ToDate = new DateTime(2015, 01, 13) });
           
            this.IsDataLoaded = true;
        }
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
