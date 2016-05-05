using System;
using UserVerifier.Common;

namespace UserVerifier.Main
{
    public class FixedDateTimeProvider : IDateTimeProvider
    {
        public FixedDateTimeProvider(DateTime now)
        {
            Now = now;
        }

        public DateTime Now { get; private set; }
    }
}