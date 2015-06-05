using System;
using System.Collections.Generic;
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
            var balance = GetBalance(account);
            var expectedMoney = initialBalance + deposit;
            Assert.That(balance, EqualTo(expectedMoney));
        }

        [Test]
        [TestCaseSource(nameof(InitialBalances))]
        public void Withdrawing100IncrementsBalanceBy100(Money initialBalance)
        {
            var account = new Account(initialBalance);
            var amountToWithdraw = new Money(100);
            account.Withdraw(amountToWithdraw);
            var balance = GetBalance(account);
            var expectedMoney = initialBalance - amountToWithdraw;
            Assert.That(balance, EqualTo(expectedMoney));
        }

        private static Money GetBalance(Account account)
        {
            Money balance = null;
            account.GetBalance(b => { balance = b; });
            return balance;
        }

        [Test]
        public void DepositingNegativeAmountThrows()
        {
            var account = new Account(new Money(0));
            var deposit = new Money(-30);
            TestDelegate tryDeposit = () => account.Deposit(deposit);
            Assert.That(tryDeposit, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void WithdrawingNegativeAmountThrows()
        {
            var account = new Account(new Money(0));
            var negativeAmountToWithdraw = new Money(-30);
            TestDelegate tryDeposit = () => account.Withdraw(negativeAmountToWithdraw);
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
}
