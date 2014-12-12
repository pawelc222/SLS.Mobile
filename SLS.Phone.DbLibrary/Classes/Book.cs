using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using System.Xml.Serialization;
namespace SLS.Phone.DbLibrary.Classes
{
    public class Book
    {
        public string Title { get; set; }
        public DateTime ToDate { get; set; }
        
        [XmlIgnore]
        public BitmapImage CoverImage { get; set; }
        
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [XmlElement("CoverImage")]
        public byte[] CoverImageSerialized { get; set; }

        public void ImgToByte()
        {
            if (CoverImage != null)
            {
                MemoryStream ms = new MemoryStream();
                WriteableBitmap wb = new WriteableBitmap(CoverImage);
                wb.SaveJpeg(ms, wb.PixelWidth, wb.PixelHeight, 0, 85);
                CoverImageSerialized = ms.GetBuffer();
            }
        }

        public void ByteToImg()
        {
            if (CoverImageSerialized != null && CoverImageSerialized.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream(CoverImageSerialized))
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    CoverImage = new BitmapImage();
                    CoverImage.SetSource(ms);
                }
            }
        }
    }
}
