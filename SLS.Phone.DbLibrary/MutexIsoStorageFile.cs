using System;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using SLS.Phone.DbLibrary;

namespace SLS.Phone.DbLibrary
{
    public static class MutexedIsoStorageFile
    {
        //named mutex
        private static Mutex Mutex = new Mutex(false, "BackgroundAgentDemo1");

        //name of isolated storage file
        private const String IsoStorageDateFile = "SLSMobileData.backup";

        //read iso storage
        //debug.writeline lets me "see" agent working in VS output window
        public static IsoStorageData Read()
        {
            IsoStorageData IsoStorageData = new IsoStorageData();
            
            Mutex.WaitOne();

            try
            {
                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                using (
                    var stream = new IsolatedStorageFileStream(IsoStorageDateFile, FileMode.OpenOrCreate,
                        FileAccess.Read, store))
                using (var reader = new StreamReader(stream))
                {
                    if (!reader.EndOfStream)
                    {
                        var serializer = new XmlSerializer(typeof (IsoStorageData));
                        IsoStorageData = (IsoStorageData) serializer.Deserialize(reader);
                    }
                }
            }
            finally
            {
                Mutex.ReleaseMutex();
            }
            //Debug.WriteLine("RRR-data.LastProcessToTouchFile=" + IsoStorageData.LastProcessToTouchFile);
            //Debug.WriteLine("RRR-data.LastTimeFileTouched=" + IsoStorageData.LastTimeFileTouched.ToString());
            //Debug.WriteLine("RRR-data.CycleAgentEveryMinute=" + IsoStorageData.CycleAgentEveryMinute.ToString());

            return IsoStorageData;
        }

        //write iso storage
        //debug.writeline lets me "see" agent working in VS output window
        public static void Write(IsoStorageData data)
        {
            //Debug.WriteLine("WWW-data.LastProcessToTouchFile=" + data.LastProcessToTouchFile);
            //Debug.WriteLine("WWW-data.LastTimeFileTouched=" + data.LastTimeFileTouched.ToString());
            //Debug.WriteLine("WWW-data.CycleAgentEveryMinute=" + data.CycleAgentEveryMinute.ToString());

            // persist the data using isolated storage
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            using (var stream = new IsolatedStorageFileStream(IsoStorageDateFile,
                FileMode.Create,
                FileAccess.Write,
                store))
            {
                var serializer = new XmlSerializer(typeof (IsoStorageData));
                serializer.Serialize(stream, data);
            }
        }

      

    }
}