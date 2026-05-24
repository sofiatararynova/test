using System;
using System.Linq;
using Lab.Interfaces;

namespace Lab.Implementations.GenCode1
{
    /// <summary>
    /// Валидатор номеров кредитных карт, реализующий алгоритм Луна.
    /// </summary>
    public class CreditCardValidator : ICreditCardValidator
    {
        /// <summary>
        /// Проверяет, является ли номер карты корректным по алгоритму Луна.
        /// </summary>
        /// <param name="cardNumber">Номер карты, который может содержать пробелы и дефисы.</param>
        /// <returns>true, если номер корректен; иначе false.</returns>
        /// <exception cref="ArgumentException">Выбрасывается, если после удаления пробелов и дефисов строка пуста, 
        /// содержит нецифровые символы или её длина не в диапазоне от 13 до 19 цифр.</exception>
        public bool IsValid(string cardNumber)
        {
            // 1. Удаляем пробелы и дефисы
            string cleaned = cardNumber?.Replace(" ", "").Replace("-", "") ?? string.Empty;

            // 2. Проверка на пустоту и наличие только цифр
            if (string.IsNullOrEmpty(cleaned))
                throw new ArgumentException("Номер карты должен содержать только цифры, пробелы или дефисы");

            if (!cleaned.All(char.IsDigit))
                throw new ArgumentException("Номер карты должен содержать только цифры, пробелы или дефисы");

            // 3. Проверка длины
            if (cleaned.Length < 13 || cleaned.Length > 19)
                throw new ArgumentException("Номер карты должен содержать от 13 до 19 цифр");

            // 4. Алгоритм Луна
            int sum = 0;
            bool alternate = false; // флаг "чётности" позиции (справа налево)

            // Проходим цифры справа налево
            for (int i = cleaned.Length - 1; i >= 0; i--)
            {
                int digit = cleaned[i] - '0';

                if (alternate)
                {
                    digit *= 2;
                    if (digit > 9)
                        digit = digit % 10 + 1; // эквивалентно сумме цифр (например, 12 -> 1+2=3)
                }

                sum += digit;
                alternate = !alternate;
            }

            return sum % 10 == 0;
        }
    }
}