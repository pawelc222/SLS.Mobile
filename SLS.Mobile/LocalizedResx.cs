using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SLS.Mobile
{
    public class LocalizedResx
    {
        public LocalizedResx()
        {
                
        }

        private static SLS.Mobile.Resources.AppResources mylocalizedresx = new SLS.Mobile.Resources.AppResources();

        public SLS.Mobile.Resources.AppResources MyLocalizedResources
        {
            get { return mylocalizedresx; }
        }

    }
}
