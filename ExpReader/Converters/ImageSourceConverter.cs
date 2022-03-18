using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ExpReader.Converters
{
    class ImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int pages = (int)value;
            if (pages < 200)
            {
                return "CoverGreen.png";
            }
            else if (pages >= 200 && pages < 1200)
            {
                return "CoverBlue.png";
            }
            else if (pages >= 1200)
            {
                return "CoverRed.png";
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
