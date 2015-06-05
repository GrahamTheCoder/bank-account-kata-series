﻿using System;
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
            var deposit = new Money(100);
            account.Deposit(deposit);
            Money balance = null;
            account.GetBalance(b => { balance = b; });
            var expectedMoney = initialBalance + deposit;
            Assert.That(balance, EqualTo(expectedMoney));
        }

        [Test]
        public void DepositingNegativeAmountThrows()
        {
            var account = new Account(new Money(0));
            var deposit = new Money(-30);
            TestDelegate tryDeposit = () => account.Deposit(deposit);
            Assert.That(tryDeposit, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }


        public IEnumerable<TestCaseData> InitialBalances()
        {
            yield return new TestCaseData(new Money(0)).SetName("No money");
            yield return new TestCaseData(new Money(50)).SetName("50 monies");
            yield return new TestCaseData(new Money(-20)).SetName("-20 monies");
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

        public static Money operator +(Money left, Money right)
        {
            var leftPounds = left.m_Pounds;
            var rightPounds = right.m_Pounds;
            return new Money(leftPounds + rightPounds);
        }

        public override string ToString()
        {
            return $"Pounds: {m_Pounds}";
        }

        public void AssertPositive(Exception exceptionOnAssertionFail)
        {
            if (m_Pounds <= 0) throw exceptionOnAssertionFail;
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
            var argumentOutOfRangeException = new ArgumentOutOfRangeException("Cannot deposit negative amount of money");
            money.AssertPositive(argumentOutOfRangeException);
            m_Balance += money;
        }

        public void GetBalance(Action<Money> action)
        {
            action(m_Balance);
        }
    }
}
