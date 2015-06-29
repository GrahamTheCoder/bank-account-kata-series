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
    }
}