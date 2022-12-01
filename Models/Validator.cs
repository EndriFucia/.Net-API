using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Validator
    {
        public static Boolean IsID(int id)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(id.ToString());
        }

        public static Boolean IsName(String name)
        {
            Regex regex = new Regex(@"[a-zA-Z]{3,30}");
            return regex.IsMatch(name);
        }

        public static Boolean IsDescription(string description)
        {
            Regex regex = new Regex(@"[a-zA-Z]{3,250}");
            return regex.IsMatch(description);
        }

        public static Boolean IsPrice(double price)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(price.ToString());
        }

        public static Boolean IsImage(String image)
        {
            Regex regex = new Regex(@"[a-zA-Z]{3,50}");
            return regex.IsMatch(image);
        }

        public static Boolean IsValidProduct(int id, String name, String description, double price, String image)
        {
            Boolean isValid = true;
            isValid = IsID(id);
            isValid = IsName(name);
            isValid = IsDescription(description);
            isValid = IsPrice(price);
            isValid = IsImage(image);
            return isValid;
        }
    }
}