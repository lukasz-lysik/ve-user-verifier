using System;

namespace UserVerifier.Password.Policies
{
    public class RestrictedPasswordPolicy : IPasswordPolicy
    {
        public int Length { get { return 24; } }
        public TimeSpan ValidityPeriod { get { return TimeSpan.FromHours(1); } }
    }
}