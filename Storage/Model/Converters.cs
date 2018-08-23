using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Storage.Model
{
    [ValueConversion(typeof(int), typeof(string))]
    public class IdToProviderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            int res;
            if (value != null && Int32.TryParse(value.ToString(), out res))
                return Sql.GetValue<Provider>(res).Name;
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class IdToProductConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            int res;
            if (value != null && Int32.TryParse(value.ToString(), out res))
                return Sql.GetValue<Product>(res).ProductName;
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
