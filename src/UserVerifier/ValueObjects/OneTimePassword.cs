namespace UserVerifier.ValueObjects
{
    public class OneTimePassword
    {
        public string Value { get; private set; }

        public OneTimePassword() {}

        public OneTimePassword(string value)
        {
            Value = value;
        }
    }
}