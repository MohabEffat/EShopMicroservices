namespace Ordering.Domain.ValueObjects
{
    public record CustomerId
    {
        public Guid Value { get; }
        private CustomerId(Guid value) => Value = value;

        public static CustomerId Of(Guid value)
        {
            if (value == Guid.Empty)
                throw new DomainException("Customer Id can't be empty.");

            return new CustomerId(value);
        }
    }
}
