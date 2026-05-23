using System;

// Пространство имен интерфейса уже задано отдельно 
// namespace Lab.Interfaces; 

namespace Lab.Implementations.GenCode3
{
    public class CreditCardValidator : Lab.Interfaces.ICreditCardValidator
    {
        // Реализация метода проверки номера кредитной карты 
        public bool IsValid(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
                throw new ArgumentException("Номер карты должен содержать только цифры, пробелы или дефисы"); 

            // Убираем все символы кроме цифр 
            string cleaned = RemoveNonDigits(cardNumber);

            // Проверяем длину очищенной строки 
            if (cleaned.Length < 13 || cleaned.Length > 19)
                throw new ArgumentException("Номер карты должен содержать от 13 до 19 цифр"); 


            return LuhnCheck(cleaned);
        }

        private static string RemoveNonDigits(string input)
        {
            char[] charsToRemove = { ' ', '-' };
            foreach (char c in charsToRemove)
            {
                input = input.Replace(c.ToString(), "");
            }
            return input;
        }

        private static bool LuhnCheck(string digits)
        {
            int sum = 0;
            bool isEvenPosition = false;

            for (int i = digits.Length - 1; i >= 0; i--)
            {
                int digit = (digits[i] - '0');

                if (isEvenPosition)
                {
                    digit *= 2;

                    if (digit > 9)
                        digit -= 9; // Суммируем две цифры числа (например, 18 → 1+8=9) 
                }

                sum += digit;
                isEvenPosition = !isEvenPosition;
            }

            return sum % 10 == 0;
        }
    }
}