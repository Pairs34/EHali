using System;
using System.Globalization;
using Xamarin.Forms;

namespace HaliciPatron.Converters
{
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var durumColor = new Color();
            switch (value)
            {
                case "ONAYLI":
                {
                    durumColor = Color.LawnGreen;
                    break;
                }

                case "ONAYSIZ":
                {
                    durumColor = Color.Orange;
                    break;
                }

                case "TESLIMEDILDI":
                {
                    durumColor = Color.LightSkyBlue;
                    break;
                }
            }

            return durumColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}