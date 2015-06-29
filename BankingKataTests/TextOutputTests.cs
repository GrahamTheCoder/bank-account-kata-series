using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingKata;
using NUnit.Framework;

namespace BankingKataTests
{
    [TestFixture]
    class TextOutputTests
    {
        [Test]
        public void CurrentBalanceIsPrinted()
        {
            var acc = CreateAccount();
            var output = new StringWriter();
            var printer = new AccountPrinter(output);

            printer.PrintCurrentBalance(acc);

            Assert.That(output.ToString(), Is.StringContaining(new Money(0m).ToString()));
        }

        [Test]
        public void ModifiedBalanceIsPrinted()
        {
            var money = new Money(2m);
            var acc = CreateAccount(money);
            var output = new StringWriter();
            var printer = new AccountPrinter(output);

            printer.PrintCurrentBalance(acc);

            Assert.That(output.ToString(), Is.StringContaining(money.ToString()));
        }

        private static Account CreateAccount(Money depositAmount = null)
        {
            var transactionLog = new Ledger();
            var acc = new Account(transactionLog);
            if (depositAmount != null)
            {
                acc.Deposit(depositAmount);
            }
            return acc;
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
            m_StreamWriter.WriteLine(balance.ToString());
        }
    }
}
