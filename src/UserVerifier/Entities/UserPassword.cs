using System;
using UserVerifier.ValueObjects;

namespace UserVerifier.Entities
{
    public class UserPassword
    {
        public UserId UserId { get; set; }
        public OneTimePassword Password { get; set; }
        public DateTime ValidUntil { get; set; }

        public bool IsExpired(DateTime now)
        {
            return ValidUntil < now;
        }
    }
}