using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace BankingKataTests
{
    [TestFixture]
    public class AccountTests
    {

        [Test]
        public void CanCreateAccount()
        {
            var intialBalance = new Money(0);
            new Account(intialBalance);
        }

        [Test]
        public void Depositing100IncrementsBalanceBy100()
        {
            var account = new Account(new Money(0));
            account.Deposit(new Money(100));
            Money balance = null;
            account.GetBalance(b => { balance = b; });
            Assert.That(balance, EqualTo(new Money(100)));
        }

        private static EqualConstraint EqualTo(Money expected)
        {
            return Is.EqualTo(expected);
        }
    }

    public class Money
    {
        public Money(int pounds)
        {
            throw new NotImplementedException();
        }
    }

    public class Account
    {
        private readonly Money m_IntialBalance;

        public Account(Money intialBalance)
        {
            m_IntialBalance = intialBalance;
        }

        public void Deposit(Money money)
        {
            throw new NotImplementedException();
        }

        public void GetBalance(Action<Money> action)
        {
            throw new NotImplementedException();
        }
    }
}
