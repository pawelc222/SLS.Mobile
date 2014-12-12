using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using SLS.Phone.DbLibrary.Classes;
using SLS.Phone.DbLibrary.SLSMobile;
using Book = SLS.Phone.DbLibrary.Classes.Book;

namespace SLS.Phone.DbLibrary
{
    public class SLSMobileDataProvider
    {
        private IsoStorageData sharedData = MutexedIsoStorageFile.Read();

        public void SyncData()
        {
            SLSMobileServiceClient client = new SLSMobileServiceClient();

            client.GetBooksForUserAsync(1);
            client.GetBooksForUserCompleted += new EventHandler<GetBooksForUserCompletedEventArgs>(client_GetBooksForUserCompleted);
        }

        void client_GetBooksForUserCompleted(object sender, GetBooksForUserCompletedEventArgs e)
        {
            sharedData.BorrowedBooks = new List<Book>();
            foreach (var book in e.Result)
            {
                Book b = new Book();
                b.Title = book.title;
                b.ToDate = book.ToDate;
                sharedData.BorrowedBooks.Add(b);
            }
            MutexedIsoStorageFile.Write(sharedData);
        }
    }
}
