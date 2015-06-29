using BankingKata;
using NUnit.Framework;

namespace BankingKataTests
{
    [TestFixture]
    public class MoneyFormatTests
    {
        private string m_CurrencySymbol = "£";

        [Test]
        public void PrintsCurrencySymbol()
        {
            Assert.That(new Money(0m).ToString(), Is.StringStarting(m_CurrencySymbol));
        }


        [TestCase(0, "0.00")]
        [TestCase(1234, "1,234.00")]
        [TestCase(-1234, "-1,234.00")]
        public void ToStringConvertsTheDecimalValueToAString(decimal moneyValue, string expectedMoneyStringWithoutCurrency)
        {
            var actualMoneyString = new Money(moneyValue).ToString();
            var actualMoneyStringWithoutCurrency = actualMoneyString.Replace(m_CurrencySymbol, "");
            Assert.That(actualMoneyStringWithoutCurrency, Is.EqualTo(expectedMoneyStringWithoutCurrency));
        }
    }
}