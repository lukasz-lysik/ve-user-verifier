using System;

namespace UserVerifier.Common
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}