namespace BankingKata
{
    public class Account
    {
        private readonly ILedger _transactionLog;

        public Account(ILedger transactionLog)
        {
            _transactionLog = transactionLog;
        }

        public Account() : this(new Ledger(new DateTimeSource()))
        {
        }

        public void Deposit(Money money)
        {
            var depositTransaction = new CreditEntry(money);
            _transactionLog.Record(depositTransaction);
        }

        public Money CalculateBalance()
        {
            return Aggregate(new BalanceCalculatingVisitor(), new Money(0m));
        }

        public T Aggregate<T>(ITransactionVisitor<T> transactionVisitor, T seed)
        {
            return _transactionLog.Accept(transactionVisitor, seed);
        }

        public void Withdraw(Money money)
        {
            var debitEntry = new DebitEntry(money);
            _transactionLog.Record(debitEntry);
        }
    }
}