namespace Ordering.Domain.ValueObjects
{
    public record OrderName
    {
        private const int RequiredLength = 5;
        public string Value { get; }

        private OrderName(string value) => Value = value;

        public static OrderName Of(string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);

            if (value.Length != RequiredLength)
                throw new ArgumentOutOfRangeException(nameof(value),
                    $"Order name must be exactly {RequiredLength} characters long.");

            return new OrderName(value);
        }

        public override string ToString() => Value;
    }

}
