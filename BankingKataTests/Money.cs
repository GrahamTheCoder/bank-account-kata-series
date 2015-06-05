using System;

namespace BankingKataTests
{
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

        public static Money operator -(Money left, Money right)
        {
            var leftPounds = left.m_Pounds;
            var rightPounds = right.m_Pounds;
            return new Money(leftPounds - rightPounds);
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
}