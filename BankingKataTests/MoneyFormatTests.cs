using BankingKata;
using NUnit.Framework;

namespace BankingKataTests
{
    [TestFixture]
    public class MoneyFormatTests
    {
        [Test]
        public void PrintsCurrencySymbol()
        {
            Assert.That(new Money(0m).ToString(), Is.StringStarting("£"));
        }


        [TestCase(0, "0.00")]
        [TestCase(1234, "1,234.00")]
        public void ToStringConvertsTheDecimalValueToAString(decimal moneyValue, string expectedMoneyStringWithoutCurrency)
        {
            var actualMoneyString = new Money(moneyValue).ToString();
            Assert.That(actualMoneyString, Is.StringEnding(expectedMoneyStringWithoutCurrency));
        }
    }
}