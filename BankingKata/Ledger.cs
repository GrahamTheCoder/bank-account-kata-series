using System;
using System.Collections.Generic;
using System.Linq;

namespace BankingKata
{
    public class Ledger : ILedger
    {
        private readonly IDateTimeSource m_DateTimeSource;
        private readonly ICollection<RecordedTransaction> m_Transactions = new List<RecordedTransaction>();

        public Ledger(IDateTimeSource dateTimeSource = null)
        {
            m_DateTimeSource = dateTimeSource ?? new DateTimeSource();
        }

        public void Record(ITransaction transaction)
        {
            var recordedTransaction = new RecordedTransaction(m_DateTimeSource.Now, transaction);
            m_Transactions.Add(recordedTransaction);
        }

        public TArgument Accept<TArgument>(ITransactionVisitor<TArgument> visitor, TArgument initialValue)
        {
            return m_Transactions.Aggregate(initialValue, (argument, transaction) => visitor.Visit(transaction, argument));
        }

        public TArgument Accept<TArgument>(IRecordedTransactionVisitor<TArgument> visitor, TArgument initialValue)
        {
            return m_Transactions.Aggregate(initialValue, (argument, transaction) => visitor.Visit(transaction, argument));
        }
    }

    public interface IRecordedTransactionVisitor<T>
    {
        T Visit(RecordedTransaction transaction, T seed);
    }

    public class RecordedTransaction : ITransaction
    {
        private readonly DateTime m_DateTimeRecorded;
        private readonly ITransaction m_Transaction;

        public RecordedTransaction(DateTime dateTimeRecorded, ITransaction transaction)
        {
            m_DateTimeRecorded = dateTimeRecorded;
            m_Transaction = transaction;
        }

        public Money ApplyTo(Money balance)
        {
            return m_Transaction.ApplyTo(balance);
        }
    }
}