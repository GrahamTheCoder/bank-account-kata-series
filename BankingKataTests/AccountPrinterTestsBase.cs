using BankingKata;

namespace BankingKataTests
{
    internal static class AccountPrinterTestsBase
    {
        internal static Account CreateAccount(Money depositAmount = null)
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
}