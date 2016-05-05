using System;

namespace UserVerifier.Common
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now { get { return DateTime.UtcNow; } }
    }
}