using System;

namespace BankingKata
{
    public interface IDateTimeSource
    {
        DateTime Now { get; }
    }

    class DateTimeSource : IDateTimeSource
    {
        public DateTime Now
        {
            get { return DateTime.Now; }
        }
    }
}