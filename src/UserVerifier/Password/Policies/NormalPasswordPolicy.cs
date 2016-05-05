using System;

namespace UserVerifier.Password.Policies
{
    public class NormalPasswordPolicy : IPasswordPolicy
    {
        public int Length { get { return 8; }}
        public TimeSpan ValidityPeriod { get { return TimeSpan.FromSeconds(30); } }
    }
}