using System.IO;
using BankingKata;
using NUnit.Framework;
using static BankingKataTests.AccountPrinterTestsBase;

namespace BankingKataTests
{
    [TestFixture]
    class BalanceAccountPrinterTests
    {
        [Test]
        public void CanPrintBalanceOfEmptyAccount()
        {
            var acc = CreateAccount();
            var output = new StringWriter();
            var printer = new AccountPrinter(output);

            printer.PrintCurrentBalance(acc);

            Assert.That(output.ToString(), Is.StringContaining(new Money(0m).ToString()));
        }

        [Test]
        public void AccountBalanceIsPrinted()
        {
            var money = new Money(2m);
            var acc = CreateAccount(money);
            var output = new StringWriter();
            var printer = new AccountPrinter(output);

            printer.PrintCurrentBalance(acc);

            Assert.That(output.ToString(), Is.StringContaining(money.ToString()));
        }

        [Test]
        public void BalanceOutputIsOfCorrectFormat()
        {
            var acc = CreateAccount();
            var output = new StringWriter();
            var printer = new AccountPrinter(output);

            printer.PrintCurrentBalance(acc);

            Assert.That(output.ToString(), Is.EqualTo("Balance: £0.00\r\n"));
        }
    }

    [TestFixture]
    class LastTransactionAccountPrinterTests
    {
        [Test]
        public void BehavesIfNoTransactionsMade()
        {
            var acc = CreateAccount();
            var output = new StringWriter();
            var printer = new AccountPrinter(output);

            printer.PrintMostRecentTransaction(acc);

            Assert.That(output.ToString(), Is.StringContaining("No transactions have been made"));
        }
    }

    internal class AccountPrinter
    {
        private readonly TextWriter m_StreamWriter;

        public AccountPrinter(TextWriter streamWriter)
        {
            this.m_StreamWriter = streamWriter;
        }

        public void PrintCurrentBalance(Account acc)
        {
            var balance = acc.CalculateBalance();
            m_StreamWriter.WriteLine(@"Balance: {0}", balance.ToString());
        }

        public void PrintMostRecentTransaction(Account acc)
        {
            m_StreamWriter.WriteLine("No transactions have been made");
        }
    }
}
