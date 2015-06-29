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


        [Test]
        public void ToStringConvertsTheDecimalValueToAString()
        {
            Assert.That(new Money(4m).ToString(), Is.EqualTo("£4.00"));
        }
    }
}