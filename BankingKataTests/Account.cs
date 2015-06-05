using System;

namespace BankingKataTests
{
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

        public void Withdraw(Money money)
        {
            var argumentOutOfRangeException = new ArgumentOutOfRangeException("Cannot withdraw negative amount of money");
            money.AssertPositive(argumentOutOfRangeException);
            m_Balance -= money;
        }

        public void GetBalance(Action<Money> action)
        {
            action(m_Balance);
        }
    }
}