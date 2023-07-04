using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umvel.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime Default(this DateTime date)
        {
            if (date == new DateTime(1, 1, 1))
            {
                return new DateTime(1900, 1, 1);
            }
            else
            {
                return date;
            }

        }
    }
}
