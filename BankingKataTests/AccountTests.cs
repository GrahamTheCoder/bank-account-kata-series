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
        [TestCaseSource(nameof(InitialBalances))]
        public void Depositing100IncrementsBalanceBy100(Money initialBalance)
        {
            var account = new Account(initialBalance);
            account.Deposit(new Money(100));
            Money balance = null;
            account.GetBalance(b => { balance = b; });
            Assert.That(balance, EqualTo(new Money(100)));
        }

        public IEnumerable<Money> InitialBalances()
        {
            yield return new Money(0);
        }

        private static EqualConstraint EqualTo(Money expected)
        {
            return Is.EqualTo(expected);
        }
    }

    public class Money : IEquatable<Money>
    {
        private readonly int m_Pounds;

        public Money(int pounds)
        {
            m_Pounds = pounds;
        }

        public bool Equals(Money other)
        {
            return m_Pounds == other?.m_Pounds;
        }
    }

    public class Account
    {
        private Money m_Balance;

        public Account(Money intialBalance)
        {
            m_Balance = intialBalance;
        }

        public void Deposit(Money money)
        {
            m_Balance = money;
        }

        public void GetBalance(Action<Money> action)
        {
            action(m_Balance);
        }
    }
}
