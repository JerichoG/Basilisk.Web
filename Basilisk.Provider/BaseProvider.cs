using Basilisk.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Provider
{
    public abstract class BaseProvider
    {
        protected const int _totalDataPerPage = 5;

        //Property model dengan property mirror db namanya harus sama
        public static void MappingModel<TDest, TSource>(TDest destination, TSource source)
        where TDest : class
        where TSource : class
        {
            var destinationProperties = destination.GetType().GetProperties();
            var sourceProperties = source.GetType().GetProperties();

            foreach(var sourceProperty in sourceProperties)
            {
                var property = destinationProperties.FirstOrDefault(a => a.Name == sourceProperty.Name);
                if(property != null)
                {
                    property.SetValue(destination, sourceProperty.GetValue(source));
                }
            }
        }

        public static int TotalHalaman(int totalData)
        {
            
            int totalHalaman = (int)(Math.Ceiling((double)totalData / (double)_totalDataPerPage));

            return totalHalaman;
        }

        public static int GetSkip(int page)
        {
            int skip = (_totalDataPerPage * (page - 1));
            return skip;
        }

        public static string GetIndoFormat(decimal money)
        {
            return money.ToString("C2", CultureInfo.CreateSpecificCulture("id-ID"));
        }
        public static string GetIndoFormat(DateTime date)
        {
            return date.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("id-ID"));
        }


    }
}
