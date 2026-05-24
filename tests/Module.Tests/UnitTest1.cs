using System;
using Lab.Interfaces;
using NUnit.Framework;


namespace Lab;

[TestFixture(typeof(Lab.Implementations.GenCode1.CreditCardValidator), Category = "GenCode1")]
[TestFixture(typeof(Lab.Implementations.GenCode2.CreditCardValidator), Category = "GenCode2")]
[TestFixture(typeof(Lab.Implementations.GenCode3.CreditCardValidator), Category = "GenCode3")]
public class CreditCardValidatorTests
{
    private ICreditCardValidator _validator;
    private readonly Type _validatorType;

    // Конструктор получает тип, переданный атрибутом TestFixture
    public CreditCardValidatorTests(Type validatorType)
    {
        _validatorType = validatorType;
    }

    [SetUp]
    public void Setup()
    {
        // Создаём экземпляр переданного типа перед каждым тестом
        _validator = (ICreditCardValidator)Activator.CreateInstance(_validatorType);
    }

    // Тесты чёрного ящика
    [Test]
    public void IsValid_ValidNumber_ReturnsTrue()
    {
        string cardNumber = "4532015112830366";
        bool result = _validator.IsValid(cardNumber);
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsValid_ValidNumberWithSpaces_ReturnsTrue()
    {
        string cardNumber = "4532 0151 1283 0366";
        bool result = _validator.IsValid(cardNumber);
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsValid_ValidNumberWithHyphens_ReturnsTrue()
    {
        string cardNumber = "4532-0151-1283-0366";
        bool result = _validator.IsValid(cardNumber);
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsValid_InvalidNumber_ReturnsFalse()
    {
        string cardNumber = "4532015112830367";
        bool result = _validator.IsValid(cardNumber);
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsValid_EmptyString_ThrowsArgumentException()
    {
        var ex = Assert.Throws<ArgumentException>(() => _validator.IsValid(""));
        Assert.That(ex.Message, Does.Contain("только цифры, пробелы или дефисы"));
    }

    [Test]
    public void IsValid_WhitespaceString_ThrowsArgumentException()
    {
        var ex = Assert.Throws<ArgumentException>(() => _validator.IsValid("   "));
        Assert.That(ex.Message, Does.Contain("только цифры, пробелы или дефисы"));
    }

    [Test]
    public void IsValid_StringWithNonDigit_ThrowsArgumentException()
    {
        var ex = Assert.Throws<ArgumentException>(() => _validator.IsValid("4532 0151 1283 036A"));
        Assert.That(ex.Message, Does.Contain("только цифры"));
    }

    [Test]
    public void IsValid_TooFewDigits_ThrowsArgumentException()
    {
        var ex = Assert.Throws<ArgumentException>(() => _validator.IsValid("123456789012"));
        Assert.That(ex.Message, Does.Contain("13 до 19"));
    }

    [Test]
    public void IsValid_TooManyDigits_ThrowsArgumentException()
    {
        var ex = Assert.Throws<ArgumentException>(() => _validator.IsValid("12345678901234567890"));
        Assert.That(ex.Message, Does.Contain("13 до 19"));
    }

    [Test]
    public void IsValid_13Digits_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => _validator.IsValid("0000000000000"));
    }

    [Test]
    public void IsValid_19Digits_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => _validator.IsValid("0000000000000000000"));
    }

    //Тесты белого ящика
    [Test]
    public void IsValid_NumberWithLeadingTrailingSpaces_ReturnsTrue()
    {
        string cardNumber = "  4532015112830366  ";
        bool result = _validator.IsValid(cardNumber);
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsValid_NumberWithMultipleSpacesInside_ReturnsTrue()
    {
        string cardNumber = "4532   0151   1283   0366";
        bool result = _validator.IsValid(cardNumber);
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsValid_NumberWithMixedSeparators_ReturnsTrue()
    {
        string cardNumber = "4532-0151 1283-0366";
        bool result = _validator.IsValid(cardNumber);
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsValid_AllZeros_ReturnsTrue()
    {
        string cardNumber = "0000000000000000";
        bool result = _validator.IsValid(cardNumber);
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsValid_OddLength_13Zeros_ReturnsTrue()
    {
        string cardNumber = "0000000000000";
        bool result = _validator.IsValid(cardNumber);
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsValid_DoesNotModifyInputString()
    {
        string input = "4532-0151-1283-0366";
        string inputCopy = string.Copy(input);
        _validator.IsValid(input);
        Assert.That(input, Is.EqualTo(inputCopy));
    }
}
