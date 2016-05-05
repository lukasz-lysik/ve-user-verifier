using System;

namespace UserVerifier.Password.Policies
{
    public interface IPasswordPolicy
    {
        int Length { get; }
        TimeSpan ValidityPeriod { get; }
    }
}
