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
            int Pages = (int)value;
            int CurrentPage = (int)parameter;
            //int CurrentPage = 1;
            int bronze = (Pages * 30) / 100;
            int silver = (Pages * 60) / 100;
            int gold = Pages;

            if (CurrentPage < bronze)
            {
                return "CoverTransparent.png";
            }
            else if (CurrentPage >= bronze && CurrentPage < silver)
            {
                //присвоить бронзовую обложку
                return "CoverBronze.png";
            }
            else if (CurrentPage >= silver && CurrentPage < gold)
            {
                //присвоить серебряную обложку
                return "CoverSilver.png";
            }
            else if (CurrentPage == gold)
            {
                //присвоить золотую обложку
                return "CoverGold.png";
            }
            return "";

            //Book book = (Book)value;

            //int bronze = (book.Pages * 30) / 100;
            //int silver = (book.Pages * 60) / 100;
            //int gold = book.Pages;

            //if (book.CurrentPage < bronze)
            //{
            //    return "CoverTransparent.png";
            //}
            //else if (book.CurrentPage >= bronze && book.CurrentPage < silver)
            //{
            //    //присвоить бронзовую обложку
            //    return "CoverBronze.png";
            //}
            //else if (book.CurrentPage >= silver && book.CurrentPage < gold)
            //{
            //    //присвоить серебряную обложку
            //    return "CoverSilver.png";
            //}
            //else if (book.CurrentPage == gold)
            //{
            //    //присвоить золотую обложку
            //    return "CoverGold.png";
            //}
            //return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
