using System;
using System.Windows.Forms;


namespace home_center.UI
{
    internal class CreditCardValidator
    {
        public static bool IsValidCreditCard(string cardNumber)
        {
            // בדיקת אורך מספר הכרטיס
            if (cardNumber.Length < 13 || cardNumber.Length > 19)
            {
                return false;
            }

            // המרת מספר הכרטיס למערך תווים
            char[] cardDigits = cardNumber.ToCharArray();

            // חישוב סכום הספרות הזוגיות
            int evenSum = 0;
            for (int i = 0; i < cardDigits.Length; i += 2)
            {
                int digit = cardDigits[i] - '0';
                evenSum += digit;
            }

            // הכפלת הספרות המוזרות וסכום התוצאות
            int oddSum = 0;
            for (int i = 1; i < cardDigits.Length; i += 2)
            {
                int digit = cardDigits[i] - '0';
                int doubledDigit = digit * 2;

                // המרת מספרים דו ספרתיים
                if (doubledDigit > 9)
                {
                    doubledDigit = (doubledDigit / 10) + (doubledDigit % 10);
                }

                oddSum += doubledDigit;
            }

            // חישוב סכום כולל
            int totalSum = evenSum + oddSum;

            // בדיקת תקינות מספר הכרטיס
            return totalSum % 10 == 0;
        }

        public static bool ValidateCreditCardAndShowMessage(string cardNumber)
        {
            bool isValid = IsValidCreditCard(cardNumber);

           
            if (isValid)
            {
              MessageBox.Show("מספר הכרטיס תקין") ;
                return true;
            }
            else
            {
                MessageBox.Show("מספר הכרטיס אינו תקין");
                return false;
            }

          
        }
    }
}
