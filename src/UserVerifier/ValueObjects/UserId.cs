namespace UserVerifier.ValueObjects
{
    public class UserId
    {
        public int Value { get; private set; }

        public UserId(int value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var p = (UserId)obj;
            return p.Value == Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}