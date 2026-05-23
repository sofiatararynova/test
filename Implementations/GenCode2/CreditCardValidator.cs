using Lab.Interfaces; 
using System; 
using System.Linq; 
 
namespace Lab.Implementations.GenCode2 
{ 
    public class CreditCardValidator : ICreditCardValidator 
    { 
        public bool IsValid(string cardNumber) 
        { 
            // Шаг 1: Удалить из входной строки все пробелы и дефисы 
            string cleanedNumber = cardNumber.Replace(" ", "").Replace("-", ""); 
 
            // Шаг 2: Проверить, что строка не пустая и состоит только из цифр 
            if (string.IsNullOrEmpty(cleanedNumber) || !cleanedNumber.All(char.IsDigit)) 
            { 
                throw new ArgumentException("Номер карты должен содержать только цифры, пробелы или дефисы"); 
            } 
 
            // Шаг 3: Проверить длину цифровой строки (от 13 до 19 цифр включительно) 
            if (cleanedNumber.Length < 13 || cleanedNumber.Length > 19) 
            { 
                throw new ArgumentException("Номер карты должен содержать от 13 до 19 цифр"); 
            } 
 
            // Шаг 4: Применить алгоритм Луна для проверки контрольной суммы 
 
 
            int sum = 0; 
            bool isEvenPosition = false; // Начинаем с предпоследней цифры (чётная позиция справа налево) 
 
            // Проходим по цифрам справа налево 
            for (int i = cleanedNumber.Length - 1; i >= 0; i--) 
            { 
                int digit = int.Parse(cleanedNumber[i].ToString()); 
 
                if (isEvenPosition) 
                { 
                    // Умножаем цифру на 2 
                    digit *= 2; 
 
                    // Если результат больше 9, заменяем суммой цифр (эквивалентно вычитанию 9) 
                    if (digit > 9) 
                    { 
                        digit -= 9; 
                    } 
                } 
 
                sum += digit; 
                isEvenPosition = !isEvenPosition; // Переключаем флаг для следующей цифры 
            } 
 
            // Проверяем, делится ли сумма на 10 без остатка 
            return sum % 10 == 0; 
        } 
    } 
} 