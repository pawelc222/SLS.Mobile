using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SLS.Phone.DbLibrary.Classes;

namespace SLS.Phone.DbLibrary
{
    public class IsoStorageData
    {
        public bool FirstRun { get; set; }
        //datetime of last write to iso storage
        public string SLSMobileServiceAddress { get; set; }

        //app or agent
        public string Username { get; set; }

        //true==continuous (every minute) cycle of agent
        public List<Book> BorrowedBooks { get; set; }

        public IsoStorageData()
        {
            FirstRun = true;
        }

    }
}
