using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace home_center.UI
{
    public class IdValidation
    {
        public static bool ValidateId(string id)
        {
            // בדיקת אורך
            if (id.Length != 9)
            {
                return false;
            }

            // בדיקת תבנית - וודא שהתעודת זהות מכילה רק ספרות
            foreach (char c in id)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            // חישוב - וודא שהספרה האחרונה תואמת את הספרות הקודמות
            int sum = 0;
            for (int i = 0; i < 8; i++)
            {
                int digit = int.Parse(id[i].ToString());
                sum += (i % 2 == 0) ? digit : (digit < 5) ? digit * 2 : digit * 2 - 9;
            }
            int lastDigit = int.Parse(id[8].ToString());
            return (sum + lastDigit) % 10 == 0;
        }

        public static bool IsValidIsraeliCreditCardNumber(string cardNumberStr)
        {
            // בדיקות מקדימות
            if (string.IsNullOrEmpty(cardNumberStr))
            {
                return false;
            }

            if (cardNumberStr.Length != 16)
            {
                return false;
            }

            if (!cardNumberStr.StartsWith("53"))
            {
                return false;
            }

            if (!cardNumberStr.All(char.IsDigit))
            {
                return false;
            }

            // חישוב ספרת ביקורת (אלגוריתם Luhn)
            int sum = 0;
            for (int i = cardNumberStr.Length - 1; i >= 0; i--)
            {
                int digit = int.Parse(cardNumberStr[i].ToString());
                if (i % 2 == 0)
                {
                    digit *= 2;
                    if (digit > 9)
                    {
                        digit -= 9;
                    }
                }
                sum += digit;
            }

            return sum % 10 == 0;
           
        }
    }
}




