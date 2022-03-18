using Acr.UserDialogs;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ExpReader.Converters
{
    class FrameImageSourceConverter : IValueConverter
    {


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Book book = (Book)value;

            int bronze = (book.Pages * 30) / 100;
            int silver = (book.Pages * 60) / 100;
            int gold = book.Pages;

            if (book.CurrentPage >= bronze && book.CurrentPage < silver)
            {
                //присвоить бронзовую обложку
                return "Cover";
                UserDialogs.Instance.Alert("Bronze");
            }
            else if (book.CurrentPage >= silver && book.CurrentPage < gold)
            {
                //присвоить серебряную обложку
                UserDialogs.Instance.Alert("Silver");
            }
            else if (book.CurrentPage == gold)
            {
                //присвоить золотую обложку
                UserDialogs.Instance.Alert("Gold");
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
