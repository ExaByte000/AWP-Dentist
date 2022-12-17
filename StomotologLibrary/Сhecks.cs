using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StomotologLibrary
{
    public class Сhecks
    {
        public static bool EmptyString(string surname, string name)
        {
            
            if(surname != String.Empty &&
            name != String.Empty &&
            !String.IsNullOrWhiteSpace(surname) &&
            !String.IsNullOrWhiteSpace(name))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        public static bool EmptyDateTime(DateTime date, string time)
        {
            if(date != null &&
            !String.IsNullOrWhiteSpace(time))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}
